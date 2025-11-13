using UnityEngine;

public class PrintDocumentTask : MonoBehaviour
{
    [Header("Task Settings")]
    [SerializeField] private GameObject printerTrigger; // The Printer GameObject
    [SerializeField] private float preparationTime = 5f; // Time required at the computer
    
    [SerializeField] private WorkManager manager;
    [SerializeField] private PrinterComponent printer;

    private bool documentPrepared = false;

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
        
        if (printerTrigger == null) Debug.LogError("Printer Trigger not assigned!");
        
        // Ensure the printer starts inactive
        printerTrigger.SetActive(false); 
    }

    // Step 1: Player enters the Computer trigger to prepare
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !documentPrepared)
        {
            // Start preparation timer coroutine
            ChangeIndicatorColor(0);
            StartCoroutine(PrepareDocument(other.gameObject));
        }
    }

    private System.Collections.IEnumerator PrepareDocument(GameObject player)
    {
        Debug.Log("Preparing document for printing... stay put!");
        documentPrepared = true;
        
        // Simple delay for preparation
        yield return new WaitForSeconds(preparationTime);

        // Check if the player is still near enough (for robustness)
        if (documentPrepared)
        {
            Debug.Log("Document ready to print. Go collect it at the printer!");
            
            // Activate the next step's trigger
            printerTrigger.SetActive(true); 
        }
    }
    
    // Function called by the *separate* PrinterComponent when the document is collected
    public void PrintingComplete()
    {
        ChangeIndicatorColor(1);
        documentPrepared = false; // Reset for the next day/task
        printerTrigger.SetActive(false); // Hide the printer trigger
        manager.TaskCompleted();
    }
}