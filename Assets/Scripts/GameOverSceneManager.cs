using System;
using UnityEngine;
public class GameOverSceneManager : MonoBehaviour
{
    public static GameOverSceneManager Instance { get; private set; }
    
    //stats that carry across to the game over scene
    public float socialStatus;
    public float professionalStatus;

    private void Awake()
    {
        // Enforce Singleton: only one instance can exist
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // Keep the GameManager across scene loads
            DontDestroyOnLoad(this);
        }
    }
}