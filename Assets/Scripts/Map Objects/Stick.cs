using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField, Range(0, 20)]
    private float pushForce;

    private void OnCollisionEnter(Collision other)
    {
        var character = other.collider.GetComponent<Character>();
        if (character)
        {
            var dir = other.transform.position - other.GetContact(0).point;
            dir.y = 0;
            character.StumbleTowards(dir * pushForce);
        }
    }
}