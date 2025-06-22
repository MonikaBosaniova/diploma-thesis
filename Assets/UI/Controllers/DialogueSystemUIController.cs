using System;
using System.Linq;
using DialogueSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class DialogueSystemUIController : DialogueSystemUIControllerContext
    {
        private int _currentIndex = 0;
        public DialogueSequence dialogueSequence;
        public AvatarVisualization avatarVisualizationConfig;

        // private void OnEnable()
        // {
        //     
        // }

        public void ShowDialogue(DialogueSequence ds)
        {
            gameObject.SetActive(true);
            dialogueSequence = ds;
            base.Initialize();
        }

        public void HideDialogue()
        {
            dialogueSequence = null;
            DialogueLabel.text = "";
            _currentIndex = 0;
            gameObject.SetActive(false);
        }

        protected override void Bind()
        {
            Debug.Log("DialogueUI bind");
            _currentIndex = 0;
            ShowCurrentLine();
            DialogueContainer.RegisterCallback<ClickEvent>(NextLine);
        }

        private void ShowCurrentLine()
        {
            if (_currentIndex >= dialogueSequence.dialogueLines.Count)
            {
                //DialogueLabel.text = "End of dialogue.";
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

        private void UpdateText(string value)
        {
            DialogueLabel.text = value;
        }

        private void NextLine(ClickEvent evt)
        {
            var previousLine = dialogueSequence.dialogueLines.ElementAt(_currentIndex).text;
            previousLine.StringChanged -= UpdateText;

            _currentIndex++;
            ShowCurrentLine();
        }
    }
}

