using System;
using UnityEngine;
using TMPro; // Assuming you are using TextMeshPro for UI

public class ReadBillboardTask : MonoBehaviour
{
    [Header("Task Settings")]
    [SerializeField] private TownManager manager;
    [SerializeField] private GameObject uiPanelPrefab; // UI Panel to display the text
    [SerializeField] private Transform canvasParent; // Parent transform for the UI
    
    private bool hasReadToday = false;
    
    public Renderer indicator;

    private void OnEnable()
    {
        hasReadToday = false;
    }

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
        if (manager == null) manager = FindObjectOfType<TownManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasReadToday)
        {
            ChangeIndicatorColor(0);
            Debug.Log("Player is reading the billboard.");
            hasReadToday = true;
            
            // 1. Display the UI
            // In a complete game, you'd get the text from DayManager
            GameObject panel = Instantiate(uiPanelPrefab, canvasParent); 
            
            // Simplified: Assume the panel has a TextMeshPro component
            TextMeshProUGUI text = panel.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = "GlowNet AI Announcement: 'Civility Scores are up 3% this quarter. Keep glowing!'";
            }
            
            // 2. Add an automatic delay/button to close the panel and complete the task.
            StartCoroutine(ClosePanelAfterDelay(panel));
        }
    }
    
    private System.Collections.IEnumerator ClosePanelAfterDelay(GameObject panel)
    {
        yield return new WaitForSeconds(5f); // Player reads for 5 seconds
        ChangeIndicatorColor(1);
        Destroy(panel);
        manager.TaskCompleted();
    }
}