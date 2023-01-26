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

    public event Action<int, int> onWeaponChange;
    public void WeaponChange(int maxAmmo, int currentAmmo)
    {
        onWeaponChange?.Invoke(maxAmmo, currentAmmo);
    }

    public event Action onNoMoreAmmo;
    public void NoMoreAmmo()
    {
        onNoMoreAmmo?.Invoke();
    }

    public event Action onAmmoCollected;
    public void AmmoCollected()
    {
        onAmmoCollected?.Invoke();
    }

}
