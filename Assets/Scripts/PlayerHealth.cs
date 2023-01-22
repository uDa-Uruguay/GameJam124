using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float currentHealth = 0f;
    [SerializeField] public float maxHealth = 20f;

    [SerializeField] private GameObject healthBarObject;
    private HealthBar healthbar;
    private void Start()
    {
        currentHealth = maxHealth;

        healthbar = healthBarObject.GetComponent<HealthBar>();
    }

    public void TakeHeal(int heal)
    {
        currentHealth += heal;

        if (healthbar) healthbar.UpdateHealth(currentHealth);

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthbar) healthbar.UpdateHealth(currentHealth);
    }
}

