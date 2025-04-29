using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "DialogueSequence", menuName = "Dialogue/Sequence")]
public class DialogueSequence : ScriptableObject
{
    public LocalizedString[] dialogueLines;
}