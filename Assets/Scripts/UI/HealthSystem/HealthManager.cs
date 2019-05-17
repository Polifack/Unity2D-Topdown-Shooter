using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Logica del Manageo de HP
public class HealthManager
{
    int _maxHP;
    int _currentHP;

    public event EventHandler OnDamaged;
    public event EventHandler OnHeal;

    public HealthManager(int n)
    {
        //Inicialización de los valores
        _maxHP = n;
        _currentHP = _maxHP;

        //Inicialización de la UI
        if (HealthUI.instance != null)
            HealthUI.instance.Setup(this);
    }

    public int GetCurrentHP()
    {
        return _currentHP;
    }
    public int GetMaxHP()
    {
        return _maxHP;
    }

    public void DoDamage(int n)
    {
        _currentHP = ((_currentHP-n) > 0) ? (_currentHP - n) : 0;

        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
    }
    public void DoHealing(int n)
    {
        _currentHP = ((_currentHP + n) < _maxHP) ? (_currentHP + n) : _maxHP;

        if (OnHeal != null) OnHeal(this, EventArgs.Empty);
    }
}
