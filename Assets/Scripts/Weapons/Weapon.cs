using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase de un objeto que pueda cargar un jugador / NPC
public abstract class Weapon : MonoBehaviour
{
    private bool isMainWeapon = false;

    public bool IsMainWeapon { get => isMainWeapon; set => isMainWeapon = value; }

    //Usada para hacer que las armas se muevan al mirar
    public void SetRotation(Quaternion r){
        if (isMainWeapon) SetRotationMain(r);
        else SetRotationBack(r);
    }

    //Delegamos en cada una de los 'heldables' la implementación de estas funciones
    public abstract void SetRotationMain(Quaternion r);
    public abstract void SetRotationBack(Quaternion r);

    public abstract void HideWeapon();
    public abstract void ShowWeapon();

    //Disparar o Atacar
    public abstract void handleShooting(Vector2 aim);
}
