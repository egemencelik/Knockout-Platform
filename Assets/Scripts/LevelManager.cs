using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public delegate void LevelWonAction();

    public delegate void LevelLostAction(Character character);

    public static event LevelWonAction OnLevelWon;
    public static event LevelLostAction OnLevelLost;

    private void Awake()
    {
        Instance = this;
    }

    public void LevelLost(Character character)
    {
        OnLevelLost?.Invoke(character);
    }

    public void LevelWon()
    {
        OnLevelWon?.Invoke();
    }
}