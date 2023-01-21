using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private int healPower = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeHeal(healPower);
            Destroy(gameObject);
        }
    }
}
