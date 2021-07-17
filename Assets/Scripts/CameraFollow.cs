using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform cameraPositionOnPaint, paintWall;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float positionChangeSmoothTime, followSmoothTime;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private bool focusedOnPaint;
    private Transform currentFocus;
    private Vector3 currentPosition;


    private void OnEnable()
    {
        LevelManager.OnLevelWon += FocusToPlayer;
        LevelManager.OnLevelLost += FocusToPlayer;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelWon -= FocusToPlayer;
        LevelManager.OnLevelLost -= FocusToPlayer;
    }

    private void FocusToPlayer()
    {
        focusedOnPaint = true;
        currentPosition = player.position + new Vector3(0, 3, 10);
        currentFocus = player;
    }

    private void FocusToPlayer(Character character)
    {
        focusedOnPaint = true;
        currentPosition = player.position + new Vector3(0, 3, 10);
        currentFocus = player;
    }

    private void LateUpdate()
    {
        if (!focusedOnPaint)
        {
            targetPosition = player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSmoothTime);

            transform.LookAt(player);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, currentPosition, ref velocity, positionChangeSmoothTime);
            transform.LookAt(currentFocus);
        }
    }

    public void Change()
    {
        focusedOnPaint = !focusedOnPaint;
        if (focusedOnPaint)
        {
            currentPosition = cameraPositionOnPaint.position;
            currentFocus = paintWall;
        }
    }
}