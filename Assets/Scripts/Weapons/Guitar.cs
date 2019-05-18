using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewGuitar", menuName = "TopdownShooter/Guitar", order = 3)]
public class Guitar : ScriptableObject
{
    public string Name;
    public float RiffTime;
    public AudioClip[] Song;
    public Sprite GuitarSprite;
}
