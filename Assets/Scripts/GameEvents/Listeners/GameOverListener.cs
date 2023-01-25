using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverListener : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
   
    private void Start()
    {
        GameEvents.current.onPlayerDying += createGameOver;
    }

    private void createGameOver()
    {
        gameOver.SetActive(true);
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerDying -= createGameOver;
    }
}
