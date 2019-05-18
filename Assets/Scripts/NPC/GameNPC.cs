using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "TopdownShooter/NPC", order = 2)]
public class GameNPC : ScriptableObject
{
    public string Name;
    [TextArea(3, 10)]
    public string[] Dialog;
    public AnimationClip IdleFront;
    public AnimationClip IdleRight;
    public AnimationClip IdleLeft;
    public AnimationClip IdleBack;
    public Animator Animator;
    public Sprite defaultNpcSprite;

}
