using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "TopdownShooter/Gun", order = 2)]
public class Gun : ScriptableObject
{
    public string WeaponName;
    public float CoolDown;
    public AudioClip ShootSFX;
    public Sprite WeaponSprite;
}
