using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    protected event Action OnGameStarted;
    protected event Action OnGameEnded;
    protected event Action OnTutorialStarted;
    protected event Action OnTutorialEnded;
    protected event Action OnMiniGameStarted;
    protected event Action OnMiniGameEnded;
    protected event Action OnQuizStarted;
    protected event Action OnQuizCompleted;

    [SerializeField] private GameObject TutorialParent;
    [SerializeField] private GameObject LevelsParent;
    
    [SerializeField] private bool skipTutorial;

    protected List<TutorialController> Tutorials;
    protected List<LevelController> Levels;

    private int _currentLevelIndex;
    private int _currentTutorialIndex;

    private void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        _currentLevelIndex = 0;
        if (!skipTutorial)
        {
            InvokeOnTutorialStarted();
            //Tutorials = FindObjectsByType<LevelController>()
        }
    }

    protected void ContinueToNextLevel()
    {
        if (_currentLevelIndex == Levels.Count - 1)
        {
            transform.GetChild(_currentLevelIndex).gameObject.SetActive(false);
            _currentLevelIndex = 0;
            InvokeOnMiniGameEnded();
        }
        else
        {
            transform.GetChild(_currentLevelIndex).gameObject.SetActive(false);
            if (_currentLevelIndex + 1 >= Levels.Count) return;
            transform.GetChild(_currentLevelIndex + 1).gameObject.SetActive(true);
            _currentLevelIndex++;
        }
    }
    
    protected void ContinueToNextTutorial()
    {
        if (_currentTutorialIndex == Tutorials.Count - 1)
        {
            transform.GetChild(_currentTutorialIndex).gameObject.SetActive(false);
            _currentTutorialIndex = 0;
            InvokeOnTutorialEnded();
        }
        else
        {
            transform.GetChild(_currentTutorialIndex).gameObject.SetActive(false);
            if (_currentTutorialIndex + 1 >= Tutorials.Count) return;
            transform.GetChild(_currentTutorialIndex + 1).gameObject.SetActive(true);
            _currentTutorialIndex++;
        }
    }

    protected void InvokeOnGameStarted()
    {
        Debug.Log("OnGameStarted");
        OnGameStarted?.Invoke();
    }    
    
    protected void InvokeOnGameEnded()
    {
        Debug.Log("OnGameEnded");
        OnGameEnded?.Invoke();
    }     
    
    protected void InvokeOnMiniGameStarted()
    {
        Debug.Log("OnMiniGameStarted");
        OnMiniGameStarted?.Invoke();
    }     
    
    protected void InvokeOnMiniGameEnded()
    {
        Debug.Log("OnMiniGameEnded");
        OnMiniGameEnded?.Invoke();
    }     
    
    protected void InvokeOnQuizStarted()
    {
        Debug.Log("OnQuizStarted");
        OnQuizStarted?.Invoke();
    }     
    
    protected void InvokeOnQuizCompleted()
    {
        Debug.Log("OnQuizCompleted");
        OnQuizCompleted?.Invoke();
    }
    
    protected void InvokeOnTutorialStarted()
    {
        Debug.Log("OnIntroductionStarted");
        OnTutorialStarted?.Invoke();
    } 
    
    protected void InvokeOnTutorialEnded()
    {
        Debug.Log("OnIntroductionEnded");
        OnTutorialEnded?.Invoke();
    }

}
