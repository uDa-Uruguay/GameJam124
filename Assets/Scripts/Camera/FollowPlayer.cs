using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camara sigue al objeto que se especifique. Sea por tag o por GameObject.

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] public bool activated = true;

    [SerializeField] private string targetTag;
    [SerializeField] private float _initialSpeed;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _dashingSpeed;
    [SerializeField] private GameObject _target;
    private Dash playerDash;


    private Vector3 velocity = Vector3.zero;

    [SerializeField] private bool _searchByTag = true;

    void FixedUpdate()
    {
        _currentSpeed = _initialSpeed;
        if (!activated) return;

        if (_searchByTag)
        {
            GameObject target = GameObject.FindGameObjectWithTag(targetTag);
            if (target) // O sea, si encontró un GameObject con este tag y no dio null.
            {
                playerDash = target.GetComponent<Dash>();
                if (playerDash) dashingBoost(playerDash);
                
                float x = target.transform.position.x;
                float y = target.transform.position.y;
                // transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, transform.position.z), _speed * Time.deltaTime); Just a different way. (There's also lerp and slrp)
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(x, y, transform.position.z),ref velocity ,_currentSpeed);
            }
        } else
        {
            playerDash = _target.GetComponent<Dash>();
            if (playerDash) dashingBoost(playerDash);

            float x = _target.transform.position.x;
            float y = _target.transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, transform.position.z), _currentSpeed * Time.deltaTime);
        }

    }

    private void dashingBoost(Dash dashInfo)
    {
        if (dashInfo.isDashing)
        {
            _currentSpeed = _dashingSpeed;
        }
    }
}
