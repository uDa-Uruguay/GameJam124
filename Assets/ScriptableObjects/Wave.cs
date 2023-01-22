using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves", order = 1)]
public class Wave : ScriptableObject
{
    [field: SerializeField] public GameObject[] EnemiesInWave { get; private set; } // Posibles enemigos de la oleada.
    [field: SerializeField] public float TimeSpawning { get; private set; } // Tiempo que dura esta oleada.
    [field: SerializeField] public float TimeBetweenEnemies { get; private set; } // Tiempo entre enemigos de la oleada.
    
    // [field: SerializeField] public float NumberOfEnemies { get; private set; } // Número máximo de enemigos.

}
