using UnityEngine;
using UnityEngine.SceneManagement;

// 1. Define the possible states for the game loop
public enum GameState
{
    // Start/End of the day
    InRoom_Start,
    InRoom_End,
    
    // Core loop phases
    SocialMediaPhase,
    WorkPhase_Movement,
    WorkPhase_Dialogue,
    TownPhase_Movement,
    TownPhase_Dialogue,

    // Win/Loss condition
    GameOver
}

public class GameManager : MonoBehaviour
{
    // --- Singleton Pattern ---
    public static GameManager Instance { get; private set; }

    // --- Core Stats (Visible in Inspector for easy balancing) ---
    [Header("Player Status")]
    [SerializeField] private float socialStatus = 50f;
    [SerializeField] private float professionalStatus = 50f;
    [SerializeField] private float minStatusToLose = 10f; // The threshold for game over
    
    // --- Game State Tracking ---
    [Header("Game Flow")]
    [SerializeField] private GameState currentState = GameState.InRoom_Start;
    [SerializeField] private int currentDay = 1;
    public bool workDialogueComplete;

    // Public getters for other scripts
    public float SocialStatus => socialStatus;
    public float ProfessionalStatus => professionalStatus;
    public GameState CurrentState => currentState;
    
    private SocialMediaPanel socialMediaPanel; // Add this private variable at the top
    private ComputerTrigger computerTrigger; // Add this private variable at the top


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
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Start the first day!
        UpdateGameState(GameState.InRoom_Start);
    }

    // --- State Management ---
    public void UpdateGameState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"Game State Updated to: {newState}");

        // Handle logic specific to state changes
        switch (newState)
        {
            case GameState.InRoom_Start:
                HandleInRoomStart();
                break;
            case GameState.SocialMediaPhase:
                HandleSocialMediaPhase();
                break;
            case GameState.WorkPhase_Movement:
                HandleWorkMovement();
                break;
            case GameState.WorkPhase_Dialogue:
                break;
            case GameState.TownPhase_Movement:
                HandleTownMovement();
                break;
            case GameState.TownPhase_Dialogue:
                break;
            case GameState.InRoom_End:
                HandleInRoomEnd();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            // Add other case handling here as you build out the game!
        }
    }

    // --- State-Specific Handlers ---

    private void HandleInRoomStart()
    {
        Debug.Log($"Day {currentDay} begins!");
        if (computerTrigger == null)
        {
            computerTrigger = FindObjectOfType<ComputerTrigger>();
        }

        if (computerTrigger != null)
        {
            computerTrigger.ResetTrigger();
        }
        // e.g., Show UI prompt to go to the computer
        // UI.ShowPrompt("Time to check the 'Glow' feed!");
    }

    private void HandleSocialMediaPhase()
    {
        if (socialMediaPanel == null)
        {
            // Simple way to find the panel if it's always in the scene
            socialMediaPanel = FindObjectOfType<SocialMediaPanel>(true);
        }

        if (socialMediaPanel != null)
        {
            socialMediaPanel.DisplayDailyPosts();
        }
        else
        {
            Debug.LogError("SocialMediaPanel not found in scene!");
        }
    }

    public void StartWorkDay()
    {
        // Called when the player clicks the 'Go to Work' trigger.
        UpdateGameState(GameState.WorkPhase_Movement);
        // e.g., Load the Work Scene
        // SceneLoader.LoadWorkScene();
    }
    
    private void HandleWorkMovement()
    {
        // In this state, the player can use point-and-click movement.
        // The ClickToMove script should check if (GameManager.Instance.CurrentState == GameState.WorkPhase_Movement)
    }
    private void HandleTownMovement()
    {
        workDialogueComplete = true;
        Debug.Log(workDialogueComplete);
        // In this state, the player can use point-and-click movement.
        // The ClickToMove script should check if (GameManager.Instance.CurrentState == GameState.TownPhase_Movement)
    }

    private void HandleInRoomEnd()
    {
        workDialogueComplete = false;
        currentDay++;
        // Check for loss condition
        if (socialStatus < minStatusToLose || professionalStatus < minStatusToLose)
        {
            UpdateGameState(GameState.GameOver);
        }
        else
        {
            // Successfully finished the day, restart the loop.
            UpdateGameState(GameState.InRoom_Start);
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over! Your status fell too low.");
        SceneManager.LoadScene("GameOver");
        // e.g., Load the Game Over screen
        // SceneLoader.LoadGameOverScreen();
    }
    
    // --- Stat Manipulation (The core satirical mechanic) ---

    /// <summary>
    /// Updates the player's status based on a conversation choice.
    /// </summary>
    /// <param name="socialChange">Amount to change Social Status by (+/-).</param>
    /// <param name="profChange">Amount to change Professional Status by (+/-).</param>
    public void ApplyChoiceEffect(float socialChange, float profChange)
    {
        socialStatus += socialChange;
        professionalStatus += profChange;
        
        // Clamp the values to ensure they stay between 0 and 100
        socialStatus = Mathf.Clamp(socialStatus, 0, 100);
        professionalStatus = Mathf.Clamp(professionalStatus, 0, 100);
        
        Debug.Log($"Choice Applied: Social Status: {socialStatus}, Professional Status: {professionalStatus}");

        // Immediately check if this choice resulted in a loss
        if (socialStatus < minStatusToLose || professionalStatus < minStatusToLose)
        {
            UpdateGameState(GameState.GameOver);
        }
    }

    /// <summary>
    /// Simulates the outcome of a 'Neutral' choice with a chance for both positive, negative, or no change.
    /// </summary>
    public void ApplyNeutralChoice()
    {
        int outcome = Random.Range(0, 3); // 0, 1, or 2
        float change = 5f; // Small, neutral-style change amount

        if (outcome == 0) // Positive outcome
        {
            ApplyChoiceEffect(change, change);
            Debug.Log("Neutral choice resulted in a slight positive bump to both.");
        }
        else if (outcome == 1) // Negative outcome
        {
            ApplyChoiceEffect(-change, -change);
            Debug.Log("Neutral choice resulted in a slight negative hit to both.");
        }
        else // outcome == 2 - No Change
        {
            Debug.Log("Neutral choice resulted in no change.");
        }
    }
}