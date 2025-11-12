using UnityEngine;
using System.Collections;

public class SendEmailTask : MonoBehaviour
{
    [Header("Task Settings")]
    [SerializeField] private float timeRequired = 7f;
    
    // Reference to the work manager (Assign in Inspector or find at runtime)
    [SerializeField] private WorkManager manager; 
    
    private bool isPlayerInZone = false;

    private void Start()
    {
        if (manager == null) manager = FindObjectOfType<WorkManager>();
        if (manager == null) Debug.LogError("WorkManager not found for SendEmailTask!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            StartCoroutine(PerformTask());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            StopAllCoroutines(); // Stop if the player leaves early
            Debug.Log("Task interrupted: Player left the computer.");
        }
    }

    private IEnumerator PerformTask()
    {
        Debug.Log("Sending email... stay put!");
        float timer = 0f;

        while (timer < timeRequired)
        {
            if (!isPlayerInZone) yield break; // Check again inside loop
            timer += Time.deltaTime;
            // Update a UI progress bar here 

           //[Image of a UI progress bar]

            yield return null;
        }

        if (isPlayerInZone)
        {
            Debug.Log("Email sent successfully!");
            manager.TaskCompleted();
        }
    }
}