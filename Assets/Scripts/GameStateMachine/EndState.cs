using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStateMachine
{
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
