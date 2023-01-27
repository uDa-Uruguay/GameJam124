using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Toma informacion del player para asi extraer su vida actual.
    [Header("Player data")]
    [SerializeField] private GameObject player;
    private PlayerHealth healthComponent;
    private PlayerScore playerScore;
    private int playerScoreData;

    [SerializeField] private GameObject healthBarObject;
    private HealthBar healtBar;

    [Header("Enemies killed")]
    [SerializeField] public bool enableShop = false;
    [SerializeField] private int enemiesKilledNeeded;
    private int enemiesKilled = 0;
    private bool shopAvailable = false;


    [Header("UI Text")]
    // Elementos de UI
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] private TMP_Text _playerScoreText;
    [SerializeField] private TMP_Text _playerHighscore;

    private AudioSource _audioSource;

    private void Awake()
    {
        // Extrae el componente PlayerHealth del gO Player.
        healthComponent = player.GetComponent<PlayerHealth>();

        // Extrae AudioSource
        _audioSource = GetComponent<AudioSource>();

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
        if (ShopButtonManager.isShopOpen) _audioSource.volume = 0f;
        else _audioSource.volume = 0.15f;

        // Vida del player.
        _playerHealthText.text = $"{healthComponent.currentHealth.ToString("0")}/{healthComponent.maxHealth}";

        // Score del player.
        _playerScoreText.text = playerScore.currentScore.ToString();

        // Tienda disponible
        if (shopAvailable)
        {
            shopAvailable = false;
            GameEvents.current.ShopAvaiable();
        }
    }

    // Actualiza el score.
    private void UpdateScore(int points)
    {
        playerScoreData += points;
        if (playerScore) playerScore.currentScore = playerScoreData;
    }

    // Actualizacion de enemigos muertos
    private void UpdateEnemiesKilled()
    {
        if (enemiesKilledNeeded > enemiesKilled) enemiesKilled++;
        else
        {
            enemiesKilled = enemiesKilledNeeded;
            shopAvailable = true;
        }
    }

    private void ShopUsed()
    {
        shopAvailable = false;
        enemiesKilled = 0;
    }

    private void showHighscore()
    {
        _playerHighscore.text = $"Score: {playerScoreData}";
    }

    private IEnumerator waitForEventSystem()
    {
        yield return new WaitForSeconds(0.2f);
        GameEvents.current.onEnemyDyingPoints += UpdateScore;
        GameEvents.current.onPlayerDying += showHighscore;
        GameEvents.current.onEnemyDying += UpdateEnemiesKilled;
        GameEvents.current.onShopDisable += ShopUsed;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
