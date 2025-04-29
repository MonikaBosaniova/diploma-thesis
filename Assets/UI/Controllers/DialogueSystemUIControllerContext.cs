using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract  class DialogueSystemUIControllerContext : UIControllerBaseContext
    {
        protected Label DialogueLabel = _Root.Q<Label>("DialogueText");
    }
}