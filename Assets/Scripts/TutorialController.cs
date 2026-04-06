using System;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject backTutorialButton;
    public GameObject nextTutorialButton;
    protected internal bool IsCompleted = false;
    
    private LevelController _currentLevelController;
    protected event Action OnTutorialStarted;
    protected internal event Action OnTutorialEnded;

    private void Start()
    {
        nextTutorialButton.SetActive(true);
        _currentLevelController = transform.GetChild(0).gameObject.GetComponent<LevelController>();
        _currentLevelController.OnLevelEnded += HandleGoNext;
        _currentLevelController.OnGoBackInTutorial += () => UpdateCurrentLevelController(0);
    }
    
    /// <summary>
    /// Updates actions to new LevelController, to check next and back actions
    /// </summary>
    /// <param name="way">1 = go next, -1 = go back</param>
    private void UpdateCurrentLevelController(int way)
    {
        _currentLevelController.OnLevelEnded -= HandleGoNext;
        _currentLevelController.OnGoBackInTutorial -= HandleGoBack;
        
        Transform currentLevelControllerTransform = _currentLevelController.transform;
        int currentLevelControllerIndex = currentLevelControllerTransform.GetSiblingIndex();
        
        if (currentLevelControllerIndex + way >= transform.childCount ||
            currentLevelControllerIndex + way < 0) return;
        
        _currentLevelController =  transform.GetChild(currentLevelControllerIndex + way).gameObject
            .GetComponent<LevelController>();
        
        currentLevelControllerTransform = _currentLevelController.transform;
        currentLevelControllerIndex = currentLevelControllerTransform.GetSiblingIndex();
        
        _currentLevelController.OnLevelEnded += HandleGoNext;
        _currentLevelController.OnGoBackInTutorial += HandleGoBack;
        
        backTutorialButton.SetActive(currentLevelControllerIndex > 0);
        
        Debug.Log("TUTORIAL: " + currentLevelControllerIndex);
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
    
    /// <summary>
    /// Called by next button, to move forward in presentation
    /// </summary>
    public void ContinueToNextLevel()
    {
        backTutorialButton.SetActive(_currentLevelController.transform.GetSiblingIndex() != 0);

        _currentLevelController.InvokeOnLevelEnded();
    }
    
    /// <summary>
    /// Called by back button, to move backwards in presentation
    /// </summary>
    public void GoBackInLevels()
    {
        backTutorialButton.SetActive(_currentLevelController.transform.GetSiblingIndex() > 0);

        _currentLevelController.InvokeGoBackInTutorial();
    }
    
    private void HandleGoNext() => UpdateCurrentLevelController(1);
    private void HandleGoBack() => UpdateCurrentLevelController(-1);
}
