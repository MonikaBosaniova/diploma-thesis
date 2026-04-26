using System.Collections.Generic;
using GameStateMachine;
using UnityEngine;

namespace Games.Ram
{
    /// <summary>
    /// Game manager for the RAM memory management minigame
    /// </summary>
    public class RamGameManager : GameManager
    {
        /// <summary>
        /// List of shapes that could be generated and after needed to be placed to RAM
        /// </summary>
        [SerializeField] public List<GameObject> allPossibleGeneratedShapes;
    }
}
