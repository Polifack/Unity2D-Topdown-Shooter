using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float WalkSpeed = 5f;

    IPlayerState state;
    Rigidbody2D rb;
    Animator anim;
    AudioSource aud;
    Vector2 lookDirection = new Vector2(1, 0);
    


    [Header("Particle Settings")]
    public ParticleSystem WalkingParticles;
    public Transform WalkingParticlesPosition;
    [RangeAttribute(0, 100)]
    public int WalkingParticlesProbability = 10;

    [Header("Sound Settings")]
    public AudioClip WalkSFX;

    [Header("Shoot Settings")]
    public GameObject aimLine;
    public Weapon Weapon;

    public IPlayerState State { get => state; set => state = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public AudioSource Aud { get => aud; set => aud = value; }

    private void Awake()
    {
        state = PlayerState.STATE_IDLE;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

    }
    private void Update()
    {
        State.Update(this);
        HandlePlayerAim();
        HandleRayCasting();
    }



    public void HandleInput(Input input)
    {
        State.HandleInput(this);
    }
    public void PlaySoundLoop(AudioClip clip)
    {
        aud.loop = true;
        aud.clip = clip;
        if (!aud.isPlaying)
            aud.Play();
    }
    public void StopSoundLoop()
    {
        aud.Stop();
    }
    public void PlaySoundOneShot(AudioClip clip)
    {
        aud.PlayOneShot(clip);
    }
    public void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(Weapon.Shoot(lookDirection));
    }
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

        if (Weapon) Weapon.setRotation(mouseAngle);
    }
    public void InstanciateParticles(ParticleSystem particles, Transform pos)
    {
        Instantiate(particles, pos);
    }
}

