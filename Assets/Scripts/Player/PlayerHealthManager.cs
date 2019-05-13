using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Logica del Manageo de HP
public class PlayerHealthManager
{
    int maxHP;
    int currentHP;
    public UIHealthManager uiManager;

    public event EventHandler OnDamaged;
    public event EventHandler OnHeal;

    public PlayerHealthManager(int n)
    {
        maxHP = n;
        currentHP = maxHP;
        uiManager = UIHealthManager.instance;
        uiManager.Setup(this);
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }

    public void DoDamage(int n)
    {
        currentHP = ((currentHP-n) > 0) ? (currentHP - n) : 0;

        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
    }

    public void DoHealing(int n)
    {
        currentHP = ((currentHP + n) < maxHP) ? (currentHP + n) : maxHP;

        if (OnHeal != null) OnHeal(this, EventArgs.Empty);
    }
}
