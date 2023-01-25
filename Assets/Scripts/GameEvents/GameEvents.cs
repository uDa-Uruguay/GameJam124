using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    // More like enemy dying, actually
    public event Action<int> onEnemyTakingDamage;
    public void EnemyTakingDamage(int points)
    {
        onEnemyTakingDamage?.Invoke(points);
    }

    public event Action onPlayerDying;
    public void PlayerDying()
    {
        onPlayerDying?.Invoke();
    }

    public event Action onPlayerTakingDamage;
    public void PlayerTakingDamage()
    {
        onPlayerTakingDamage?.Invoke();
    }
}
