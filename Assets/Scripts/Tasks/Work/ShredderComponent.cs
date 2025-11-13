using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class ShredderComponent : MonoBehaviour
{
    [SerializeField] private ShredDocumentTask printerTask; // Link back to the main task script
    [SerializeField] private float shredTime = 3f;
    
    private bool isShredding = false;

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
    private void OnEnable()
    {
        printerTask = FindObjectOfType<ShredDocumentTask>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShredding && printerTask.gameObject.activeInHierarchy) 
        {
            ChangeIndicatorColor(0);
            isShredding = true;
            StartCoroutine(ShredTask());
        }
    }

    private IEnumerator ShredTask()
    {
        Debug.Log("Shredding document... wait...");
        countdown.text = "Shredding document... wait...";
        yield return new WaitForSeconds(shredTime);
        
        Debug.Log("Document shredded!");
        countdown.text = "Document shredded!";

        yield return new WaitForSeconds(0.5f);
        
        isShredding = false;
        
        // Tell the main task script that the whole job is done
        if (printerTask != null)
        {
            ChangeIndicatorColor(1);
            printerTask.ShreddingComplete();
        }
    }
}