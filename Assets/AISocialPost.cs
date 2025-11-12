using UnityEngine;

// This allows us to create new assets of this type via the Unity editor menu
[CreateAssetMenu(fileName = "NewPost", menuName = "SocialGlow/AISocialPost")]
public class AISocialPost : ScriptableObject
{
    [Header("Post Content")]
    public string postID;           // Unique identifier for easier tracking
    [TextArea(3, 10)]
    public string postText;         // The main body of the AI's social media post
    public string themeTag;         // e.g., "Corporate Loyalty", "Activism", "Neutrality"
    public string relatedTopic;     // e.g., "New Work Policy", "AI Ethics", "Product Launch"
}