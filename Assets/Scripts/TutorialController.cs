using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject backTutorialButton;
    public GameObject nextTutorialButton;
    protected internal bool IsCompleted = false;
    
    protected LevelController CurrentLevelController;
    protected event Action OnTutorialStarted;
    protected internal event Action OnTutorialEnded;

    private void Start()
    {
        CurrentLevelController = transform.GetChild(0).gameObject.GetComponent<LevelController>();
        CurrentLevelController.OnLevelEnded += HandleGoNext;
        //CurrentLevelController.OnGoBackInTutorial += () => UpdateCurrentLevelController(-1);
    }

    private void UpdateCurrentLevelController(int way)
    {
        CurrentLevelController.OnLevelEnded -= HandleGoNext;
        CurrentLevelController.OnGoBackInTutorial -= HandleGoBack;
        
        if (CurrentLevelController.transform.GetSiblingIndex() + way >= transform.childCount ||
            CurrentLevelController.transform.GetSiblingIndex() + way < 0) return;
        
        CurrentLevelController =  transform.GetChild(CurrentLevelController.transform.GetSiblingIndex() + way).gameObject
            .GetComponent<LevelController>();
        CurrentLevelController.OnLevelEnded += HandleGoNext;
        CurrentLevelController.OnGoBackInTutorial += HandleGoBack;
        
        Debug.Log("TUTORIAL: " + CurrentLevelController.transform.GetSiblingIndex());
        nextTutorialButton.SetActive(CurrentLevelController.transform.GetSiblingIndex() != 0);
        backTutorialButton.SetActive(CurrentLevelController.transform.GetSiblingIndex() > 1);
    }

    public void InvokeOnLeveStarted()
    {
        IsCompleted = false;
        Debug.Log("OnTutorialStarted");
        OnTutorialStarted?.Invoke();
    }  
    
    public void InvokeOnLevelEnded()
    {
        IsCompleted = true;
        Debug.Log("OnTutorialEnded");
        OnTutorialEnded?.Invoke();
    }

    public void ContinueToNextLevel()
    {
        // nextTutorialButton.SetActive(CurrentLevelController.transform.GetSiblingIndex() != 0);
        // backTutorialButton.SetActive(CurrentLevelController.transform.GetSiblingIndex() > 1);
        

        CurrentLevelController.InvokeOnLevelEnded();
    }

    public void GoBackInLevels()
    {
        //TODO 1- harcoded, because the start quiz is in 0 position
        // backTutorialButton.SetActive(CurrentLevelController.transform.GetSiblingIndex() > 1);
        // nextTutorialButton.SetActive(CurrentLevelController.transform.GetSiblingIndex() != 0);

        CurrentLevelController.InvokeGoBackInTutorial();
    }
    
    private void HandleGoNext() => UpdateCurrentLevelController(1);
    private void HandleGoBack() => UpdateCurrentLevelController(-1);
}
