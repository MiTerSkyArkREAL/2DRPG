using UnityEngine;

public class PlatformMoveUpDownScript : MonoBehaviour
{
    public float moveSpeed = 2f;      // Controls how fast the platform oscillates
    public float moveDistance = 5f;   // How far up and down the platform moves
    private Vector3 startPos;         // Starting position of the platform

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Use sine wave to create smooth up/down motion
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
