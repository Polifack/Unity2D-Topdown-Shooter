using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotable : MonoBehaviour
{
    public GameObject explosion;
    public Transform[] explosionPosition;
    public float lifeTime = 1f;

    public void Explode()
    {
        foreach (Transform position in explosionPosition)
        {
            Instantiate(explosion, position);
        }
        Invoke("DestroySelf", lifeTime);
    }
    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
