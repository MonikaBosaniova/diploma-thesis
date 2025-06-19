using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract  class DialogueSystemUIControllerContext : UIControllerBaseContext
    {
        protected Label DialogueLabel => _Root.Q<Label>("DialogueText");
        protected VisualElement AvatarImage => _Root.Q<VisualElement>("AvatarImage");
        
        protected VisualElement DialogueContainer => _Root.Q<VisualElement>("DialogueBackground");
    }
}