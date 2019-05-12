using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IPlayerState
{
    public static readonly PlayerWalkState STATE_WALK = new PlayerWalkState();
    public static readonly PlayerIdleState STATE_IDLE = new PlayerIdleState();


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

    public virtual Vector2 ComputeMovement()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

}
