using System;
using UnityEngine;

public class PrinterComponent : MonoBehaviour
{
    [SerializeField] private PrintDocumentTask computerTask; // Link back to the main task script

    private void OnEnable()
    {
        computerTask = FindFirstObjectByType<PrintDocumentTask>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered and the task is active
        if (other.CompareTag("Player") && computerTask.gameObject.activeInHierarchy) 
        {
            Debug.Log("Document collected successfully!");
            
            // Tell the main task script that the whole job is done
            if (computerTask != null)
            {
                computerTask.PrintingComplete();
            }
        }
    }
}