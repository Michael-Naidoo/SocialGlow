using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDayData", menuName = "SocialGlow/WorkDayData")]
public class WorkDayData : ScriptableObject
{
    [Header("Day Setup")]
    public int dayNumber;
    public string dailyTheme; // e.g., "Conflict over New Corp Policy"

    [Header("AI Social Posts (3-4 per day)")]
    // References to the AISocialPost Scriptable Objects that define the daily context
    public List<AISocialPost> dailyPosts = new List<AISocialPost>();

    [Header("Work Phase - NPC Dialogue and Choices")]
    public string workNPCName;
    [TextArea(3, 5)]
    public string npcOpeningDialogue; // What the NPC says before the first choice
    
    // The series of choices the player makes during the conversation
    public List<ConversationChoice> choiceOptions = new List<ConversationChoice>();
    // NOTE: If you need a sequence of choices, you would likely nest lists or 
    // create a separate DialogueTree Scriptable Object, but this list handles the 
    // current requirement of a series of 2 choices.
    
    [Header("Town Phase - NPC Dialogue")]
    public string townNPCName;
    [TextArea(3, 5)]
    public string townNPCDialogue;
    
    public List<ConversationChoice> townChoiceOptions = new List<ConversationChoice>();
    
    // Placeholder for side task data (e.g., read billboard text, vendor dialogue)
    [Header("Side Task Data")]
    public string billboardText;
    public string vendorDialogue;
}