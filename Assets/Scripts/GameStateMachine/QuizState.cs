using UI;
using UnityEngine;

namespace GameStateMachine
{
    public class QuizState : GameState
    {
        
        protected QuizData QuizData;
        protected GameObject QuizParent;
        private int _currentTutorialIndex;
        private QuizUIController _quizUIController;
        
        public override void Init(GameObject o)
        {
            base.Init(o);
            QuizParent = o;
            stateObject = o;
            _quizUIController = QuizParent.GetComponent<QuizUIController>();
            QuizData = _quizUIController.quizData;
        }
        
        public override void Enter()
        {
            base.Enter();
            _quizUIController.ShowQuiz(QuizData);
        }

        public override void Exit()
        {
            if (_quizUIController._successRate >= SuccessRateForStar)
            {
                manager.AddStar();
            }
            base.Exit();
        }

        public override void Update()
        {
        }

        protected void ContinueToNextQuiz()
        {
            // if (_currentTutorialIndex == Tutorials.Count - 1)
            // {
            //     Tutorials.ElementAt(_currentTutorialIndex).Close();
            //     _currentTutorialIndex = 0;
            //     OnStateComplete?.Invoke();
            // }
            // else
            // {
            //     Tutorials.ElementAt(_currentTutorialIndex).Close();
            //     if (_currentTutorialIndex + 1 >= Tutorials.Count) return;
            //     Tutorials.ElementAt(_currentTutorialIndex + 1).Init();
            //     _currentTutorialIndex++;
            // }
        }
    }
}
