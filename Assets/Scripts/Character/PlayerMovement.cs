using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    [SerializeField, Range(0, 10)]
    private int moveSpeed;

    [SerializeField]
    private float swerveSpeed = 0.5f;

    [SerializeField]
    private float maxSwerveAmount = 1f;

    private PlayerSwerveInputHandler swerveInputHandler;
    private Rigidbody rb;
    private float swerveAmount;
    private float rotatingPlatformPush;
    private bool canMove;
    private readonly Vector3 workspace = new Vector3();

    private void Awake()
    {
        swerveInputHandler = GetComponent<PlayerSwerveInputHandler>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!canMove) return;
        
        swerveAmount = Time.deltaTime * swerveSpeed * swerveInputHandler.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount, 0, 0);
    }

    public void SetRotatingPlatformPush(float push)
    {
        rotatingPlatformPush = push;
    }

    public void Move()
    {
        rb.SetVelocity(rotatingPlatformPush, float.NaN, moveSpeed * 50 * Time.deltaTime, workspace);
    }

    public void SetActive(bool isActive)
    {
        canMove = isActive;
    }
}