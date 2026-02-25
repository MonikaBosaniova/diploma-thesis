using System;
using System.Collections.Generic;
using GameStateMachine;
using UnityEngine;

namespace Games.Ram
{
    public class CoolingGameManager : GameManager
    {
        //[SerializeField] public List<GameObject> allPossibleGeneratedShapes;

        // private void Awake()
        // {
        //     if(sliders != null) sliders.SetActive(false);
        // }

        // protected override void Initialization()
        // {
        //     base.Initialization();
        //     var tutorialState = tutorialParent.GetComponent<TutorialState>();
        //     var minigameState = minigameParent.GetComponent<MinigameState>();
        //     var quizState = quizParent.GetComponent<QuizState>();
        //
        //     tutorialState.OnStateStart += () =>
        //     {
        //         if (sliders != null) sliders.SetActive(false);
        //     };
        //
        //     minigameState.OnStateStart += () =>
        //     {
        //         if (sliders != null) sliders.SetActive(true);
        //     };
        //
        //     quizState.OnStateStart += () =>
        //     {
        //         if (sliders != null) sliders.SetActive(false);
        //     };
        // }
    }
}