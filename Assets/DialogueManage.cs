using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    // --- UI References ---
    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;
    public GameObject choiceButtonPrefab;
    public Transform choiceButtonsParent; // Horizontal Layout Group, usually

    // --- State Tracking ---
    private WorkDayData currentDayData;
    private int currentChoiceIndex = 0;
    private List<ConversationChoice> currentChoices;

    // --- Initialization and Setup ---
    public void StartDialogue()
    {
        // 1. Fetch the data for the current day
        currentDayData = FindObjectOfType<DayManager>().GetCurrentDayData();
        if (currentDayData == null)
        {
            Debug.LogError("Failed to load current day data for dialogue.");
            return;
        }

        // 2. Set the initial state
        GameManager.Instance.UpdateGameState(GameState.WorkPhase_Dialogue);
        dialoguePanel.SetActive(true);
        currentChoiceIndex = 0;
        
        // 3. Display the opening dialogue
        npcNameText.text = currentDayData.workNPCName;
        dialogueText.text = currentDayData.npcOpeningDialogue;
        
        // Use all choice options defined in the WorkDayData
        currentChoices = currentDayData.choiceOptions;
        
        // 4. Show the first set of choices
        DisplayChoices(currentChoiceIndex);
    }

    // --- Choice Presentation ---
    private void DisplayChoices(int index)
    {
        // Clear old buttons
        foreach (Transform child in choiceButtonsParent)
        {
            Destroy(child.gameObject);
        }

        // The current index defines the START of the choice set. 
        // We'll assume choices are presented in pairs (index and index + 1).
        if (index >= currentChoices.Count)
        {
            EndDialogue();
            return;
        }
        
        // Only display 2 choices per phase (index and index + 1)
        for (int i = index; i < index + 2 && i < currentChoices.Count; i++)
        {
            ConversationChoice choice = currentChoices[i];
            
            // Instantiate the button from prefab
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceButtonsParent);
            
            // Set the button text
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

            // Add a listener to handle the click event, passing the choice data
            Button button = buttonObj.GetComponent<Button>();
            int choiceMadeIndex = i; // Local copy for closure
            button.onClick.AddListener(() => OnChoiceSelected(choiceMadeIndex));
        }
    }

    // --- Player Choice Logic ---
    private void OnChoiceSelected(int choiceIndex)
    {
        ConversationChoice choice = currentChoices[choiceIndex];

        // 1. Apply the stat effect using the GameManager
        if (choice.effect.isNeutralChoice)
        {
            GameManager.Instance.ApplyNeutralChoice();
        }
        else
        {
            GameManager.Instance.ApplyChoiceEffect(
                choice.effect.socialChange,
                choice.effect.professionalChange
            );
        }

        // 2. Display the player's follow-up comment and hide buttons temporarily
        dialogueText.text = $"**You:** {choice.followUpComment}\n\n[NPC response here]"; 
        
        // Temporarily clear buttons after a choice
        foreach (Transform child in choiceButtonsParent)
        {
            Destroy(child.gameObject);
        }

        // 3. Wait briefly, then advance to the next set of choices
        currentChoiceIndex += 2; // Move past the pair of choices just presented
        Invoke("AdvanceConversation", 2f); // Simple delay for player to read response
    }
    
    private void AdvanceConversation()
    {
        // For simplicity, we just display the next set of choices immediately.
        // In a real game, you would display an NPC response here before showing new buttons.
        DisplayChoices(currentChoiceIndex);
    }

    // --- End Dialogue ---
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue finished. Transitioning to Town Phase.");

        // Transition the game to the next state
        GameManager.Instance.UpdateGameState(GameState.TownPhase_Movement);
    }
}