using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;

    public float distance = 6f;
    public float height = 3f;

    public float mouseSensitivity = 3f;

    private float rotationX = 15f;
    private float rotationY = 0f;

    void LateUpdate()
    {
        if (player == null)
            return;

        // Mouse movement
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Limit up/down rotation
        rotationX = Mathf.Clamp(rotationX, -20f, 60f);

        // Calculate camera rotation
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // Position camera around player
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

        transform.position = player.position + Vector3.up * height + offset;

        // Look at player
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}