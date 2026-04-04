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
        nextTutorialButton.SetActive(true);
        CurrentLevelController = transform.GetChild(0).gameObject.GetComponent<LevelController>();
        CurrentLevelController.OnLevelEnded += HandleGoNext;
        CurrentLevelController.OnGoBackInTutorial += () => UpdateCurrentLevelController(0);
    }

    private void UpdateCurrentLevelController(int way)
    {
        CurrentLevelController.OnLevelEnded -= HandleGoNext;
        CurrentLevelController.OnGoBackInTutorial -= HandleGoBack;
        
        Transform CurrentLevelControllerTransform = CurrentLevelController.transform;
        int CurrentLevelControllerIndex = CurrentLevelControllerTransform.GetSiblingIndex();
        
        if (CurrentLevelControllerIndex + way >= transform.childCount ||
            CurrentLevelControllerIndex + way < 0) return;
        
        CurrentLevelController =  transform.GetChild(CurrentLevelControllerIndex + way).gameObject
            .GetComponent<LevelController>();
        
        CurrentLevelControllerTransform = CurrentLevelController.transform;
        CurrentLevelControllerIndex = CurrentLevelControllerTransform.GetSiblingIndex();
        
        CurrentLevelController.OnLevelEnded += HandleGoNext;
        CurrentLevelController.OnGoBackInTutorial += HandleGoBack;
        
        Debug.Log("TUTORIAL: " + CurrentLevelControllerIndex);
        backTutorialButton.SetActive(CurrentLevelControllerIndex > 0);
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
        backTutorialButton.SetActive(CurrentLevelController.transform.GetSiblingIndex() != 0);

        CurrentLevelController.InvokeOnLevelEnded();
    }

    public void GoBackInLevels()
    {
        backTutorialButton.SetActive(CurrentLevelController.transform.GetSiblingIndex() > 0);

        CurrentLevelController.InvokeGoBackInTutorial();
    }
    
    private void HandleGoNext() => UpdateCurrentLevelController(1);
    private void HandleGoBack() => UpdateCurrentLevelController(-1);
}
