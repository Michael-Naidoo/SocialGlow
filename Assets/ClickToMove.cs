using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    // Required Components
    private Rigidbody rb;

    // The Input Action Reference for the mouse position value (Assign in Inspector)
    [SerializeField]
    private InputActionReference mousePositionAction;

    // The name of the Input Action that triggers the click (e.g., "ClickToMove")
    [SerializeField]
    private string moveClickActionName = "ClickToMove"; 

    // LayerMask to filter what the raycast can hit (e.g., only the Ground, Assign in Inspector)
    [SerializeField]
    private LayerMask groundLayer;
    
    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 5f; // Player movement speed

    private Vector3 targetPosition; // The world point the player is moving towards

    // --- Initialization ---
    private void Awake()
    {
        // Get the required component
        rb = GetComponent<Rigidbody>();

        // Ensure the Rigidbody is present and configured for movement
        if (rb == null)
        {
            Debug.LogError("ClickToMove requires a Rigidbody component on the same GameObject.");
        }
        
        // Ensure gravity is disabled as we don't want physics forces pulling the player down
        rb.useGravity = false;
        
        // Initialize targetPosition to a value that signifies no movement is currently needed
        targetPosition = transform.position;
    }

    // --- Physics Movement Loop ---
    private void FixedUpdate()
    {
        // Check if we are close enough to the target position
        if (Vector3.Distance(rb.position, targetPosition) > 0.1f)
        {
            // Calculate the direction towards the target
            Vector3 direction = (targetPosition - rb.position).normalized;
            
            // Calculate the movement step
            Vector3 moveStep = direction * moveSpeed * Time.fixedDeltaTime;
            
            // Apply the movement using Rigidbody.MovePosition()
            // This is crucial for maintaining physics interaction reliability!
            rb.MovePosition(rb.position + moveStep);
            
            // Optional: You might want to rotate the player to face the target here
            // Quaternion lookRotation = Quaternion.LookRotation(direction);
            // rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, Time.fixedDeltaTime * 10f);
        }
        else
        {
            // Stop movement precisely when the target is reached
            rb.linearVelocity = Vector3.zero;
        }
    }

    // --- Input System Subscriptions (Remain the same) ---
    private void OnEnable()
    {
        if (TryGetComponent<PlayerInput>(out var playerInput))
        {
            InputAction moveAction = playerInput.actions.FindAction(moveClickActionName);
            if (moveAction != null)
            {
                moveAction.performed += MoveToDestination;
            }
        }
    }

    private void OnDisable()
    {
        if (TryGetComponent<PlayerInput>(out var playerInput))
        {
            InputAction moveAction = playerInput.actions.FindAction(moveClickActionName);
            if (moveAction != null)
            {
                moveAction.performed -= MoveToDestination;
            }
        }
    }

    // --- Core Movement Logic ---
    private void MoveToDestination(InputAction.CallbackContext context)
    {
        // Check if the current GameState allows for player movement
        if (GameManager.Instance.CurrentState != GameState.InRoom_Start && 
            GameManager.Instance.CurrentState != GameState.WorkPhase_Movement && 
            GameManager.Instance.CurrentState != GameState.TownPhase_Movement)
        {
            Debug.Log("Movement is disabled in the current state: " + GameManager.Instance.CurrentState);
            return;
        }
        
        // 1. Get the current mouse position (Vector2 screen coordinates)
        Vector2 mousePosition = mousePositionAction.action.ReadValue<Vector2>();

        // 2. Create a ray from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // 3. Perform the raycast, checking against the ground layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            // 4. Set the new target position
            targetPosition = hit.point;
            Debug.Log($"Clicked: {hit.point}. Moving.");
        }
    }
}