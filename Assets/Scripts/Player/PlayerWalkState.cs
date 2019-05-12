using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    private Vector2 input;

    public override void OnEnterState(Player character) {
        character.Anim.Play("MovementBlend");
        character.PlaySoundLoop(character.WalkSFX);
    }

    public override void OnExitState(Player character)
    {
        character.StopSoundLoop();
    }

    public override void HandleInput(Player character)
    {
        if (base.ComputeMovement().magnitude == 0) ToState(character, STATE_IDLE);
        else input = base.ComputeMovement();
    }

    public override void Update(Player character)
    {
        HandleInput(character);
        character.HandleShooting();

        Vector2 position = character.Rb.position;
        position = position + Vector2.ClampMagnitude(input * character.WalkSpeed, character.WalkSpeed) * Time.deltaTime;
        character.Rb.MovePosition(position);

        if (Random.Range(0, 100) < character.WalkingParticlesProbability)
            character.InstanciateParticles(character.WalkingParticles, character.WalkingParticlesPosition);
    }
}
