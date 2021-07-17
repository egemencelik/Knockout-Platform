using UnityEngine;

public class Opponent : Character
{
    protected override void LevelWon()
    {
        SetState(CharacterState.Lost);
        GetComponent<AIMovement>().StopAllCoroutines();
        enabled = false;
        movement.SetActive(false);
    }

    protected override void LevelLost(Character character)
    {
        SetState(character != this ? CharacterState.Lost : CharacterState.Won);
        GetComponent<AIMovement>().StopAllCoroutines();
        movement.SetActive(false);
        enabled = false;
    }

    public override void OnFall()
    {
        transform.position = startPosition;
        SetState(CharacterState.Running);
    }
}