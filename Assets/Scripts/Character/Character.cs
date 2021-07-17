using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody rb;
    protected IMovement movement;
    public CharacterState currentState;

    protected Vector3 startPosition;

    protected static readonly int Running = Animator.StringToHash("Running");
    protected static readonly int Fall = Animator.StringToHash("Fall");
    protected static readonly int Won = Animator.StringToHash("Won");
    protected static readonly int Lost = Animator.StringToHash("Lost");
    protected static readonly int Stumble = Animator.StringToHash("Stumble");
    protected static readonly int Paint = Animator.StringToHash("Paint");

    #region Unity Callbacks

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<IMovement>();
    }

    private void OnEnable()
    {
        LevelManager.OnLevelWon += LevelWon;
        LevelManager.OnLevelLost += LevelLost;
    }

    private void Start()
    {
        SetState(CharacterState.Running);
        startPosition = transform.position;
        
    }

    private void FixedUpdate()
    {
        if (currentState == CharacterState.Running)
        {
            movement.Move();
        }
    }

    private void OnDisable()
    {
        LevelManager.OnLevelWon -= LevelWon;
        LevelManager.OnLevelLost -= LevelLost;
    }

    #endregion

    protected abstract void LevelWon();
    protected abstract void LevelLost(Character character);
    public abstract void OnFall();
    

    protected void StopMovement()
    {
        rb.Stop();
        movement.SetActive(false);
    }

    protected void StartMovement()
    {
        movement.SetActive(true);
    }

    public void StumbleTowards(Vector3 force)
    {
        SetState(CharacterState.Stumble);
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void SetState(CharacterState state)
    {
        currentState = state;
        switch (state)
        {
            case CharacterState.Running:
                StartMovement();
                animator.SetBool(Running, true);
                break;
            case CharacterState.Idle:
                StopMovement();
                animator.SetBool(Running, false);
                break;
            case CharacterState.Fallen:
                OnFall();
                break;
            case CharacterState.Painting:
                GetComponent<Painter>().enabled = true;
                StopMovement();
                animator.SetBool(Running, false);
                animator.SetTrigger(Paint);
                break;
            case CharacterState.Stumble:
                StopMovement();
                animator.SetTrigger(Stumble);
                break;
            case CharacterState.Lost:
                StopMovement();
                animator.SetBool(Running, false);
                animator.SetTrigger(Lost);
                break;
            case CharacterState.Won:
                StopMovement();
                animator.SetBool(Running, false);
                animator.SetTrigger(Won);
                break;
        }
    }
}