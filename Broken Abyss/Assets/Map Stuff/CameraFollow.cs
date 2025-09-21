using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The sprite the camera follows
    public float smoothSpeed = 5f; // Higher values make the camera follow faster
    public Vector3 offset = new Vector3(0, 0, -10); // Default offset for 2D view

    void LateUpdate()
    {
        if(target != null)
        {
            // Define the target position with an offset
            Vector3 desiredPosition = target.position + offset;

            // Smoothly interpolate between current and target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Only update the x and y positions (ignores z for 2D setup)
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, offset.z);
        }
    }
}