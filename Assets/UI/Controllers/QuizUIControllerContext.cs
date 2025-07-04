using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract  class QuizUIControllerContext : UIControllerBaseContext
    {
        protected Label Question => _Root.Q<Label>("Question");
        protected VisualElement AnswersParent => _Root.Q<VisualElement>("Answers");
        protected Label Score => _Root.Q<Label>("Score");
    }
}