using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private readonly Color DEFAULT_COLOR = Color.white;
    private readonly Color TRANSPARENT_COLOR = new Color(1, 1, 1, 0);

    IPlayerState state;
    Rigidbody2D rb;
    Animator anim;
    AudioSource aud;
    Collider2D col;
    SpriteRenderer sr;
    Vector2 lookDirection = new Vector2(1, 0);

    //Managers
    HealthManager healthManager;
    InventoryManager inventoryManager;
    WeaponManager weaponManager;

    [Header("Player Data")]
    public int MaxHP = 10;
    public bool canMove = false;

    [Header("Player Movement Settings")]
    public float WalkSpeed = 5f;
    public float RecoilWeak = 10;
    public float DodgeStrength = 10;

    [Header("Particle Settings")]
    public ParticleSystem WalkingParticles;
    public Transform WalkingParticlesPosition;
    [RangeAttribute(0, 100)]
    public int WalkingParticlesProbability = 10;

    [Header("Weapon Settings")]
    public GameObject aimLine;
    public Weapon Weapon;
    public Weapon Guitar;

    public IPlayerState State { get => state; set => state = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public AudioSource Aud { get => aud; set => aud = value; }
    public Collider2D Col { get => col; set => col = value; }
    public Vector2 LookDirection { get => lookDirection; set => lookDirection = value; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        state = PlayerState.STATE_IDLE;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        weaponManager = new WeaponManager(Weapon, Guitar);
        inventoryManager = new InventoryManager(0, new List<InventoryItem>());
        healthManager = new HealthManager(MaxHP);
    }
    private void Update()
    {
        State.Update(this);
        HandlePlayerAim();
        HandleRayCasting();
        HandleInput();
    }

    //FUNCIONES AUXILIARES.
    public void Heal(int n)
    {
        healthManager.DoHealing(n);
    }
    public void Damage(int n)
    {
        healthManager.DoDamage(n);
    }
    public void InstanciateParticles(ParticleSystem particles, Transform pos)
    {
        Instantiate(particles, pos);
    }
    public void HideWeapons()
    {
        Weapon.HideWeapon();
        Guitar.HideWeapon();
    }
    public void ShowWeapons()
    {
        Weapon.ShowWeapon();
        Guitar.ShowWeapon();
    }
    public void Shoot()
    {
        weaponManager.handleShooting(lookDirection);
    }
    public void ChangeColor (Color color)
    {
        sr.color = color;
    }
    public void ChangeEnemyCol(bool value)
    {
        if (GameObject.FindWithTag("Enemy")!=null)
        Physics2D.IgnoreCollision(GameObject.FindWithTag("Enemy").GetComponent<Collider2D>(),
                        GetComponent<Collider2D>(),
                        value);
    }
    public void EnableColorBlink(float delay)
    {
        InvokeRepeating("StartColorBlink", 0f, 0.1f);
        Invoke("StopColorBlink", delay);
    }
    public void StartColorBlink()
    {
        sr.color = (sr.color == DEFAULT_COLOR) ? TRANSPARENT_COLOR : DEFAULT_COLOR;
    }
    public void StopColorBlink()
    {
        CancelInvoke("StartColorBlink");
        sr.color = Color.white;
    }
    public int GetCurrentHealth()
    {
        return healthManager.GetCurrentHP();
    }
    public void AddItem(InventoryItem newItem)
    {
        inventoryManager.addItem(newItem);
    }
    public void RemoveItem(InventoryItem newItem)
    {
        inventoryManager.removeItem(newItem);
    }

    //Collision
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Item")) {
            collision.gameObject.GetComponent<PickeableItem>().onPlayerEnter(this);
            //if recogible añadir item y destruir el gameobject
        }
    }

    //Raycast
    public void HandleRayCasting()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            Interactable character = hit.collider.GetComponent<Interactable>();
            if (character != null)
            {
                character.onRaycastEnter();
                if (Input.GetKeyDown(KeyCode.E))
                    character.onInteract();
            }
        }
    }

    //Aim
    public void HandlePlayerAim()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 thisPosition = transform.position;
        Vector2 difference = (mousePosition - thisPosition);

        lookDirection = difference;
        lookDirection.Normalize();

        anim.SetFloat("LookX", lookDirection.x);
        anim.SetFloat("LookY", lookDirection.y);

        HandleAimLine(difference);
    }
    public void HandleAimLine(Vector2 difference)
    {
        SpriteRenderer aimSpriteRenderer = aimLine.GetComponent<SpriteRenderer>();
        aimSpriteRenderer.size = new Vector2(difference.magnitude, aimSpriteRenderer.size.y);

        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion mouseAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        aimLine.transform.rotation = mouseAngle;

        Weapon.SetRotation(mouseAngle);
        Guitar.SetRotation(mouseAngle);
    }

    //Input
    public void HandleInventoryOpening()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (UIManager.singleton.IsActiveScreen("uiPause"))
            {
                //Desactivamos el menu de inventario
                canMove = true;
                UIManager.singleton.ChangeActiveScreen("uiDefault");
            }
            else
            {
                //Activamos el menu de inventario
                canMove = false;
                UIManager.singleton.ChangeActiveScreen("uiPause");
                inventoryManager.updateInventory();
            }
        }
    }

    //FUNCIONES QUE DELEGAN SU COMPORTAMIENTO EN EL ESTADO
    public void HandleInput()
    {
        if (canMove)
        State.HandleInput(this);
        HandleInventoryOpening();
    }
    public void TakeDamage(int damage, Vector2 facingTo)
    {
        Damage(damage);
        State.HandleDamage(this, facingTo, RecoilWeak);
    }
}

