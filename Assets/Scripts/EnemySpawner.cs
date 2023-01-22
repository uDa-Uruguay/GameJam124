using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField] private GameObject enemy1Prefab;

    [SerializeField] private float timeIntervalW; // Tiempo de duración de la oleada.
    [SerializeField] private float timeIntervalE; // Tiempo entre enemigo y enemigo.
    [SerializeField] private Wave[] waves;
    // private GameObject[] enemies;

    private Wave currentwave;

    private int _waveNumber = 0;
    private bool stopSpawning = false;
    private bool spawning = false;

    private void Awake()
    {
        // Setting de la primer oleada.
        currentwave = waves[_waveNumber];
        timeIntervalW = currentwave.TimeSpawning;
        timeIntervalE = currentwave.TimeBetweenEnemies;
        // enemies = currentwave.EnemiesInWave;
    }

    private void FixedUpdate()
    {
        //Debug.Log(_waveNumber);
        

        if (stopSpawning) return;

        checkWaveNum();

        // Si paso el tiempo de la oleada, comienza la siguiente.
        if (Time.time >= timeIntervalW)
        {
            _waveNumber++;
            checkWaveNum();
            currentwave = waves[_waveNumber];
            timeIntervalW += currentwave.TimeSpawning;
            timeIntervalE = currentwave.TimeBetweenEnemies;
            spawning = false;
        }

        if (!spawning) StartCoroutine(spawnWave(currentwave, timeIntervalE));
    }

    private void checkWaveNum()
    {
        // Si ya no hay más oleadas para pasar, se termina.
        if (_waveNumber >= waves.Length)
        {
            stopSpawning = true;
            return;
        }
    }

    private IEnumerator spawnWave(Wave wave, float interval)
    {
        spawning = true;
        GameObject newEnemy = Instantiate(wave.EnemiesInWave[Random.Range(0, wave.EnemiesInWave.Length)], new Vector2(Random.Range(-9.30f, 9.30f), Random.Range(-5.5f, 5.5f)), Quaternion.identity);
        yield return new WaitForSeconds(interval);
        spawning = false;
    }
}
