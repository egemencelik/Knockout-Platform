using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonut : MonoBehaviour
{
    [SerializeField]
    private Transform movingStick;

    private bool isForward;
    private bool isMoving;
    private Vector3 back, forward;
    private float timeSinceLastMove;
    
    private void OnEnable()
    {
        LevelManager.OnLevelWon += LevelFinished;
        LevelManager.OnLevelLost += LevelFinished;
    }

    void Start()
    {
        back = movingStick.localPosition;
        forward = back - new Vector3(.3f, 0, 0);
    }

    void Update()
    {
        if (!isMoving)
        {
            CheckMove();
        }
    }
    
    private void OnDisable()
    {
        LevelManager.OnLevelWon -= LevelFinished;
        LevelManager.OnLevelLost -= LevelFinished;
    }
    
    private void LevelFinished(Character character)
    {
        StopAllCoroutines();
        isMoving = true;
    }

    private void LevelFinished()
    {
        StopAllCoroutines();
        isMoving = true;
    }

    private void CheckMove()
    {
        timeSinceLastMove += Time.deltaTime;

        switch (isForward)
        {
            case true when timeSinceLastMove > 1:
            case false when timeSinceLastMove > 2:
                StartCoroutine(MoveStick());
                break;
        }
    }

    private IEnumerator MoveStick()
    {
        isMoving = true;
        timeSinceLastMove = 0;
        var currentPos = movingStick.localPosition;
        var timeToMove = isForward ? 1 : .4f;
        var position = isForward ? back : forward;
        var t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            movingStick.localPosition = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }

        isForward = !isForward;
        isMoving = false;
    }
}