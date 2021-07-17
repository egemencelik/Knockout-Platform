using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var character = other.collider.GetComponent<Character>();
        if (character)
        {
            character.SetState(CharacterState.Fallen);
        }
    }
}