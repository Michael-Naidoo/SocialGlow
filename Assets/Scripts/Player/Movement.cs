using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Vector3 targetPosition;
    private bool moving;
    public float moveSpeed = 5f;
    public float stoppingDistance = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moving = false;
    }

    // This method is called by the Input System when the click action is performed.
    public void OnClickToMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Create a ray from the mouse position.
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // Perform the raycast to the scene.
            if (Physics.Raycast(ray, out hit))
            {
                // Set the target position to the point where the ray hit.
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                if (targetPosition.x > -40 && targetPosition.x < 45 && targetPosition.z > -22 && targetPosition.z < 28)
                {
                    moving = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (moving)
        {
            // Calculate the direction to the target.
            Vector3 direction = (targetPosition - rb.position).normalized;

            // Check if we are close to the target.
            if (Vector3.Distance(rb.position, targetPosition) < stoppingDistance)
            {
                moving = false;
                rb.linearVelocity = Vector3.zero; // Stop the rigidbody.
            }
            else
            {
                // Use MovePosition for smooth, physics-aware movement.
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
