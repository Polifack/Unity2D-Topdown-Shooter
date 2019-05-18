using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Efectos de daño -> Particulas + Parpadeo + Camera Shake
public class PlayerStateRecoil : PlayerState
{
    float recoilTime = 0.5f;
    float cTime;

    public override void OnEnterState(Player character)
    {
        AudioManager.singleton.Play("playerGrunt");
        character.ChangeEnemyCol(true);
        cTime = 0;
        character.EnableColorBlink(recoilTime);
    }
    public override void OnExitState(Player character)
    {
        //instanciar particulas de polvo
        character.ChangeEnemyCol(false);
        cTime = 0;
    }

    public override void HandleDamage(Player character, Vector2 recoilDirection, float recoilStrength)
    {
        return; 
        //Durante el recoil no se puede recibir daño
    }

    //El input es igual que para otros estados con movimiento permitido
    public override void HandleInput(Player character)
    {
        if (base.ComputeMovement().magnitude != 0) ToState(character, STATE_WALK);
        character.Shoot();

        if (Input.GetKeyDown(KeyCode.Space)) ToState(character, STATE_DODGE);
    }

    public override void Update(Player character)
    {
        character.Shoot();

        if (cTime < recoilTime)
        {
            cTime += Time.deltaTime;
        }
        else
        {
            ToState(character, STATE_IDLE);
        }
    }
}
