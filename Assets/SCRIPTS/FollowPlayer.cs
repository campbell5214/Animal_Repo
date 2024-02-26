using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Offset of the camera from the player

    void LateUpdate()
    {
        if (target != null)
        {
            // Set the position of the camera to the position of the player plus the offset
            transform.position = target.position + offset;
        }
    }
}
