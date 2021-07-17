using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour, IMovement
{
    [SerializeField, Range(0, 10)]
    private int moveSpeed;

    private Rigidbody rb;
    private Character character;
    public float rotatingPlatformPush, xVelocity;
    private bool canMove;
    private readonly Vector3 workspace = new Vector3();
    private float CalculatedXVelocity => rotatingPlatformPush + xVelocity;
    private float CurrentPositionZ => transform.position.z;
    private float lastPositionZ;
    private static readonly WaitForSeconds waitforSeconds = new WaitForSeconds(5);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
        
    }

    private void Start()
    {
        StartCoroutine(CheckIfStuck());
    }

    private void Update()
    {
    }

    private IEnumerator CheckIfStuck()
    {
        while (true)
        {
            if (Math.Abs(lastPositionZ - CurrentPositionZ) < .1f)
            {
                character.OnFall();
            }
            lastPositionZ = CurrentPositionZ;
            yield return waitforSeconds;
        }
    }

    public void SetRotatingPlatformPush(float push)
    {
        rotatingPlatformPush = push;
        if (push == 0) xVelocity = 0;
    }

    public void Move()
    {
        if (!canMove) return;
        if (rotatingPlatformPush != 0)
        {
            AdjustXVelocity();
        }
        
        rb.SetVelocity(CalculatedXVelocity, float.NaN, moveSpeed * 50 * Time.deltaTime, workspace);
    }

    public void SetActive(bool isActive)
    {
        canMove = isActive;
    }

    public void SetXVelocity(float vel)
    {
        xVelocity = vel;
    }

    private void AdjustXVelocity()
    {
        if (Math.Abs(xVelocity) - Math.Abs(rotatingPlatformPush) < 2f)
        {
            if (rotatingPlatformPush < 0)
            {
                xVelocity += 6 * Time.deltaTime;
            }
            else
            {
                xVelocity -= 6 * Time.deltaTime;
            }
        }
        
    }
}