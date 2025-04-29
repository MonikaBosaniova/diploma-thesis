using UnityEngine;

namespace UI
{
    public class DialogueSystemUIController : DialogueSystemUIControllerContext
    {
        private int currentIndex = 0;
        private DialogueSequence dialogueSequence;
        
        protected override void Bind()
        {
            Debug.Log("DialogueUI bind");
        }
        
        public void ShowCurrentLine()
        {
            if (currentIndex >= dialogueSequence.dialogueLines.Length)
            {
                DialogueLabel.text = "End of dialogue.";
                return;
            }

            var localizedString = dialogueSequence.dialogueLines[currentIndex];
            localizedString.StringChanged += UpdateText;
            localizedString.RefreshString(); // trigger load
        }

        private void UpdateText(string value)
        {
            DialogueLabel.text = value;
        }

        public void NextLine()
        {
            var previousLine = dialogueSequence.dialogueLines[currentIndex];
            previousLine.StringChanged -= UpdateText;

            currentIndex++;
            ShowCurrentLine();
        }
    }
}

