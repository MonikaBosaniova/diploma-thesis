using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
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
            Close.clicked += ReturnToMenu;
            //DialogueContainer.RegisterCallback<ClickEvent>(NextLine);
        }


        private void ShowResultOfQuestion(bool isCorrect, int hierarchyIndexOfButton)
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
            
            //TODO
            //show results prettier
            
            NextQuizQuestion();
        }
        private void ReturnToMenu()
        {
            //TODO return
            ResultBoard.style.display = DisplayStyle.None;
            SceneManager.LoadScene(0);
        }

        private void CorrectAnswerWasClicked(Button answerButton)
        {
            score += _currentQuizQuestion.value;
            Score.text = score.ToString();
        }

        private void IncorrectAnswerWasClicked(Button answerButton)
        {
            //TODO
            //show wrong answer prettier
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
            localizedStringQuestion.StringChanged += UpdateText;//(te(value) => Question.text = value);
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
                    answerButton.clicked += () => ShowResultOfQuestion(true, index);
                else
                    answerButton.clicked += () => ShowResultOfQuestion(false, index);
                
                localizedString.StringChanged += (value) => answerButton.text = value;
                localizedString.RefreshString();
            }
        }
        
        private void NextQuizQuestion()
        {
            Debug.Log("Quiz next");
            var previousLine = quizData.quizQuestions.ElementAt(_indexOfQuizQuestion).question;//dialogueSequence.dialogueLines.ElementAt(_currentIndex).text;
            previousLine.StringChanged -= UpdateText;
            
            _indexOfQuizQuestion++;
            if(_indexOfQuizQuestion != quizData.quizQuestions.Count)
                ShowAndBindCurrentQuizQuestion();
            else
            {
                ShowResultOfQuiz();
            }
        }

        private void ShowResultOfQuiz()
        {
            QuizParent.style.display = DisplayStyle.None;
            AnswersParent.Clear();
            
            ResultBoard.style.display = DisplayStyle.Flex;
            QuizResults.text = score + " / " + quizData.quizQuestions.Count;
        }

        void UpdateText(string value)  
        {  
            Question.text = value;
        }  
    }
}