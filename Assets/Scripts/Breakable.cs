using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Breakable : MonoBehaviour
{
    public int MaxHealth;

    private int currentHealth;
    private Animator animator;
    private void Start()
    {
        currentHealth = MaxHealth;
        animator = GetComponent<Animator>();
    }
    public void takeDamage(int n)
    {
        currentHealth -= n;
        Debug.Log(currentHealth);
        if (currentHealth == 0) breakThis();
    }

    private void breakThis()
    {
        animator.Play("DarkShelfBreak");
    }

    private void destroyThis()
    {
        animator.Play("DarkShelfDestroyed");
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
