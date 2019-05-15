using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : Heldable
{
    public SOWeapon WeaponData;

    public SpriteRenderer Hand;
    public SpriteRenderer Hand2;
    public AudioClip[] guitarSFX;
    public GameObject guitarEffect;
    public Light effLight;
    public float shredTime;
    public float animatorSpeed;
    public float lightIntensity;
    public float cooldownTransparency;
    public float shakeDuration;
    public float musicParticlesLifetime;
    public ParticleSystem particles;
    public CircleCollider2D soundCollider;
    public int SoundForce;

    private AudioSource soundPlayer;
    private bool canShoot = true;
    private SpriteRenderer sr;
    private SpriteRenderer srH;
    private SpriteRenderer srH2;
    private Animator anim;
    private Animator gAnim;
    private SpriteRenderer gSr;

    private float lastClickTime;
    private int nextNote;

    public override void SetRotationMain(Quaternion r)
    {
        srH.enabled = true;
        srH2.enabled = true;
        if (r.z < 0.9999 && r.z > 0.05)
        {
            sr.sortingOrder = -2;
            srH.sortingOrder = -1;
            srH2.sortingOrder = -2;
            Quaternion temp = transform.rotation;

            temp.z = 0.5f;
            transform.rotation = temp;
        }
        else
        {
            sr.sortingOrder = 1;
            srH.sortingOrder = 2;
            srH2.sortingOrder = 2;
            Quaternion temp = transform.rotation;
            temp.z = -0.5f;
            transform.rotation = temp;
        }
    }
    public override void SetRotationBack(Quaternion r)
    {
        srH.enabled = false;
        srH2.enabled = false;
        if (r.z < 0.9999 && r.z > 0.05)
        {
            sr.sortingOrder = 1;
        }
        else
        {
            sr.sortingOrder = -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 knockback = SoundForce * (collision.transform.position - transform.position);
        Debug.Log(collision.gameObject.name);
    }
    public override IEnumerator Shoot(Vector2 aim)
    {
        if (canShoot)
        {
            //Manage interrumptions            
            lastClickTime = Time.time;
            canShoot = false;

            //Check index out of bounds
            if (nextNote >= guitarSFX.Length) nextNote = 0;

            //Generate random color and add it
            Color riffColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            Color riffColorA = riffColor;
            riffColorA.a = cooldownTransparency;
            gSr.color = riffColorA;
            effLight.color = riffColor;

            //Particles + Lightning + Animation + Sound + Shake
            particles.Play();
            effLight.intensity = lightIntensity;
            gAnim.Play("GuitarSoundEff");
            anim.Play("Riff");
            soundPlayer.PlayOneShot(guitarSFX[nextNote]);
            CameraShaker.singleton.Shake(shakeDuration);

            //Check collisions
            soundCollider.enabled = true;

            //Waiting and Reset
            yield return new WaitForSeconds(shredTime);
            lastClickTime = Time.time;
            canShoot = true;
            nextNote++;

            //Stop effects
            anim.Play("Idle");
            gAnim.Play("GuitarSoundOff");
            effLight.intensity = 0;
            particles.Stop();
            soundCollider.enabled = false;
        }
        yield return null;

    }

    public override void HideWeapon()
    {
        sr.enabled = false;
        srH.enabled = false;
        srH2.enabled = false;
        wasMain = isMainWeapon;
        isMainWeapon = false;
    }

    public override void ShowWeapon()
    {
        sr.enabled = true;
        srH.enabled = true;
        srH2.enabled = true;
        isMainWeapon = wasMain;
    }

    //Awake used for references
    private void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        gAnim = guitarEffect.GetComponent<Animator>();
        gSr = guitarEffect.GetComponent<SpriteRenderer>();
        srH = Hand;
        srH2 = Hand2;
    }
    //Start used for initialization
    private void Start()
    {
        effLight.intensity = 0;
        sr.sprite = WeaponData.WeaponSprite;
        nextNote = 0;
        ParticleSystem.MainModule temp = particles.main;
        temp.startLifetime = musicParticlesLifetime;
        particles.Stop();
        soundCollider.enabled = false;
        gAnim.speed = animatorSpeed;
    }
    private void Update()
    {
        if (Time.time - lastClickTime > shredTime + 0.2f)
        {
            nextNote = 0;
        }
    }

}
