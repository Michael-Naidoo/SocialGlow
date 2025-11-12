using UnityEngine;
using System;

// Use [Serializable] so Unity can display this data structure in the Inspector
[Serializable]
public struct StatEffect
{
    public float socialChange;        // Amount to add/subtract from Social Status
    public float professionalChange;  // Amount to add/subtract from Professional Status
    public bool isNeutralChoice;     // If true, the GameManager's Neutral logic will apply
}

[Serializable]
public class ConversationChoice
{
    [TextArea(1, 3)]
    public string choiceText;        // The button text the player sees
    public StatEffect effect;        // The immediate or calculated effect of choosing this option
    [TextArea(1, 3)]
    public string followUpComment;   // What the player says to the NPC after the choice
}