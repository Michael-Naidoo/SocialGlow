using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    // Example Logic in an NPCInteraction.cs script (Work Scene)
    public void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            DialogueManager dm = FindObjectOfType<DialogueManager>(true);
            if (dm != null)
            {
                // Stop player movement
                // ClickToMove.StopMovement(); 
            
                // Start the conversation
                dm.StartDialogue();
            }
        }
    }
}
