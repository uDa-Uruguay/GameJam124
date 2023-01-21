using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 0f;
    private float maxHealth = 20f;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeHeal(int heal)
    {
        health += heal;

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0f)
        {
            health = 0f;
            Destroy(gameObject);
        }
    }
}

