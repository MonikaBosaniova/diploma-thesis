using System;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Holds dialogue sequence data for a level, including optional follow-up dialogues
/// </summary>
public class DialogueSequenceController : MonoBehaviour
{
    public DialogueSequence dialogueSequence;
    
    public List<DialogueSequence>  afterMoreDialogueSequences = new List<DialogueSequence>();

    private Label dialogueText;
    
}