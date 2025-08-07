using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract  class QuizUIControllerContext : UIControllerBaseContext
    {
        protected Label Question => _Root.Q<Label>("Question");
        protected VisualElement AnswersParent => _Root.Q<VisualElement>("Answers");
        protected VisualElement QuizParent => _Root.Q<VisualElement>("QuizParent");
        protected Label Score => _Root.Q<Label>("Score");
        protected VisualElement ResultBoard => _Root.Q<VisualElement>("Result");
        protected Label QuizResults => ResultBoard.Q<Label>("Quiz");
        
        protected Button Close => _Root.Q<Button>("Close");
    }
}