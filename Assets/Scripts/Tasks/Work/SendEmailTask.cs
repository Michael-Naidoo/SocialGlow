using UnityEngine;
using System.Collections;
using TMPro;
public class SendEmailTask : MonoBehaviour
{
    [Header("Task Settings")]
    [SerializeField] private float timeRequired = 7f;
    
    // Reference to the work manager (Assign in Inspector or find at runtime)
    [SerializeField] private WorkManager manager; 
    
    private bool isPlayerInZone = false;

    public Renderer indicator;

    public TextMeshProUGUI countdown;
    
    

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
    private void Start()
    {
        countdown.text = "";
        if (manager == null) manager = FindObjectOfType<WorkManager>();
        if (manager == null) Debug.LogError("WorkManager not found for SendEmailTask!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeIndicatorColor(0);
            isPlayerInZone = true;
            StartCoroutine(PerformTask());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeIndicatorColor(1);
            isPlayerInZone = false;
            countdown.text = "";
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
            countdown.text = $"{timeRequired - timer}";
            if (!isPlayerInZone) yield break; // Check again inside loop
            timer += Time.deltaTime;
            // Update a UI progress bar here 

           //[Image of a UI progress bar]

            yield return null;
        }

        if (isPlayerInZone)
        {
            ChangeIndicatorColor(1);
            countdown.text = "";
            Debug.Log("Email sent successfully!");
            manager.TaskCompleted();
        }
    }
}