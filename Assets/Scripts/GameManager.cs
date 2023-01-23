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
    private PlayerScore playerScore;
    private int playerScoreData;

    [SerializeField] private GameObject healthBarObject;
    private HealthBar healtBar;

    // Elementos de UI
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] private TMP_Text _playerScoreText;

    private void Awake()
    {
        healthComponent = player.GetComponent<PlayerHealth>();
        if (healthComponent) //is not null.
        {
            _playerHealthText.text = healthComponent.maxHealth.ToString();
        }

        playerScore = player.GetComponent<PlayerScore>();
        if (playerScore) _playerScoreText.text = playerScore.currentScore.ToString();


        healtBar = healthBarObject.GetComponent<HealthBar>();
        if (healtBar && healthComponent)
        {
            //Debug.Log("Working");
            healtBar.maxHealth = healthComponent.maxHealth;
        }

    }

    public void Update()
    {
        // Vida del player.
        _playerHealthText.text = healthComponent.currentHealth.ToString();

        // Score del player.
        _playerScoreText.text = playerScore.currentScore.ToString();
    }
}
