using UnityEngine;

public class ShredDocumentTask : MonoBehaviour
{
    [Header("Task Settings")]
    [SerializeField] private GameObject shredderTrigger; // The Shredder GameObject

    private ShredderComponent shredderComponent;
    [SerializeField] private float timeAtShredder = 3f;
    
    [SerializeField] private WorkManager manager;
    
    private bool documentPrinted = false;

    public Renderer indicator;

    private void ChangeIndicatorColor(int color)
    {
        if (color == 0)
        {
            indicator.material.color = Color.red;
        }
        else
        {
            indicator.material.color = Color.green;
        }
    }
    private void OnEnable()
    {
        if (manager == null) manager = FindObjectOfType<WorkManager>();
        if (shredderTrigger == null) Debug.LogError("Shredder Trigger not assigned!");
        
        // Ensure the shredder starts inactive or hidden
        shredderTrigger.SetActive(false); 
    }

    // Step 1: Player enters the Printer trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !documentPrinted)
        {
            ChangeIndicatorColor(0);
            Debug.Log("Document printed. Go to the shredder!");
            documentPrinted = true;
            
            // Activate the shredder trigger for the next step
            shredderTrigger.SetActive(true); 
            // In a final game, you'd disable this printer trigger here, but we keep it simple.
        }
    }
    
    // Function called by a *separate* ShredderComponent on the shredderTrigger object
    public void ShreddingComplete()
    {
        ChangeIndicatorColor(1);
        documentPrinted = false; // Reset for the next day/task
        shredderTrigger.SetActive(false); // Hide the shredder trigger
        manager.TaskCompleted();
    }
}