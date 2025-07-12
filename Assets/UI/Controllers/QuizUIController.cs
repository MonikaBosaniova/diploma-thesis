using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace UI
{
    public class QuizUIController : QuizUIControllerContext
    {
        public QuizData quizData;
        private QuizQuestion _currentQuizQuestion;
        private int _indexOfQuizQuestion = 0;
        private int score = 0;
        
        public void ShowQuiz(QuizData qd)
        {
            gameObject.SetActive(true);
            _indexOfQuizQuestion = 0;
            quizData = qd;
            base.Initialize();
        }

        // public void HideDialogue()
        // {
        //     dialogueSequence = null;
        //     DialogueLabel.text = "";
        //     _currentIndex = 0;
        //     gameObject.SetActive(false);
        // }

        protected override void Bind()
        {
            Debug.Log("Quiz bind");
            _indexOfQuizQuestion = 0;
            ShowAndBindCurrentQuizQuestion();
            //DialogueContainer.RegisterCallback<ClickEvent>(NextLine);
        }

        private void ShowResult(bool isCorrect, int hierarchyIndexOfButton)
        {
            Debug.Log(AnswersParent.childCount + " children...");
            var answerButton = AnswersParent.ElementAt(hierarchyIndexOfButton).Q<Button>();
            if (isCorrect)
            {
                answerButton.style.unityBackgroundImageTintColor = Color.green;
                CorrectAnswerWasClicked(answerButton);
            }
            else
            {
                answerButton.style.unityBackgroundImageTintColor = Color.red;
                IncorrectAnswerWasClicked(answerButton);
            }
            NextQuiz();
        }

        private void CorrectAnswerWasClicked(Button answerButton)
        {
            score += _currentQuizQuestion.value;
            Score.text = score.ToString();
        }

        private void IncorrectAnswerWasClicked(Button answerButton)
        {
            throw new NotImplementedException();
        }

        private void ShowAndBindCurrentQuizQuestion()
        {
            //Remove all buttons before
            if (AnswersParent.Children().Count() != 0)
            {
                while (AnswersParent.Children().Count() != 0)
                {
                    AnswersParent.RemoveAt(0);
                }
            }
            
            //Map current question
            _currentQuizQuestion = quizData.quizQuestions.ElementAt(_indexOfQuizQuestion);
            var localizedStringQuestion = _currentQuizQuestion.question;
            localizedStringQuestion.StringChanged += (value) => Question.text = value;
            localizedStringQuestion.RefreshString(); 
            
            List<LocalizedString> randomizedAnswers = _currentQuizQuestion.answers.OrderBy(x => Guid.NewGuid()).ToList();
            
            //Create new answer buttons
            for (int i = 0; i < _currentQuizQuestion.answers.Count; i++)
            {
                var answer = quizData.AnswerTemplate.Instantiate();
                AnswersParent.Add(answer);
                var localizedString = randomizedAnswers.ElementAt(i);
                var answerButton = answer.Q<Button>();
                var index = i;
                
                if(randomizedAnswers.ElementAt(i) == _currentQuizQuestion.answers.ElementAt(0))
                    answerButton.clicked += () => ShowResult(true, index);
                else
                    answerButton.clicked += () => ShowResult(false, index);
                
                localizedString.StringChanged += (value) => answerButton.text = value;
                localizedString.RefreshString();
            }
        }
        
        private void NextQuiz()
        {
            Debug.Log("Quiz next");
            // var previousLine = dialogueSequence.dialogueLines.ElementAt(_currentIndex).text;
            // previousLine.StringChanged -= UpdateText;
            //
            // _currentIndex++;
            // //ShowCurrentLine();
        }
    }
}

