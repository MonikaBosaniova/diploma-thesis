using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// Context class providing UI element bindings for the dialogue system panel
    /// </summary>
    public abstract  class DialogueSystemUIControllerContext : UIControllerBaseContext
    {
        /// <summary>
        /// Reference to the dialogue text label element
        /// </summary>
        protected Label DialogueLabel => _Root.Q<Label>("DialogueText");
        /// <summary>
        /// Reference to the avatar image visual element
        /// </summary>
        protected VisualElement AvatarImage => _Root.Q<VisualElement>("AvatarImage");
        /// <summary>
        /// Reference to the clickable dialogue background container
        /// </summary>
        protected VisualElement DialogueContainer => _Root.Q<VisualElement>("DialogueBackground");
    }
}