using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onEnemyTakingDamage;
    public void EnemyTakingDamage()
    {
        onEnemyTakingDamage?.Invoke();
    }

    public event Action onPlayerDying;
    public void PlayerDying()
    {
        onPlayerDying?.Invoke();
    }
}
