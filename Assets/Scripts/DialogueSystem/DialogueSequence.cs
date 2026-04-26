using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace DialogueSystem
{
    /// <summary>
    /// ScriptableObject containing a list of dialogue lines for a conversation sequence
    /// </summary>
    [CreateAssetMenu(fileName = "DialogueSequence", menuName = "Dialogue/Sequence")]
    public class DialogueSequence : ScriptableObject
    {
        public List<DialogueInfo> dialogueLines;
    }

    /// <summary>
    /// Represents a single dialogue line with localized text and avatar emote
    /// </summary>
    [System.Serializable]
    public struct DialogueInfo
    {
        public LocalizedString text;
        public AvatarEmoteType avatar;
    }
}