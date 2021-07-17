using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    private CameraFollow cameraFollow;

    private void Awake()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<Character>();
        if (character)
        {
            if (character is Opponent)
            {
                character.SetState(CharacterState.Won);
                LevelManager.Instance.LevelLost(character);
            }
            else
            {
                cameraFollow.Change();
                character.SetState(CharacterState.Painting);
            }
        }
    }
}