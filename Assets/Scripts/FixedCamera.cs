using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Asi evitamos que la camara vaya mas alla de los limites.
public class FixedCamera : MonoBehaviour
{
    [SerializeField] private float leftLimit = -4f;
    [SerializeField] private float rightLimit = 4f;
    [SerializeField] private float topLimit = 2.2f;
    [SerializeField] private float bottomLimit = -2.2f;

    private float currentX;
    private float currentY;

    [SerializeField] private FollowPlayer playerFollower;
    void Update()
    {
        currentX = transform.position.x;
        currentY = transform.position.y;

        if (leftLimit >= currentX || rightLimit <= currentX || topLimit <= currentY || bottomLimit >= currentY)
        {
            playerFollower.activated = false;
        } else
        {
            playerFollower.activated = true;
        }
    }
}
