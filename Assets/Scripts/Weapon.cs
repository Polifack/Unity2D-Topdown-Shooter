using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public SOWeapon WeaponData;

    public GameObject BulletPrefab;
    public Transform ShootPoint;
    public SpriteRenderer Hand;
    public SpriteRenderer Hand2;

    private AudioClip shootSfx;
    private float coolDown;
    private AudioSource soundPlayer;
    private bool canShoot =true;
    private SpriteRenderer sr;
    private SpriteRenderer srH;
    private SpriteRenderer srH2;
    private Animator anim;

    private void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = WeaponData.WeaponSprite;
        srH = Hand;
        if (Hand2 != null) srH2 = Hand2;
        shootSfx = WeaponData.ShootSFX;
        coolDown = WeaponData.CoolDown;
    }

    public void setRotation(Quaternion r)
    {
        sr.transform.rotation = r;
        //no rotar si z=-0.5 y w=0.8 
        //Debug.Log(r.w);
        //Debug.Log(r.z);
        rt(r);
    }

    private void rt(Quaternion r)
    {
        if (r.w < 0.7 && r.w > 0) sr.flipY = true;
        else sr.flipY = false;
        if (r.z < 0.9999 && r.z > 0.05) { sr.sortingOrder = -2; srH.sortingOrder = -1; if (srH2 != null) srH2.sortingOrder = -1; }
        else { sr.sortingOrder = 1; srH.sortingOrder = 2; if (srH2 != null) srH2.sortingOrder = 2; }

    }

    public IEnumerator Shoot(Vector2 aim)
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
}
