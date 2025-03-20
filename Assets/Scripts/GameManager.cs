using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    protected event Action OnGameStarted;
    protected event Action OnGameEnded;
    protected event Action OnIntroductionStarted;
    protected event Action OnIntroductionEnded;
    protected event Action OnMiniGameStarted;
    protected event Action OnMiniGameEnded;
    protected event Action OnQuizStarted;
    protected event Action OnQuizCompleted;

    protected List<LevelController> Levels;

    private int _currentLevelIndex;

    private void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        _currentLevelIndex = 0;
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
    
    protected void InvokeOnIntroductionStarted()
    {
        Debug.Log("OnIntroductionStarted");
        OnIntroductionStarted?.Invoke();
    } 
    
    protected void OInvokeOnIntroductionEnded()
    {
        Debug.Log("OnIntroductionEnded");
        OnIntroductionEnded?.Invoke();
    }

}
