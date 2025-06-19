using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueSequence", menuName = "Dialogue/Sequence")]
    public class DialogueSequence : ScriptableObject
    {
        public List<DialogueInfo> dialogueLines;
    }

    [System.Serializable]
    public struct DialogueInfo
    {
        public LocalizedString text;
        public AvatarEmoteType avatar;
    }
}