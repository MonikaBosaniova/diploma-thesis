using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStateMachine
{
    public abstract class GameState : MonoBehaviour
    {
        internal GameObject stateObject;
        internal Action OnStateComplete;
        internal Action OnStateStart;
        internal GameManager manager;
        internal const float SuccessRateForStar = 0.8f;
        
        public virtual void Init(GameObject obj)
        {
            stateObject = obj;
            manager = FindAnyObjectByType<GameManager>();
        }

        public virtual void Enter()
        {
            manager.StartTime = Time.time;
            stateObject.SetActive(true);
            OnStateStart?.Invoke();
        }
        
        public virtual void Exit()
        {
            manager.time = Time.time - manager.StartTime;
            SaveProgress();
            LogProgress(this.name);
            stateObject.SetActive(false);
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

        private void LogProgress(string state)
        {
            if(LoggerService.Instance != null)
                LoggerService.Instance.LogProgressInLevel(state, manager.time);
        }
        
        public abstract void Update();
    }
}