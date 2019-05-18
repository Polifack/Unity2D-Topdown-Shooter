using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IPlayerState
{
    public static readonly PlayerStateWalk STATE_WALK = new PlayerStateWalk();
    public static readonly PlayerStateIdle STATE_IDLE = new PlayerStateIdle();
    public static readonly PlayerStateDodge STATE_DODGE = new PlayerStateDodge();
    public static readonly PlayerStateRecoil STATE_RECOIL = new PlayerStateRecoil();
    public static readonly PlayerStateDeath STATE_DEATH = new PlayerStateDeath();


    public virtual void OnEnterState(Player character) { }
    public virtual void OnExitState(Player character) { }
    public virtual void ToState(Player character, IPlayerState targetState)
    {
        character.State.OnExitState(character);
        character.State = targetState;
        character.State.OnEnterState(character);
    }
    public abstract void Update(Player character);
    public abstract void HandleInput(Player character);
    public abstract void HandleDamage(Player character, Vector2 recoilDirection, float recoilStrength);

    public virtual Vector2 ComputeMovement()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

}
