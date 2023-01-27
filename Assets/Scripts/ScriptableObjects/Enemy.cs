using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este archivo se puede crear desde el menu y nos permite settear facilmente enemigos con diferentes stats.

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemies")]

public class Enemy : ScriptableObject
{
    [field: SerializeField] public float EnemyHealth { get; private set; } // Vida maxima del enemigo.
    [field: SerializeField] public float MovementSpeed { get; private set; } // Velocidad de movimiento.
    [field: SerializeField] public float AttackSpeed { get; private set; } // Velocidad de ataque.
    [field: SerializeField] public float Damage { get; private set; } // Velocidad de ataque.
    [field: SerializeField] public Sprite EnemySprite { get; private set; } // Velocidad de ataque.
    [field: SerializeField] public int Score { get; private set; } // Velocidad de ataque.

    //[field: SerializeField] public Behavior[] EnemyBehavior { get; private set; } // Cómo se comporta. IA.

}
