using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDodge : PlayerState
{
    float dodgeTime = 0.5f;
    float cTime;
    Vector2 dodgeDirection;
    public override void OnEnterState(Player character)
    {
        AudioManager.singleton.Play("playerDodge");

        character.Anim.Play("DodgeBlend");
        character.InstanciateParticles(character.WalkingParticles, character.WalkingParticlesPosition);
        character.ChangeEnemyCol(true);
        cTime = 0;
        dodgeDirection = character.LookDirection;
        character.HideWeapons();
    }
    public override void OnExitState(Player character)
    {
        character.InstanciateParticles(character.WalkingParticles, character.WalkingParticlesPosition);
        character.ChangeEnemyCol(false);
        cTime = 0;
        character.ShowWeapons();
    }
    public override void HandleInput(Player character)
    {
        return;
        //No admite input durante un dodge.
    }
    public override void Update(Player character)
    {
        if (cTime < dodgeTime)
        {
            Vector2 position = character.Rb.position;
            position = position + Vector2.ClampMagnitude(dodgeDirection * character.DodgeStrength, character.DodgeStrength) * Time.deltaTime;
            character.Rb.MovePosition(position);
            cTime += Time.deltaTime;
        }
        else
        {
            ToState(character, STATE_IDLE);
        }
    }
    public override void HandleDamage(Player character, Vector2 recoilDirection, float recoilStrength)
    {
        return;
        //Durante un dodge no se recibe daño
    }
}
