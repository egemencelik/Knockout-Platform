using UnityEngine;

public class Player : Character
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void LevelWon()
    {
        SetState(CharacterState.Won);
    }

    protected override void LevelLost(Character character)
    {
        SetState(CharacterState.Lost);
    }

    public override void OnFall()
    {
        animator.SetBool(Running, false);
        animator.SetTrigger(Fall);
        StopMovement();
        rb.AddForce(0, 0, -5f, ForceMode.Impulse);
        LevelManager.Instance.LevelLost(null);
    }
}