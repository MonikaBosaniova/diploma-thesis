using System;
using System.Collections.Generic;
using System.Linq;
using Gates;
using UnityEngine;

namespace GameStateMachine
{
    public class QuizState : GameState
    {
        
        protected QuizData QuizData;
        protected GameObject QuizParent;
        private int _currentTutorialIndex;
        
        public override void Init(GameObject o)
        {
            QuizParent = o;
            stateObject = o;
            QuizData = QuizParent.GetComponent<QuizController>().quizData;
        }
        
        public override void Enter()
        {
            base.Enter();
            foreach (var tutorialStep in QuizData.quizQuestions)
            {
                //tutorialStep.OnLevelEnded += ContinueToNextQuiz;
            }
            //Tutorials.ElementAt(0).Init();
        }

        public override void Exit()
        {
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
