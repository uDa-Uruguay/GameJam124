using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Informacion de forma 'static' para acceder desde cualquier sitio. Stats y power ups.
public class CurrentStats : MonoBehaviour
{
    public static CurrentStats current;
    private void Awake()
    {
        current = this;
    }

    public bool haveDash = false;
    public bool haveShield = false;
    public float speedBoost = 1f;
    public int maxHealth = 20;
    public float bulletSpeedBoost = 1f;
    public float damageBoost = 1f;
}
