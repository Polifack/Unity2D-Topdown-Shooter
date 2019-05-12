using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void OnEnterState(Player character)
    {
        character.Anim.Play("IdleBlend");
    }

    public override void HandleInput(Player character)
    {
        if (base.ComputeMovement().magnitude != 0) ToState(character, STATE_WALK);
        character.HandleShooting();
    }

    public override void Update(Player character)
    {
        HandleInput(character);
        character.HandleShooting();
    }
}
