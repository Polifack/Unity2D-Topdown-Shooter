using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Configuración del elemento interactuable")]
    public float Timeout;

    public virtual void onRaycastEnter() { }
    public virtual void onInteract() { }
}
