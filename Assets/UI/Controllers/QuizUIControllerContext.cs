using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// Context class providing UI element bindings for the quiz panel (question, answers, score, results)
    /// </summary>
    public abstract  class QuizUIControllerContext : UIControllerBaseContext
    {
        /// <summary>
        /// Label displaying the current quiz question text
        /// </summary>
        protected Label Question => _Root.Q<Label>("Question");
        /// <summary>
        /// Container for dynamically generated answer buttons
        /// </summary>
        protected VisualElement AnswersParent => _Root.Q<VisualElement>("Answers");
        /// <summary>
        /// Parent container for the quiz question panel
        /// </summary>
        protected VisualElement QuizParent => _Root.Q<VisualElement>("QuizParent");
        /// <summary>
        /// Label displaying the current score
        /// </summary>
        protected Label Score => _Root.Q<Label>("Score");
        /// <summary>
        /// Container for the final result board displayed after quiz completion
        /// </summary>
        protected VisualElement ResultBoard => _Root.Q<VisualElement>("Result");
        /// <summary>
        /// Label displaying the final quiz results (score / total)
        /// </summary>
        protected Label QuizResults => ResultBoard.Q<Label>("Quiz");
        /// <summary>
        /// Button to close the quiz and return to the menu
        /// </summary>
        protected Button Close => _Root.Q<Button>("Close");
        /// <summary>
        /// Overlay element that blocks clicks between answer feedback and next question
        /// </summary>
        protected VisualElement ScreenBlocker => _Root.Q<VisualElement>("ScreenBlocker");
    }
}