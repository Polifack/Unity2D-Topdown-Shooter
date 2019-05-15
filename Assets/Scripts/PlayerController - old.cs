using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rigidbody2d;
    private Vector2 lookDirection = new Vector2(1, 0);
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int currentHealth;
    private bool isInvencible = false;
    private bool canMove = true;

    [Header("Player Stats")]
    public int MaxHealth = 5;
    public float InvencibilityTime = 1f;

    [Header("Movement Settings")]
    public float Speed = 3.0f;

    [Header("Particle Settings")]
    public ParticleSystem WalkingParticles;
    public Transform FeetPosition;
    [RangeAttribute(0, 100)]
    public int ParticleWalkProbability = 10;

    [Header("Sound Settings")]
    public AudioSource AudioSourceLoop;
    public AudioSource AudioSourceOneShot;
    public AudioClip WalkSFX;

    [Header("Shooting Settings")]
    public GameObject aimLine;
    public Gun Weapon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentHealth = MaxHealth;
    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        manageAiming();
        manageAnimation(input);
        if (canMove) manageMovement(input);
        manageParticles(input);
        manageSound(input);
        ManageShooting();
    }
    private void manageAiming()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 thisPosition = transform.position;
        Vector2 difference = (mousePosition - thisPosition);

        lookDirection = difference;
        lookDirection.Normalize();

        SpriteRenderer aimSpriteRenderer = aimLine.GetComponent<SpriteRenderer>();
        aimSpriteRenderer.size = new Vector2(difference.magnitude, aimSpriteRenderer.size.y);

        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion mouseAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        aimLine.transform.rotation = mouseAngle;

        Weapon.SetRotation(mouseAngle);
    }

    private void manageMovement(Vector2 input)
    {
        if (input.magnitude > 0)
        {
            // Movimiento absoluto
            Vector2 position = rigidbody2d.position;
            position = position + Vector2.ClampMagnitude(input * Speed, Speed) * Time.deltaTime;
            rigidbody2d.MovePosition(position);

            // Movimiento relativo
            /*
            Vector2 position = rigidbody2d.position;
            Vector2 lookDirectionOrt = new Vector2(lookDirection.y, lookDirection.x);

            Vector2 movementForwards = lookDirection * input.y;
            Vector2 movementSidewards = lookDirectionOrt * -input.x;

            Vector2 movement = movementSidewards + movementForwards;

            position = position + Vector2.ClampMagnitude(movement * Speed, Speed) * Time.deltaTime;
            rigidbody2d.MovePosition(position);
            */
        }
    }

    private void manageParticles(Vector2 input)
    {
        if (input.magnitude > 0 && (Random.Range(0, 100) < 10))
        {
            Instantiate(WalkingParticles, FeetPosition);
        }
    }

    private void manageSound(Vector2 input)
    {
        if (input.magnitude > 0) PlaySoundLoop(WalkSFX);
        else StopSoundLoop();
    }

    private void manageAnimation(Vector2 input)
    {
        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
        animator.SetFloat("Speed", input.magnitude);
    }

    public void ManageShooting()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(Weapon.Shoot(lookDirection));
    }
    public void TakeDamage(int dmg, Vector2 dir)
    {
        StartCoroutine(takeDamage(dmg, dir));
    }

    private IEnumerator takeDamage(int dmg, Vector2 dir)
    {
        if (!isInvencible)
        {
            isInvencible = true;
            canMove = false;

            spriteRenderer.color = Color.white;
            InvokeRepeating("startColorFlip", 0f, 0.2f);
            currentHealth -= dmg;
            Vector2 pos = rigidbody2d.position;
            Vector2 newPos = pos + 2*dir;
            rigidbody2d.MovePosition(newPos);

            //soundPlayer.PlayOneShot(takeDamage);

            CameraShaker.singleton.Shake(0.1f);
            
            yield return new WaitForSeconds(0.2f);
            canMove = true;
            yield return new WaitForSeconds(InvencibilityTime);

            Debug.Log("hp");

            isInvencible = false;
            CancelInvoke("startColorFlip");
            spriteRenderer.color = Color.white;

        }
        yield return null;
    }

    private void startColorFlip()
    {
        spriteRenderer.color = (spriteRenderer.color == Color.white) ? Color.black : Color.white;
    }

    public void PlaySoundLoop(AudioClip clip)
    {
        AudioSourceLoop.loop = true;
        AudioSourceLoop.clip = clip;
        if (!AudioSourceLoop.isPlaying)
            AudioSourceLoop.Play();
    }
    public void StopSoundLoop()
    {
        AudioSourceLoop.Stop();
    }
    public void PlaySoundOneShot(AudioClip clip)
    {
        AudioSourceOneShot.PlayOneShot(clip);
    }
}