using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase de un objeto que pueda cargar un jugador / NPC
public abstract class Heldable : MonoBehaviour
{
    public bool isMainWeapon = false;
    public bool wasMain;

    //Declarar este arma como el arma que esta siendo utilizada o que esta guardada
    public void ChangeWeapon()
    {
        isMainWeapon = !isMainWeapon;
    }
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
    public abstract IEnumerator Shoot(Vector2 aim);
}
