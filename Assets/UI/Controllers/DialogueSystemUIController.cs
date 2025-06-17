using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class DialogueSystemUIController : DialogueSystemUIControllerContext
    {
        private int _currentIndex = 0;
        public DialogueSequence dialogueSequence;

        private void Start()
        {
            base.Initialize();
        }

        protected override void Bind()
        {
            Debug.Log("DialogueUI bind");
            ShowCurrentLine();
            DialogueContainer.RegisterCallback<ClickEvent>(NextLine);
        }

        private void ShowCurrentLine()
        {
            if (_currentIndex >= dialogueSequence.dialogueLines.Length)
            {
                DialogueLabel.text = "End of dialogue.";
                return;
            }

            var localizedString = dialogueSequence.dialogueLines[_currentIndex];
            localizedString.StringChanged += UpdateText;
            localizedString.RefreshString(); // trigger load
        }

        private void UpdateText(string value)
        {
            DialogueLabel.text = value;
        }

        private void NextLine(ClickEvent evt)
        {
            var previousLine = dialogueSequence.dialogueLines[_currentIndex];
            previousLine.StringChanged -= UpdateText;

            _currentIndex++;
            ShowCurrentLine();
        }
    }
}

