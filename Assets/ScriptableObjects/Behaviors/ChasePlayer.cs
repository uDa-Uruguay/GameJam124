using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este comportamiento o "Behavior" simplemente hace que quien lo posea, persiga al Player (o a quien se le asigne como tag en 'targetTag')

[CreateAssetMenu(menuName = "ScriptableObjects/Behavior/Chase")]

public class ChasePlayer : Behavior
{
    public string targetTag;
    public override void behavior(EnemyData enemy)
    {
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if (target) // O sea, si encontró un GameObject con este tag y no dio null.
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, target.transform.position, enemy._movementSpeed * Time.deltaTime);
        }
    }

}
