using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private TMP_Text _playerHighscore;

    private void Awake()
    {
        // Extrae el componente PlayerHealth del gO Player.
        healthComponent = player.GetComponent<PlayerHealth>();
        if (healthComponent) //is not null.
        {
            // Extrae la vida máxima del componente. (ESTO ESTA EN DESUSO)
            _playerHealthText.text = healthComponent.maxHealth.ToString();
        }

        // Extrae componente de Score del Player y lo asigna a _playerScoreText
        playerScore = player.GetComponent<PlayerScore>();
        if (playerScore) _playerScoreText.text = playerScore.currentScore.ToString();

        // Extrae el componente HealthBar del objeto del mismo nombre.
        healtBar = healthBarObject.GetComponent<HealthBar>();
        if (healtBar && healthComponent)
        {
            // Asigna maxhealth iguales.
            healtBar.maxHealth = healthComponent.maxHealth;
        }

        // Para evitar problemas de creacion. Usando Start soluciona tambien.
        StartCoroutine(waitForEventSystem());
    }

    public void Update()
    {
        // Vida del player.
        _playerHealthText.text = healthComponent.currentHealth.ToString();

        // Score del player.
        _playerScoreText.text = playerScore.currentScore.ToString();
    }

    // Actualiza el score. (Actualmente no toma parametros personalizados)
    private void UpdateScore()
    {
        playerScoreData += 15;
        if (playerScore) playerScore.currentScore = playerScoreData;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void showHighscore()
    {
        _playerHighscore.text = $"Score: {playerScoreData}";
    }

    private IEnumerator waitForEventSystem()
    {
        yield return new WaitForSeconds(0.2f);
        GameEvents.current.onEnemyTakingDamage += UpdateScore;
        GameEvents.current.onPlayerDying += showHighscore;
    }
}
