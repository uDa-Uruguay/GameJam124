using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float currentHealth = 0f;
    [SerializeField] public float maxHealth = 20f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeHeal(int heal)
    {
        currentHealth += heal;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0f)
        {
            currentHealth = 0f;
            Destroy(gameObject);
        }
    }
}

