using System.Collections.Generic;
using GameStateMachine;
using UnityEngine;

namespace Games.Ram
{
    public class CoolingGameManager : GameManager
    {
        //[SerializeField] public List<GameObject> allPossibleGeneratedShapes;
        [SerializeField] private GameObject sliders;

        protected override void Initialization()
        {
            base.Initialization();
            var tutorialState = tutorialParent.GetComponent<TutorialState>();
            var minigameState = minigameParent.GetComponent<MinigameState>();
            var quizState = quizParent.GetComponent<QuizState>();

            tutorialState.OnStateStart += () =>
            {
                if (sliders != null) sliders.SetActive(false);
            };

            minigameState.OnStateStart += () =>
            {
                if (sliders != null) sliders.SetActive(true);
            };

            quizState.OnStateStart += () =>
            {
                if (sliders != null) sliders.SetActive(false);
            };
        }
    }
}