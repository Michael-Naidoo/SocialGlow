using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnNPCs : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints;
    private GameObject newSpawnPoint;
    [SerializeField] private GameObject NPC_Prefab;
    public static SpawnNPCs Instance { get; private set; }
    private void Awake()
    {
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
            // Optionally, if you want this object to persist across scene loads.
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SpawnNewNPC()
    {
        int position = Random.Range(0, spawnPoints.Count);
        Instantiate(NPC_Prefab, spawnPoints[position].transform.position, quaternion.identity);
    }
}
