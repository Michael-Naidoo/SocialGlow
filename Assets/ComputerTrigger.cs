using UnityEngine;

public class ComputerTrigger : MonoBehaviour
{
    private bool hasInteracted = false;

    private void Start()
    {
        // Ensure the Computer has a Collider set as a Trigger
        if (GetComponent<Collider>() == null || !GetComponent<Collider>().isTrigger)
        {
            Debug.LogError("ComputerTrigger requires a Collider set as a Trigger!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider - " + other.name + " - has entered the computer zone.");
        // 1. Check if the colliding object is the player. 
        // We assume your player character has the tag "Player".
        if (other.CompareTag("Player") && !hasInteracted)
        {
            // 2. Check if the current state is the *expected* state for this action.
            // This prevents the player from accidentally triggering it during work or town time.
            if (GameManager.Instance.CurrentState == GameState.InRoom_Start)
            {
                Debug.Log("Player entered computer zone. Starting Social Media Phase.");
                
                // Set flag to true so the player doesn't accidentally re-trigger it
                hasInteracted = true; 

                // 3. Update the Game State to show the social media panel
                GameManager.Instance.UpdateGameState(GameState.SocialMediaPhase);
            }
            else
            {
                Debug.Log($"Can't use the computer now. Current State: {GameManager.Instance.CurrentState}");
            }
        }
    }

    /// <summary>
    /// Resets the trigger flag for the next day.
    /// This should be called by the GameManager during the start of a new day.
    /// </summary>
    public void ResetTrigger()
    {
        hasInteracted = false;
        Debug.Log("Computer trigger reset for the new day.");
    }
}