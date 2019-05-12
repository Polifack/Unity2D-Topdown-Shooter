using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = v;
    }
}
