using System;
using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueSequenceController : MonoBehaviour
{
    public DialogueSequence dialogueSequence;
    
    public List<DialogueSequence>  afterMoreDialogueSequences = new List<DialogueSequence>();

    private Label dialogueText;
    
}