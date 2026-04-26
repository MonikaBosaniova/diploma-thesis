using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStateMachine
{
    /// <summary>
    /// Final state of the game state machine, transitions the player back to the skill tree
    /// </summary>
    public class EndState : GameState
    {
        public override void Init(GameObject o)
        {
            base.Init(o);
            StateObject = o;
        }

        public override void Enter()
        {
            base.Enter();
            GoToSkillTree();
        }

        public override void Update()
        {
        }
        
    }
}
