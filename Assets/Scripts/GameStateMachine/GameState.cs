using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStateMachine
{
    public abstract class GameState : MonoBehaviour
    {
        internal GameObject StateObject;
        internal Action OnStateComplete;
        internal Action OnStateStart;
        internal GameManager Manager;
        internal const float SuccessRateForStar = 0.8f;
        
        /// <summary>
        /// Called in initialization
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Init(GameObject obj)
        {
            StateObject = obj;
            Manager = FindAnyObjectByType<GameManager>();
        }
        
        /// <summary>
        /// Called when the state is entered
        /// </summary>
        public virtual void Enter()
        {
            Manager.StartTime = Time.time;
            StateObject.SetActive(true);
            OnStateStart?.Invoke();
        }
        
        /// <summary>
        /// Called when the state is exited
        /// </summary>
        public virtual void Exit()
        {
            Manager.time = Time.time - Manager.StartTime;
            SaveProgress();
            LogProgress(this.name);
            StateObject.SetActive(false);
        }
        
        /// <summary>
        /// Save progress to ProgressService
        /// Saving nodeID, stars, time
        /// </summary>
        protected void SaveProgress()
        {
            Debug.Log("SAVING: " + Manager.stars);
            if(ProgressService.I != null)
                ProgressService.I.RecordLevelResult(Manager.nodeID, Manager.stars, Manager.time);
            
        }
        
        /// <summary>
        /// Go to main menu, to phase when manu with levels are opened
        /// </summary>
        protected void GoToSkillTree()
        {
            if(ProgressService.I != null)
                ProgressService.I.OpenSkillTree = true;
            SceneManager.LoadScene(0);
        }
        
        /// <summary>
        /// Log progress in level, state and time what it takes to finish
        /// </summary>
        /// <param name="state"></param>
        protected void LogProgress(string state)
        {
            if(LoggerService.Instance != null)
                LoggerService.Instance.LogProgressInLevel(state, Manager.time);
        }
        
        public abstract void Update();
    }
}