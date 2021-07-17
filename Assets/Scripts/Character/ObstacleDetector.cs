using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    private float direction;
    private AIMovement movement;

    private void Awake()
    {
        movement = GetComponentInParent<AIMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var obstacle = other.GetComponent<Obstacle>();

        if (obstacle)
        {
            direction = obstacle.transform.position.x > transform.position.x ? -1 : 1;
        }

        var rotatingPlatform = other.GetComponent<RotatingPlatform>();

        if (rotatingPlatform)
        {
            direction = rotatingPlatform.transform.position.x > transform.position.x ? .8f : -.8f;
        }

        movement.SetXVelocity(direction * 5);
    }
    
    private void OnTriggerExit(Collider other)
    {
        direction = 0;
        movement.SetXVelocity(0);
    }
}