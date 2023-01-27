using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> onEnemyDyingPoints;
    public void EnemyDyingPoints(int points)
    {
        onEnemyDyingPoints?.Invoke(points);
    }

    public event Action onEnemyDying;
    public void EnemyDying()
    {
        onEnemyDying?.Invoke();
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

    public event Action onShopAvailable;
    public void ShopAvaiable()
    {
        onShopAvailable?.Invoke();
    }

    public event Action onShopDisable;
    public void ShopDisable()
    {
        onShopDisable?.Invoke();
    }

    public event Action onDashBought;
    public void DashBough()
    {
        onDashBought?.Invoke();
    }
}
