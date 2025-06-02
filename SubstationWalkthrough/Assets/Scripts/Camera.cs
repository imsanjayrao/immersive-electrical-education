using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;        // Reference to the player
    public float sensitivity = 2f;  // Mouse sensitivity
    public float distance = 3f;     // Distance from the player
    public float minYAngle = -90f, maxYAngle = 90f; // Allow full vertical movement

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Get mouse input even if the target is stationary
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);

        // Compute rotation from yaw & pitch
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Calculate position based on rotation and distance
        Vector3 position = target.position - rotation * Vector3.forward * distance;

        // Apply rotation and position
        transform.position = position;
        transform.rotation = rotation;
    }
}