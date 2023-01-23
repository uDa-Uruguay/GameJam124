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

    // Margenes del area de spawn
    [SerializeField] private float leftMargin;
    [SerializeField] private float righMargin;
    [SerializeField] private float topMargin;
    [SerializeField] private float bottomMargin;
    private float randomX;
    private float randomY;


    private void Start()
    {
        // Setting de la primer oleada.
        currentwave = waves[_waveNumber];
        timeIntervalW = currentwave.TimeSpawning;
        timeIntervalE = currentwave.TimeBetweenEnemies;

        GameEvents.current.onPlayerDying += StopSpawning;
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
        ResetEnemyPosition();
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

    private void ResetEnemyPosition()
    {
        // Setting del area donde spawnearan. (Rectangulo)
        randomX = Random.Range(leftMargin, righMargin);
        randomY = Random.Range(bottomMargin, topMargin);
    }

    // Spawnea de un enemugo y toma en cuenta la oleada actual y el intervalo de tiempo entre enemigo y enemigo.
    private IEnumerator spawnWave(Wave wave, float interval)
    {
        spawning = true;
        GameObject newEnemy = Instantiate(wave.EnemiesInWave[Random.Range(0, wave.EnemiesInWave.Length)], new Vector2(randomX, randomY), Quaternion.identity);
        yield return new WaitForSeconds(interval);
        spawning = false;
    }

    // Si el player murio se para el spawn
    private void StopSpawning()
    {
        stopSpawning = true;
    }
}
