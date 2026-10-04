using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;

    public Transform cameraTransform;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get camera directions
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Keep movement on the ground
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calculate movement direction
        Vector3 movement = forward * vertical + right * horizontal;

        // Choose walking or running speed
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        // Move Prince
        controller.Move(movement * speed * Time.deltaTime);

        // Turn Prince toward movement direction
        if (movement.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}