using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Heldable
{
    public SOWeapon WeaponData;

    public GameObject BulletPrefab;
    public Transform ShootPoint;
    public SpriteRenderer Hand;

    private AudioClip shootSfx;
    private float coolDown;
    private AudioSource soundPlayer;
    private bool canShoot = true;
    private SpriteRenderer sr;
    private SpriteRenderer srH;
    private Animator anim;

    private void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = WeaponData.WeaponSprite;
        srH = Hand;
        shootSfx = WeaponData.ShootSFX;
        coolDown = WeaponData.CoolDown;
    }
    public override void SetRotationMain(Quaternion r)
    {
        srH.enabled = true;
        sr.transform.rotation = r;

        if (r.w < 0.7 && r.w > 0)   
            sr.flipY = true;
        else
            sr.flipY = false;
        if (r.z < 0.9999 && r.z > 0.05)
        {
            sr.sortingOrder = -2;
            srH.sortingOrder = -1;
        }
        else
        {
            sr.sortingOrder = 1;
            srH.sortingOrder = 2;
        }
    }
    public override void SetRotationBack(Quaternion r)
    {
        Quaternion temp = transform.rotation;
        temp.z = -0.75f;
        transform.rotation = temp;

        srH.enabled = false;
        if (r.z < 0.9999 && r.z > 0.05)
        {
            sr.sortingOrder = 1;
        }
        else
        {
            sr.sortingOrder = -1;
        }
    }

    public override IEnumerator Shoot(Vector2 aim)
    {
        if (canShoot)
        {
            anim.Play("Shoot");
            BulletController projectile = Instantiate(BulletPrefab, transform.position, transform.rotation).GetComponent<BulletController>();
            projectile.Launch(aim, 1500);
            soundPlayer.PlayOneShot(shootSfx);

            CameraShaker.singleton.Shake(0.5f);

            canShoot = false;

            yield return new WaitForSeconds(coolDown);
            anim.Play("Idle");
            canShoot = true;
        }
        yield return null;

    }

    public override void HideWeapon()
    {
        sr.enabled = false;
        srH.enabled = false;
        wasMain = isMainWeapon;
        isMainWeapon = false;
    }

    public override void ShowWeapon()
    {
        sr.enabled = true;
        srH.enabled = true;
        isMainWeapon = wasMain;
    }
}
