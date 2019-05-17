using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    private Vector2 input;

    public override void OnEnterState(Player character) {
        character.Anim.Play("MovementBlend");

        AudioManager.singleton.Play("playerWalk");
    }
    public override void OnExitState(Player character)
    {
        AudioManager.singleton.Stop("playerWalk");
    }
    public override void HandleInput(Player character)
    {
        if (base.ComputeMovement().magnitude == 0) ToState(character, STATE_IDLE);
        else input = base.ComputeMovement();
        if (Input.GetKeyDown(KeyCode.X))character.HandleWeaponSwitch();
        if (Input.GetKeyDown(KeyCode.Space)) ToState(character, STATE_DODGE);

    }
    public override void Update(Player character)
    {
        character.HandleShooting();

        Vector2 position = character.Rb.position;
        position = position + Vector2.ClampMagnitude(input * character.WalkSpeed, character.WalkSpeed) * Time.deltaTime;
        character.Rb.MovePosition(position);

        if (Random.Range(0, 100) < character.WalkingParticlesProbability)
            character.InstanciateParticles(character.WalkingParticles, character.WalkingParticlesPosition);
    }
    public override void HandleDamage(Player character, Vector2 recoilDirection, float recoilStrength)
    {
        //Simplemente cambiamos de estado.
        if (character.GetCurrentHealth()<=0)
            ToState(character, STATE_DEATH);
        else
            ToState(character, STATE_RECOIL);
    }
}
