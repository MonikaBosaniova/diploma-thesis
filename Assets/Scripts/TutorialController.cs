using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    protected internal bool IsCompleted = false;
    
    protected LevelController CurrentLevelController;
    protected event Action OnTutorialStarted;
    protected internal event Action OnTutorialEnded;

    private void Start()
    {
        CurrentLevelController = transform.GetChild(0).gameObject.GetComponent<LevelController>();
        CurrentLevelController.OnLevelEnded += UpdateCurrentLevelController;
    }

    private void UpdateCurrentLevelController()
    {
        if (CurrentLevelController.transform.GetSiblingIndex() + 1 >= transform.childCount) return;
        CurrentLevelController =  transform.GetChild(CurrentLevelController.transform.GetSiblingIndex() + 1).gameObject
            .GetComponent<LevelController>();
        CurrentLevelController.OnLevelEnded += UpdateCurrentLevelController;
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
        CurrentLevelController.InvokeOnLevelEnded();
    }
}
