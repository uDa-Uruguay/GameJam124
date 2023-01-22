using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHeal : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private int healPower = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeHeal(healPower);
            Destroy(gameObject);
        }
    }
}
