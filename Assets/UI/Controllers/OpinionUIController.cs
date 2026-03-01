using GameStateMachine;
using UnityEngine;

namespace UI
{
    public class OpinionUIController : OpinionUIControllerContext
    {
        public void ShowOpinionMenu()
        {
            gameObject.SetActive(true);
            base.Initialize();
        }

        protected override void Bind()
        {
            Close.clicked += SentAnDGoToQuiz;
        }
        
        private void SentAnDGoToQuiz()
        {
            LogOpinion(enjoySlider.value, tutorialSlider.value, knowledgeSlider.value);
            FindAnyObjectByType<MinigameState>().OnStateComplete?.Invoke();
        }

        protected void LogOpinion(int enjoy, int tutorial, int knowledge)
        {
            Debug.Log("OPINION: " +  enjoy + " - " + tutorial + " - " + knowledge);
            if(LoggerService.Instance != null)
                LoggerService.Instance.LogOpinionAboutMinigame(enjoy, tutorial, knowledge);
        }
        
    }
}