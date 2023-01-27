using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float currentHealth = 0f;
    [SerializeField] public float maxHealth = 20f;

    [SerializeField] private GameObject healthBarObject;
    private HealthBar healthbar;

    public bool isInvulnerable = false;
    private void Start()
    {
        currentHealth = maxHealth;

        healthbar = healthBarObject.GetComponent<HealthBar>();
    }

    private void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
            GameEvents.current.PlayerDying();
        }

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
        if (isInvulnerable) return;

        currentHealth -= damage;

        if (healthbar) healthbar.UpdateHealth(currentHealth);
    }
}

