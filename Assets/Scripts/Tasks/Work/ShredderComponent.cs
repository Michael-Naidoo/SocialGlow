using System;
using UnityEngine;
using System.Collections;

public class ShredderComponent : MonoBehaviour
{
    [SerializeField] private ShredDocumentTask printerTask; // Link back to the main task script
    [SerializeField] private float shredTime = 3f;
    
    private bool isShredding = false;

    private void OnEnable()
    {
        printerTask = FindObjectOfType<ShredDocumentTask>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShredding && printerTask.gameObject.activeInHierarchy) 
        {
            isShredding = true;
            StartCoroutine(ShredTask());
        }
    }

    private IEnumerator ShredTask()
    {
        Debug.Log("Shredding document... wait...");
        yield return new WaitForSeconds(shredTime);
        
        Debug.Log("Document shredded!");
        isShredding = false;
        
        // Tell the main task script that the whole job is done
        if (printerTask != null)
        {
            printerTask.ShreddingComplete();
        }
    }
}