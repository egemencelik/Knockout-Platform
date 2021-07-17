using System;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField]
    private float rotatePush;

    private RotatingObject rotatingObject;

    private void Awake()
    {
        rotatingObject = GetComponent<RotatingObject>();
        rotatePush = rotatingObject.Rotation.z > 0 ? -rotatePush : rotatePush;
    }

    private void OnCollisionEnter(Collision other)
    {
        var movement = other.gameObject.GetComponent<IMovement>();
        movement?.SetRotatingPlatformPush(rotatePush);
    }

    private void OnCollisionExit(Collision other)
    {
        var movement = other.gameObject.GetComponent<IMovement>();
        movement?.SetRotatingPlatformPush(0);
    }
}