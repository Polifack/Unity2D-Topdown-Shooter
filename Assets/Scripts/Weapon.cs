using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public SOWeapon WeaponData;

    public GameObject BulletPrefab;
    public Transform ShootPoint;

    private AudioClip shootSfx;
    private float coolDown;
    private AudioSource soundPlayer;
    private bool canShoot =true;

    private void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
        GetComponent<SpriteRenderer>().sprite = WeaponData.WeaponSprite;
        shootSfx = WeaponData.ShootSFX;
        coolDown = WeaponData.CoolDown;
    }

    public void setRotation(Quaternion r)
    {
        transform.rotation = r;
    }

    public IEnumerator Shoot(Vector2 aim)
    {
        if (canShoot)
        {
            BulletController projectile = Instantiate(BulletPrefab, transform.position, transform.rotation).GetComponent<BulletController>();
            projectile.Launch(aim, 1500);
            soundPlayer.PlayOneShot(shootSfx);

            CameraShaker.singleton.Shake();

            canShoot = false;

            yield return new WaitForSeconds(coolDown);

            canShoot = true;
        }
        yield return null;

    }
}
