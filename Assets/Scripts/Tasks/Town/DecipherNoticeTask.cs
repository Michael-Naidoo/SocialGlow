using UnityEngine;
using System.Collections;
using TMPro;

public class DecipherNoticeTask : MonoBehaviour
{
    [Header("Task Settings")]
    [SerializeField] private float decipherTime = 8f;
    [SerializeField] private TownManager manager;
    [SerializeField] private GameObject uiPanelPrefab; // UI Panel to display the text
    [SerializeField] private Transform canvasParent; // Parent transform for the UI
    
    private bool isPlayerDeciphering = false;

    private void Start()
    {
        if (manager == null) manager = FindObjectOfType<TownManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerDeciphering)
        {
            isPlayerDeciphering = true;
            GameObject panel = Instantiate(uiPanelPrefab, canvasParent); 
            
            // Simplified: Assume the panel has a TextMeshPro component
            TextMeshProUGUI text = panel.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = "Deciphering mandatory public compliance notice... stay engaged.'";
            }
            StartCoroutine(PerformTask(panel, text));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPlayerDeciphering)
        {
            StopAllCoroutines(); 
            isPlayerDeciphering = false;
            Debug.Log("Deciphering interrupted. The jargon overload was too much.");
        }
    }

    private IEnumerator PerformTask(GameObject panel, TextMeshProUGUI text)
    {
        Debug.Log("Deciphering mandatory public compliance notice... stay engaged.");
        
        // Optionally, display a quick UI panel with jargon text here
        
        yield return new WaitForSeconds(decipherTime);

        text.text = "Notice fully understood: 'Consume more, question less.' Task Complete.";

        yield return new WaitForSeconds(2f);

        if (isPlayerDeciphering)
        {
            Destroy(panel);
            isPlayerDeciphering = false;
            Debug.Log("Notice fully understood: 'Consume more, question less.' Task Complete.");
            manager.TaskCompleted();
        }
    }
}