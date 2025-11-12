using UnityEngine;

public abstract class JobManagerBase : MonoBehaviour
{
    // The number of tasks completed so far in this location
    private int jobsCompleted = 0;
    
    // The threshold to spawn the next NPC (Set in Inspector)
    [SerializeField] private int npcSpawnThreshold = 2; 
    
    // The NPC prefab to spawn
    [SerializeField] private GameObject npcPrefab;
    
    // The location where the NPC will spawn (Set in Inspector)
    [SerializeField] private Transform spawnPoint;

    /// <summary>
    /// Called by the mini-game script upon successful completion.
    /// </summary>
    public void TaskCompleted()
    {
        jobsCompleted++;
        Debug.Log($"Task completed in {gameObject.name}. Total: {jobsCompleted}/{npcSpawnThreshold}");

        if (jobsCompleted >= npcSpawnThreshold)
        {
            SpawnNPC();
        }
    }

    private void SpawnNPC()
    {
        if (npcPrefab != null && spawnPoint != null)
        {
            Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"NPC spawned at {spawnPoint.name}!");
            
            // Reset the counter and set a new threshold for the next spawn
            jobsCompleted = 0;
            // Optionally, increase the difficulty by raising the threshold here:
            // npcSpawnThreshold = Mathf.Min(10, npcSpawnThreshold + 1); 
        }
        else
        {
            Debug.LogError($"Cannot spawn NPC. Missing Prefab or Spawn Point on {gameObject.name}.");
        }
    }
}