using System.Linq;
using DialogueSystem;
using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// Controls the dialogue system UI panel, displays dialogue lines sequentially with avatar emotes
    /// </summary>
    public class DialogueSystemUIController : DialogueSystemUIControllerContext
    {
        private int _currentIndex = 0;
        public DialogueSequence dialogueSequence;
        public AvatarVisualization avatarVisualizationConfig;

        /// <summary>
        /// Shows the dialogue window and initializes with the given dialogue sequence
        /// </summary>
        /// <param name="ds">Dialogue sequence data to display</param>
        public void ShowDialogue(DialogueSequence ds)
        {
            gameObject.SetActive(true);
            dialogueSequence = ds;
            base.Initialize();
        }

        /// <summary>
        /// Hides the dialogue window, clears text and resets the line index
        /// </summary>
        public void HideDialogue()
        {
            dialogueSequence = null;
            DialogueLabel.text = "";
            _currentIndex = 0;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Binds the click event on the dialogue container and shows the first dialogue line
        /// </summary>
        protected override void Bind()
        {
            _currentIndex = 0;
            ShowCurrentLine();
            DialogueContainer.RegisterCallback<ClickEvent>(NextLine);
        }

        /// <summary>
        /// Displays the current dialogue line text and avatar emote, hides when all lines are shown
        /// </summary>
        private void ShowCurrentLine()
        {
            if (_currentIndex >= dialogueSequence.dialogueLines.Count)
            {
                HideDialogue();
                return;
            }
            
            var localizedString = dialogueSequence.dialogueLines.ElementAt(_currentIndex).text;
            var avatarEmoteType = dialogueSequence.dialogueLines.ElementAt(_currentIndex).avatar;
            AvatarImage.style.backgroundImage =
                avatarVisualizationConfig.emotes.FirstOrDefault(e => e.emoteType == avatarEmoteType).emoteSprite;
            localizedString.StringChanged += UpdateText;
            localizedString.RefreshString(); // trigger load
        }

        /// <summary>
        /// Updates the dialogue label text when the localized string changes
        /// </summary>
        /// <param name="value">Localized text value</param>
        private void UpdateText(string value)
        {
            if(DialogueLabel != null)
                DialogueLabel.text = value;
        }

        /// <summary>
        /// Advances to the next dialogue line, unsubscribes from previous localized string
        /// </summary>
        /// <param name="evt">Click event data</param>
        private void NextLine(ClickEvent evt)
        {
            var previousLine = dialogueSequence.dialogueLines.ElementAt(_currentIndex).text;
            previousLine.StringChanged -= UpdateText;

            _currentIndex++;
            ShowCurrentLine();
        }
    }
}

