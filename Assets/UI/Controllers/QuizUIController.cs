using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameStateMachine;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UI
{
    public class QuizUIController : QuizUIControllerContext
    {
        [SerializeField] protected GameObject backButton;
        public bool showCorrectAnswer;
        public bool quizBefore;
        public bool showResults;
        public QuizData quizData;
        public float _successRate = 0;
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
            //Debug.Log("Quiz bind");
            _indexOfQuizQuestion = 0;
            ShowAndBindCurrentQuizQuestion();
            Close.clicked += ReturnToMenu;
            //DialogueContainer.RegisterCallback<ClickEvent>(NextLine);
        }


        private void ShowResultOfQuestion(bool isCorrect, int hierarchyIndexOfButton, int answerIndex)
        {
            //Debug.Log(AnswersParent.childCount + " children...");
            var answerButton = AnswersParent.ElementAt(hierarchyIndexOfButton).Q<Button>();
            ScreenBlocker.style.display = DisplayStyle.Flex;
            if (isCorrect)
            {
                if(showCorrectAnswer)
                    answerButton.style.unityBackgroundImageTintColor = Color.green;
                CorrectAnswerWasClicked(answerButton, answerIndex);
            }
            else
            {
                if(showCorrectAnswer)
                    answerButton.style.unityBackgroundImageTintColor = Color.red;
                IncorrectAnswerWasClicked(answerButton, answerIndex);
            }
            
            //TODO
            //show results prettier
            
            StartCoroutine(WaitToShowQuizAnswer());
        }
        private void ReturnToMenu()
        {
            //ResultBoard.style.display = DisplayStyle.None;
            FindAnyObjectByType<QuizState>().OnStateComplete?.Invoke();
        }

        private void CorrectAnswerWasClicked(Button answerButton, int answerIndex)
        {
            score += _currentQuizQuestion.value;
            Score.text = score.ToString();
            
            LogQuizAnswer(true, answerIndex);
        }

        private void IncorrectAnswerWasClicked(Button answerButton, int answerIndex)
        {
            //TODO
            //show wrong answer prettier
            LogQuizAnswer(false, answerIndex);
        }

        private void LogQuizAnswer(bool isCorrect, int answer)
        {
            if (LoggerService.Instance != null)
            {
                LoggerService.Instance.LogQuizAnswer(quizBefore, _indexOfQuizQuestion, isCorrect, answer);
            }
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
                    answerButton.clicked += () => ShowResultOfQuestion(true, index, 0);
                else
                {
                    if(randomizedAnswers.ElementAt(i) == _currentQuizQuestion.answers.ElementAt(1))
                        answerButton.clicked += () => ShowResultOfQuestion(false, index, 1);
                    else
                    {
                        answerButton.clicked += () => ShowResultOfQuestion(false, index, 2);
                    }
                }
                
                localizedString.StringChanged += (value) => answerButton.text = value;
                localizedString.RefreshString();
            }
        }
        
        private void NextQuizQuestion()
        {
            //Debug.Log("Quiz next");
            var previousLine = quizData.quizQuestions.ElementAt(_indexOfQuizQuestion).question;//dialogueSequence.dialogueLines.ElementAt(_currentIndex).text;
            previousLine.StringChanged -= UpdateText;
            ScreenBlocker.style.display = DisplayStyle.None;
            
            _indexOfQuizQuestion++;
            if(_indexOfQuizQuestion != quizData.quizQuestions.Count)
                ShowAndBindCurrentQuizQuestion();
            else
            {
                if(showResults)
                    ShowResultOfQuiz();
                else
                {
                    TutorialController tc = transform.parent.GetComponent<TutorialController>();
                    tc?.ContinueToNextLevel();
                }
            }
        }

        private void ShowResultOfQuiz()
        {
            backButton.SetActive(false);
            QuizParent.style.display = DisplayStyle.None;
            AnswersParent.Clear();
            
            ResultBoard.style.display = DisplayStyle.Flex;
            QuizResults.text = score + " / " + quizData.quizQuestions.Count;
            _successRate = score / (float)quizData.quizQuestions.Count;
        }

        void UpdateText(string value)  
        {  
            Question.text = value;
        }  
        
        IEnumerator WaitToShowQuizAnswer()
        {
            yield return new WaitForSeconds(.33f);
            NextQuizQuestion();
        }
    }
}