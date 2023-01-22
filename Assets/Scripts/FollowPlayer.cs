using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camara sigue al objeto que se especifique. Sea por tag o por GameObject.

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] public bool activated = true;

    [SerializeField] private string targetTag;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private GameObject _target;


    [SerializeField] private bool _searchByTag = true;
    // private bool isDashing = false;

    void FixedUpdate()
    {
        if (!activated) return;

        if (_searchByTag)
        {
            GameObject target = GameObject.FindGameObjectWithTag(targetTag);
            if (target) // O sea, si encontró un GameObject con este tag y no dio null.
            {
                float x = target.transform.position.x;
                float y = target.transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, transform.position.z), _speed * Time.deltaTime);
            }
        } else
        {
            float x = _target.transform.position.x;
            float y = _target.transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, transform.position.z), _speed * Time.deltaTime);
        }

    }
}
