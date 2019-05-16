using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeRoomManager : MonoBehaviour
{
    public TilemapRenderer t1;
    public TilemapRenderer t2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("cambiando room...");
            changeRoom();
        }
    }

    //Problemas: Se desrenderizan todos los edificios -> los edificios a los que se pueda entrar tienen que ir en un tilemap / spriterenderer aparte
    //Añadir: Oscurecer el resto del mundo
    private void changeRoom()
    {
        t1.enabled = !t1.enabled;
        t2.enabled = !t2.enabled;
        TilemapCollider2D t = t1.gameObject.GetComponent<TilemapCollider2D>();
        if (t != null)
            t.enabled = !t.enabled;
    }
}
