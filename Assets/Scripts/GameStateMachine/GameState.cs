using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStateMachine
{
    public abstract class GameState : MonoBehaviour
    {
        internal GameObject stateObject;
        internal Action OnStateComplete;
        internal GameManager manager;
        internal const float SuccessRateForStar = 0.8f;
        
        public virtual void Init(GameObject obj)
        {
            stateObject = obj;
            manager = FindAnyObjectByType<GameManager>();
        }

        public virtual void Enter()
        {
            stateObject.SetActive(true);
        }
        
        public virtual void Exit()
        {
            SaveProgress();
            stateObject.SetActive(false);
        }
        
        protected void PrintChildren(GameObject parent)
        {
            foreach (Transform child in parent.transform)
            {
                //Debug.Log("Child: " + child.name);
            }
        }
        
        protected void SaveProgress()
        {
            Debug.Log("SAVING: " + manager.stars);
            if(ProgressService.I != null)
                ProgressService.I.RecordLevelResult(manager.nodeID, manager.stars, manager.time);
        }

        protected void GoToSkillTree()
        {
            if(ProgressService.I != null)
                ProgressService.I.OpenSkillTree = true;
            SceneManager.LoadScene(0);
        }
        
        public abstract void Update();
    }
}