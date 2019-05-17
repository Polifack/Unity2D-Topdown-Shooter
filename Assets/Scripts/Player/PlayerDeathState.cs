using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public override void OnEnterState(Player character)
    {
        AudioManager.singleton.Play("playerDeath");

        character.Anim.Play("DeathBlend");
        character.HideWeapons();
    }

    public override void HandleDamage(Player character, Vector2 recoilDirection, float recoilStrength)
    {
        return;
    }

    public override void HandleInput(Player character)
    {
        return;
    }

    public override void Update(Player character)
    {
        return;
    }
}
