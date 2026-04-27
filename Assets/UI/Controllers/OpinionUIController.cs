using GameStateMachine;

namespace UI
{
    /// <summary>
    /// Controls the opinion survey UI shown after completing a minigame, collects player feedback, used in testing phase
    /// not in game anymore
    /// </summary>
    public class OpinionUIController : OpinionUIControllerContext
    {
        /// <summary>
        /// Displays the opinion menu and initializes UI bindings
        /// </summary>
        public void ShowOpinionMenu()
        {
            gameObject.SetActive(true);
            base.Initialize();
        }

        /// <summary>
        /// Binds the close button to submit feedback and proceed to quiz
        /// </summary>
        protected override void Bind()
        {
            Close.clicked += SentAnDGoToQuiz;
        }
        
        /// <summary>
        /// Logs the opinion slider values and triggers the transition to the quiz state
        /// </summary>
        private void SentAnDGoToQuiz()
        {
            LogOpinion(enjoySlider.value, tutorialSlider.value, knowledgeSlider.value);
            FindAnyObjectByType<MinigameState>().OnStateComplete?.Invoke();
        }

        /// <summary>
        /// Logs the opinion survey values via the LoggerService
        /// </summary>
        /// <param name="enjoy">Enjoyment rating value</param>
        /// <param name="tutorial">Tutorial helpfulness rating value</param>
        /// <param name="knowledge">Knowledge gained rating value</param>
        protected void LogOpinion(int enjoy, int tutorial, int knowledge)
        {
            if(LoggerService.Instance != null)
                LoggerService.Instance.LogOpinionAboutMinigame(enjoy, tutorial, knowledge);
        }
    }
}