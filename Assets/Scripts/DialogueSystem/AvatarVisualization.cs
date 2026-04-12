using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{   
    /// <summary>
    /// Defines the structure for storing the Avatar emotes data
    /// </summary>
    [CreateAssetMenu(fileName = "AvatarVisualization", menuName = "Dialogue/AvatarVisualization")]
    public class AvatarVisualization : ScriptableObject
    {
        public List<AvatarEmote> emotes;
    }
    
    /// <summary>
    /// Pair of the emoteType with the sprite, that represents the emotion
    /// </summary>
    [System.Serializable]
    public struct AvatarEmote
    {
        public AvatarEmoteType emoteType;
        public Texture2D emoteSprite;
    }
}
