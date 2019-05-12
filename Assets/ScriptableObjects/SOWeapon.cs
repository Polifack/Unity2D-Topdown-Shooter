using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/Weapon", order = 1)]
public class SOWeapon : ScriptableObject
{
    public string WeaponName;
    public float CoolDown;
    public AudioClip ShootSFX;
    public Sprite WeaponSprite;
}
