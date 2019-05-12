using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletController : MonoBehaviour
{
    //Data variables
    public SOBullet BulletData;
    private AnimationClip fly;
    private AnimationClip impact;
    private float lifetime = 3f;

    //Component variables
    private Rigidbody2D rigidbody2d;
    private Animator animator;

    //Runtime variables
    private float currentLife = 0f;
    private Vector2 currentDir;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        fly = BulletData.bulletFlyingClip;
        impact = BulletData.bulletExplosionClip;
        lifetime = BulletData.BulletLifetime;
    }

    public void Launch(Vector2 direction, float force)
    {
        currentDir = direction;
        rigidbody2d.AddForce(currentDir * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        rigidbody2d.simulated = false;
        transform.position = other.transform.position;

        animator.Play("BulletImpact");

        if (other.gameObject.CompareTag("NotDestructible"))
        {
            Destroy(gameObject);
        }
            
        else
        {
            if (other.gameObject.CompareTag("Destructible"))
            {
                Breakable b = other.gameObject.GetComponent<Breakable>();
                b.takeDamage(1);
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyScript b = other.gameObject.GetComponent<EnemyScript>();
                b.takeDamage(1, currentDir);
            }
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        currentLife += Time.deltaTime;
        if (currentLife >= lifetime) Destroy(gameObject);
    }
}
