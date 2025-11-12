using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // Assuming you are using TextMeshPro for UI Text

public class SocialMediaPanel : MonoBehaviour
{
    // --- UI References ---
    [Header("UI Elements")]
    public GameObject postPrefab;               // Prefab for a single social media post
    public Transform contentParent;             // The scroll view content panel
    public Button continueButton;               // Button to proceed to the Work Phase

    // --- References to the Day's Posts ---
    private List<AISocialPost> dailyPosts;

    private void Awake()
    {
        // Subscribe the continue button to transition the game state
        continueButton.onClick.AddListener(OnContinueClicked);
    }

    private void OnDestroy()
    {
        // Clean up the listener when the object is destroyed
        continueButton.onClick.RemoveListener(OnContinueClicked);
    }

    // --- Main Display Function ---
    public void DisplayDailyPosts()
    {
        // 1. Get the data from the DayManager
        WorkDayData dayData = FindObjectOfType<DayManager>().GetCurrentDayData();
        if (dayData == null)
        {
            Debug.LogError("No daily data loaded. Cannot display social media posts.");
            return;
        }

        dailyPosts = dayData.dailyPosts;

        // 2. Clear previous posts if any (for development/testing)
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 3. Instantiate and populate a UI element for each post
        foreach (AISocialPost post in dailyPosts)
        {
            GameObject postObject = Instantiate(postPrefab, contentParent);
            
            // Assuming the prefab has a child with a TextMeshProUGUI component
            // We need a helper script on the prefab to simplify this!
            // For now, we'll try to find the component directly:
            TextMeshProUGUI postText = postObject.GetComponentInChildren<TextMeshProUGUI>();
            if (postText != null)
            {
                // Format the post with its theme and content
                postText.text = $"**{post.themeTag.ToUpper()} ALERT**\n\n{post.postText}";
            }
            else
            {
                Debug.LogWarning("Post Prefab missing a TextMeshProUGUI component in children.");
            }
        }
        
        // Show the panel and the continue button
        gameObject.SetActive(true);
    }

    // --- Game State Transition ---
    private void OnContinueClicked()
    {
        // Hide the panel
        gameObject.SetActive(false);
        
        // Transition the game to the next state: Work Phase Movement
        GameManager.Instance.StartWorkDay(); 
    }
}