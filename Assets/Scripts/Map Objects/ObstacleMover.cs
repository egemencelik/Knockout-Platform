using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField]
    private Transform start, target;

    [SerializeField]
    private float speed = 15f;

    private Vector3 currentTarget;
    private bool isMoving;

    private void OnEnable()
    {
        LevelManager.OnLevelWon += LevelFinished;
        LevelManager.OnLevelLost += LevelFinished;
    }

    void Start()
    {
        transform.position = start.position;
        currentTarget = target.position;
        isMoving = true;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, currentTarget) < .5f)
        {
            ReverseTarget();
        }

        if (isMoving)
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        LevelManager.OnLevelWon -= LevelFinished;
        LevelManager.OnLevelLost -= LevelFinished;
    }

    private void LevelFinished()
    {
        isMoving = false;
    }

    private void LevelFinished(Character character)
    {
        isMoving = false;
    }

    private void ReverseTarget()
    {
        currentTarget = currentTarget == start.position ? target.position : start.position;
    }
}