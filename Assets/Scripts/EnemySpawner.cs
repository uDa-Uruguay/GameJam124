using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controlador de olas o "waves"
public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private float timeIntervalW; // Tiempo de duración de la oleada.
    [SerializeField] private float timeIntervalE; // Tiempo entre enemigo y enemigo.
    [SerializeField] private Wave[] waves; // Almacena todas las waves.

    private Wave currentwave; // Lleva registro de cúal es la oleada actual.
    private int _waveNumber = 0; // Número de oleada actual.

    private bool stopSpawning = false; // Detiene las oleadas.
    private bool spawning = false; // Check para evitar que la corrutina se ejecute sin parar.

    private void Awake()
    {
        // Setting de la primer oleada.
        currentwave = waves[_waveNumber];
        timeIntervalW = currentwave.TimeSpawning;
        timeIntervalE = currentwave.TimeBetweenEnemies;
    }

    private void FixedUpdate()
    {
        // Si ya no hay mas oleadas, no se ejecuta más alla de este codigo.
        if (stopSpawning) return;
        if (finalWave()) return;

        // Si paso el tiempo de la oleada, comienza la siguiente.
        if (Time.time >= timeIntervalW)
        {
            _waveNumber++; 
            if (finalWave()) return;

            currentwave = waves[_waveNumber];
            timeIntervalW += currentwave.TimeSpawning;
            timeIntervalE = currentwave.TimeBetweenEnemies;
            spawning = false;
        }

        // Si ya paso el tiempo necesario entre enemigo y enemigo, se ejecuta nuevamente.
        if (!spawning) StartCoroutine(spawnWave(currentwave, timeIntervalE));
    }

    // Si ya no hay más oleadas para pasar, retorna true.
    private bool finalWave()
    {
        if (_waveNumber >= waves.Length)
        {
            stopSpawning = true;
            return true;
        }
        else return false;
    }

    // Spawnea de un enemugo y toma en cuenta la oleada actual y el intervalo de tiempo entre enemigo y enemigo.
    private IEnumerator spawnWave(Wave wave, float interval)
    {
        spawning = true;
        GameObject newEnemy = Instantiate(wave.EnemiesInWave[Random.Range(0, wave.EnemiesInWave.Length)], new Vector2(Random.Range(-9.30f, 9.30f), Random.Range(-5.5f, 5.5f)), Quaternion.identity);
        yield return new WaitForSeconds(interval);
        spawning = false;
    }
}
