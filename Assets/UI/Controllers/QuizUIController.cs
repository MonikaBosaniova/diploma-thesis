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


        private void ShowResultOfQuestion(bool isCorrect, int hierarchyIndexOfButton, int answerIndex, int correctAnswerIndex)
        {
            //Debug.Log(AnswersParent.childCount + " children...");
            var answerButton = AnswersParent.ElementAt(hierarchyIndexOfButton).Q<Button>();
            var  correctButton = AnswersParent.ElementAt(correctAnswerIndex).Q<Button>();
            
            ScreenBlocker.style.display = DisplayStyle.Flex;
            if (isCorrect)
            {
                if(showCorrectAnswer)
                    answerButton.style.unityBackgroundImageTintColor = Color.green;
                CorrectAnswerWasClicked(answerIndex);
            }
            else
            {
                if (showCorrectAnswer)
                {
                    answerButton.style.unityBackgroundImageTintColor = Color.red;
                    correctButton.style.unityBackgroundImageTintColor = Color.green;
                }
                IncorrectAnswerWasClicked(answerIndex);
            }
            
            StartCoroutine(WaitToShowQuizAnswer());
        }
        private void ReturnToMenu()
        {
            FindAnyObjectByType<QuizState>().OnStateComplete?.Invoke();
        }

        private void CorrectAnswerWasClicked(int answerIndex)
        {
            score += _currentQuizQuestion.value;
            Score.text = score.ToString();
            
            LogQuizAnswer(true, answerIndex);
        }

        private void IncorrectAnswerWasClicked(int answerIndex)
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
                answer.style.flexGrow = 1;
                AnswersParent.Add(answer);
                var localizedString = randomizedAnswers.ElementAt(i);
                var answerButton = answer.Q<Button>();
                var index = i;

                var correctIndex = 0;

                for (int j = 0; j < randomizedAnswers.Count; j++)
                {
                    if (randomizedAnswers.ElementAt(j) == _currentQuizQuestion.answers.ElementAt(0))
                    {
                        correctIndex = j;
                    }
                }
                
                if(randomizedAnswers.ElementAt(i) == _currentQuizQuestion.answers.ElementAt(0))
                    answerButton.clicked += () => ShowResultOfQuestion(true, index, 0,  correctIndex);
                else
                {
                    if(randomizedAnswers.ElementAt(i) == _currentQuizQuestion.answers.ElementAt(1))
                        answerButton.clicked += () => ShowResultOfQuestion(false, index, 1,  correctIndex);
                    else
                    {
                        answerButton.clicked += () => ShowResultOfQuestion(false, index, 2, correctIndex);
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
            var timeToWait = quizBefore ? 0.33f : 2f;
            yield return new WaitForSeconds(timeToWait);
            NextQuizQuestion();
        }
    }
}