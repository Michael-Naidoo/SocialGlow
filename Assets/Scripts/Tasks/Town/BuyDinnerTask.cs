using UnityEngine;
using System.Collections;
using TMPro;

public class BuyDinnerTask : MonoBehaviour
{
    [Header("Task Settings")]
    [SerializeField] private float timeRequired = 10f;
    [SerializeField] private TownManager manager; 
    
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
        if (manager == null) manager = FindObjectOfType<TownManager>();
        if (manager == null) Debug.LogError("TownManager not found for BuyDinnerTask!");
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
            countdown.text = "";
            ChangeIndicatorColor(1);
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
            countdown.text = $"{timeRequired - timer}";
            if (!isPlayerInZone) yield break;
            timer += Time.deltaTime;
            yield return null;
        }

        if (isPlayerInZone)
        {
            countdown.text = "";
            ChangeIndicatorColor(1);
            Debug.Log("Dinner acquired!");
            manager.TaskCompleted();
        }
    }
}