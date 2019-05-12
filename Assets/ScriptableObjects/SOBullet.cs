using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "Weapon/Bullet", order = 2)]
public class SOBullet : ScriptableObject
{
    public float BulletLifetime = 3f;

    public AnimationClip bulletFlyingClip;
    public AnimationClip bulletExplosionClip;
}
