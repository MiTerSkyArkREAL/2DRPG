using UnityEngine;

public class LeftRightPlatformMovementScript : MonoBehaviour
{
    public float moveSpeed = 2f;      // Controls how fast the platform oscillates
    public float moveDistance = 5f;   // How far left and right the platform moves
    private Vector3 startPos;         // Starting position of the platform

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Use sine wave to create smooth left/right motion
        float newX = startPos.x + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(newX, startPos.y, startPos.z);
    }
}
