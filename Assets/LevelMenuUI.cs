using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject menuGO;

    [SerializeField]
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        LevelManager.OnLevelWon += LevelWon;
        LevelManager.OnLevelLost += LevelLost;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelWon -= LevelWon;
        LevelManager.OnLevelLost -= LevelLost;
    }

    private void LevelWon()
    {
        text.text = "You won!";
        menuGO.SetActive(true);
    }

    private void LevelLost(Character character)
    {
        text.text = "You lost!";
        menuGO.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
