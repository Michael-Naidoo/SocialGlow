using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextManager : MonoBehaviour
{
    public bool talking;
    public static TextManager Instance { get; private set; }

    [SerializeField] private List<TextFile> textFiles;
    public TextFile currentTextFile;

    public DialogManager dialogManager;

    private void Awake()
    {
        // Check if an instance already exists.
        if (Instance != null && Instance != this)
        {
            // If another instance exists, destroy this one to enforce the singleton.
            Debug.Log("dis is gebroken");
            Destroy(this.gameObject);
        }
        else
        {
            // Set this instance as the singleton.
            Instance = this;
            Debug.Log(gameObject.name);
        }
    }

    private void Start()
    {
        dialogManager = DialogManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentTextFile = textFiles[Random.Range(0, textFiles.Count)];
        talking = true;
        dialogManager.NPC = other.gameObject;
        dialogManager.StartDialog();
    }
}
