using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager
{
    private Weapon _mainWeapon;
    private Weapon _secondaryWeapon;

    public WeaponManager(Weapon primary, Weapon secondary)
    {
        _mainWeapon = primary;
        _secondaryWeapon = secondary;
        setPrimaryWeapon();        
    }

    public void setSecondaryWeapon()
    {
        _mainWeapon.IsMainWeapon = false;
        _secondaryWeapon.IsMainWeapon = true;
    }
    public void setPrimaryWeapon()
    {
        _mainWeapon.IsMainWeapon = true;
        _secondaryWeapon.IsMainWeapon = false;
    }
    public void handleShooting(Vector2 aim)
    {
        if (Input.GetMouseButtonDown(0))
        {
            setPrimaryWeapon();
            _mainWeapon.handleShooting(aim);
        }
        if (Input.GetMouseButtonDown(1))
        {
            setSecondaryWeapon();
            _secondaryWeapon.handleShooting(aim);
        }
            
    }
}
