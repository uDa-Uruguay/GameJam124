using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Toma informacion del player para asi extraer su vida actual.
    [SerializeField] private GameObject player;
    private PlayerHealth healthComponent;

    [SerializeField] private GameObject healthBarObject;
    private HealthBar healtBar;

    // Elementos de UI
    [SerializeField] private TMP_Text _playerHealth;
    [SerializeField] private TMP_Text _playerScore;

    private void Awake()
    {
        healthComponent = player.GetComponent<PlayerHealth>();
        if (healthComponent) //is not null.
        {
            _playerHealth.text = healthComponent.maxHealth.ToString();
        }

        healtBar = healthBarObject.GetComponent<HealthBar>();
        if (healtBar && healthComponent) healtBar.maxHealth = healthComponent.maxHealth;
    }

    public void Update()
    {
        // Vida del player.
        _playerHealth.text = healthComponent.currentHealth.ToString();

        // FALTA SCORE.
    }
}
