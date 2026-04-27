using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameStateMachine;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// Controls the quiz UI panel, handles question display, answer evaluation, scoring and result presentation
    /// </summary>
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
        private int _score = 0;
        
        /// <summary>
        /// Shows the quiz panel and initializes with the given quiz data
        /// </summary>
        /// <param name="qd">Quiz data containing questions and answer template</param>
        public void ShowQuiz(QuizData qd)
        {
            gameObject.SetActive(true);
            _indexOfQuizQuestion = 0;
            quizData = qd;
            base.Initialize();
        }

        /// <summary>
        /// Binds the first quiz question and the close button
        /// </summary>
        protected override void Bind()
        {
            _indexOfQuizQuestion = 0;
            ShowAndBindCurrentQuizQuestion();
            Close.clicked += ReturnToMenu;
        }


        /// <summary>
        /// Displays visual feedback for correct/incorrect answers and starts the delay coroutine
        /// </summary>
        /// <param name="isCorrect">Whether the selected answer is correct</param>
        /// <param name="hierarchyIndexOfButton">Index of the clicked button in the answers container</param>
        /// <param name="answerIndex">Original answer index (0 = correct)</param>
        /// <param name="correctAnswerIndex">Index of the correct answer button</param>
        private void ShowResultOfQuestion(bool isCorrect, int hierarchyIndexOfButton, int answerIndex, int correctAnswerIndex)
        {
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
        /// <summary>
        /// Triggers the quiz state completion to return to the menu
        /// </summary>
        private void ReturnToMenu()
        {
            FindAnyObjectByType<QuizState>().OnStateComplete?.Invoke();
        }

        /// <summary>
        /// Increments the score and logs a correct answer
        /// </summary>
        /// <param name="answerIndex">Index of the correct answer</param>
        private void CorrectAnswerWasClicked(int answerIndex)
        {
            _score += _currentQuizQuestion.value;
            Score.text = _score.ToString();
            
            LogQuizAnswer(true, answerIndex);
        }

        /// <summary>
        /// Logs an incorrect answer selection
        /// </summary>
        /// <param name="answerIndex">Index of the selected incorrect answer</param>
        private void IncorrectAnswerWasClicked(int answerIndex)
        {
            LogQuizAnswer(false, answerIndex);
        }

        /// <summary>
        /// Sends quiz answer data to the LoggerService for analytics
        /// </summary>
        /// <param name="isCorrect">Whether the answer was correct</param>
        /// <param name="answer">Index of the answer selected</param>
        private void LogQuizAnswer(bool isCorrect, int answer)
        {
            if (LoggerService.Instance != null)
            {
                LoggerService.Instance.LogQuizAnswer(quizBefore, _indexOfQuizQuestion, isCorrect, answer);
            }
        }

        /// <summary>
        /// Displays the current quiz question with randomized answer buttons and binds click events
        /// </summary>
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
            localizedStringQuestion.StringChanged += UpdateText;
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
        
        /// <summary>
        /// Advances to the next quiz question or shows results when all questions are answered
        /// </summary>
        private void NextQuizQuestion()
        {
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

        /// <summary>
        /// Displays the final quiz result board with score and success rate
        /// </summary>
        private void ShowResultOfQuiz()
        {
            backButton.SetActive(false);
            QuizParent.style.display = DisplayStyle.None;
            AnswersParent.Clear();
            
            ResultBoard.style.display = DisplayStyle.Flex;
            QuizResults.text = _score + " / " + quizData.quizQuestions.Count;
            _successRate = _score / (float)quizData.quizQuestions.Count;
        }

        /// <summary>
        /// Updates the question label text when the localized string changes
        /// </summary>
        /// <param name="value">Localized question text</param>
        void UpdateText(string value)  
        {  
            Question.text = value;
        }  
        
        /// <summary>
        /// Waits before advancing to the next question (shorter delay for pre-quiz)
        /// </summary>
        /// <returns>Coroutine that waits for the configured delay</returns>
        IEnumerator WaitToShowQuizAnswer()
        {
            var timeToWait = quizBefore ? 0.33f : 2f;
            yield return new WaitForSeconds(timeToWait);
            NextQuizQuestion();
        }
    }
}