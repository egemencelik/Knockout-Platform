using System;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public Vector3 Rotation;

    private bool rotates;

    private void OnEnable()
    {
        LevelManager.OnLevelWon += LevelFinished;
        LevelManager.OnLevelLost += LevelFinished;
    }

    private void Start()
    {
        rotates = true;
    }

    void Update()
    {
        if (!rotates) return;
        
        transform.Rotate(Rotation * Time.deltaTime);
    }

    private void OnDisable()
    {
        LevelManager.OnLevelWon -= LevelFinished;
        LevelManager.OnLevelLost -= LevelFinished;
    }
    
    private void LevelFinished()
    {
        rotates = false;
    }

    private void LevelFinished(Character character)
    {
        rotates = false;
    }
}