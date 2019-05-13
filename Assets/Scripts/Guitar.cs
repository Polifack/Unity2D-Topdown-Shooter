using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : MonoBehaviour
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
    public float shakeDuration;
    public float musicParticlesLifetime;
    public ParticleSystem particles;
    public CircleCollider2D soundCollider;
    public int SoundForce;


    private AudioSource soundPlayer;
    private bool canShoot =true;
    private SpriteRenderer sr;
    private SpriteRenderer srH;
    private SpriteRenderer srH2;
    private Animator anim;

    private float lastClickTime;
    private int nextNote;

    private void Awake()
    {
        effLight.intensity = 0;
        soundPlayer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = WeaponData.WeaponSprite;
        srH = Hand;
        if (Hand2 != null) srH2 = Hand2;
        nextNote = 0;
        ParticleSystem.MainModule temp = particles.main;
        temp.startLifetime = musicParticlesLifetime;
        particles.Stop();
        soundCollider.enabled = false;
    }

    public void setRotation(Quaternion r)
    {
        sr.transform.rotation = r;
        rt(r);
    }

    private void rt(Quaternion r)
    {
        if (r.w < 0.7 && r.w > 0) sr.flipY = true;
        else sr.flipY = false;
        if (r.z < 0.9999 && r.z > 0.05) { sr.sortingOrder = -2; srH.sortingOrder = -1; if (srH2 != null) srH2.sortingOrder = -1; }
        else { sr.sortingOrder = 1; srH.sortingOrder = 2; if (srH2 != null) srH2.sortingOrder = 2; }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 dir = SoundForce * ( collision.transform.position - transform.position );
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir);
    }

    public IEnumerator Shred()
    {
        //Get References
        Animator sAnim = guitarEffect.GetComponent<Animator>();
        SpriteRenderer sRend = guitarEffect.GetComponent<SpriteRenderer>();

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
            riffColorA.a = 0.1f;
            sRend.color = riffColorA;
            effLight.color = riffColor;
            
            //Particles + Lightning + Animation + Sound + Shake
            particles.Play();
            effLight.intensity = lightIntensity;
            sAnim.Play("GuitarSoundEff");
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
            sAnim.Play("GuitarSoundOff");
            effLight.intensity = 0;
            particles.Stop();
            soundCollider.enabled = false;
        }
        yield return null;

    }

    private void Update()
    {
        if (Time.time - lastClickTime > shredTime+0.2f)
        {
            nextNote = 0;
        }
    }
}
