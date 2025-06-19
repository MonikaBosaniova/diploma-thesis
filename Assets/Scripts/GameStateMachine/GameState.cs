using System;
using UnityEngine;

namespace GameStateMachine
{
    public abstract class GameState : MonoBehaviour
    {
        internal GameObject stateObject;
        internal Action OnStateComplete;
        
        public GameState(GameObject obj)
        {
            stateObject = obj;
        }

        public virtual void Enter()
        {
            stateObject.SetActive(true);
            Debug.Log("Entered: " + stateObject.name);
            PrintChildren(stateObject);
        }
        
        public virtual void Exit()
        {
            stateObject.SetActive(false);
            Debug.Log("Exited: " + stateObject.name);
        }
        
        protected void PrintChildren(GameObject parent)
        {
            foreach (Transform child in parent.transform)
            {
                Debug.Log("Child: " + child.name);
            }
        }
        
        public abstract void Update();
    }
}