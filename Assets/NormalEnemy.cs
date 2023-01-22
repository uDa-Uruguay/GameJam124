using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    public float attackSpeed = 2f;
    public float canAttack;
    
    public int attackDamage = 2;
    public float _enemySpeed = 3f;
    public Transform target;

    public PlayerHealth playerHealt;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _enemySpeed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && attackSpeed <= canAttack)
        {
            playerHealt.TakeDamage(attackDamage);
            canAttack = 0f;
        }
        else
        {
            canAttack += Time.deltaTime;
        }
    }
}


