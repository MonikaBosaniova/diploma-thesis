using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "AvatarVisualization", menuName = "Dialogue/AvatarVisualization")]
    public class AvatarVisualization : ScriptableObject
    {
        public List<AvatarEmote> emotes;
    }

    [System.Serializable]
    public struct AvatarEmote
    {
        public AvatarEmoteType emoteType;
        public Texture2D emoteSprite;
    }
}
