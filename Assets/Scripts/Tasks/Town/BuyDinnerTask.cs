using UnityEngine;
using System.Collections;

public class BuyDinnerTask : MonoBehaviour
{
    [Header("Task Settings")]
    [SerializeField] private float timeRequired = 10f;
    [SerializeField] private TownManager manager; 
    
    private bool isPlayerInZone = false;

    private void Start()
    {
        if (manager == null) manager = FindObjectOfType<TownManager>();
        if (manager == null) Debug.LogError("TownManager not found for BuyDinnerTask!");
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
            StopAllCoroutines(); 
            Debug.Log("Task interrupted: Player walked away from the vendor.");
        }
    }

    private IEnumerator PerformTask()
    {
        Debug.Log("Waiting for dinner... this line is long.");
        float timer = 0f;

        while (timer < timeRequired)
        {
            if (!isPlayerInZone) yield break;
            timer += Time.deltaTime;
            yield return null;
        }

        if (isPlayerInZone)
        {
            Debug.Log("Dinner acquired!");
            manager.TaskCompleted();
        }
    }
}