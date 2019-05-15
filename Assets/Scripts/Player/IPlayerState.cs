using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void OnEnterState(Player player);
    void OnExitState(Player player);
    void ToState(Player player, IPlayerState target);
    void Update(Player player);
    void HandleInput(Player player);
    void HandleDamage(Player player, Vector2 recoilDirection, float recoilStrength);
}
