using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    protected internal bool IsCompleted = false;
    protected event Action OnTutorialStarted;
    protected internal event Action OnTutorialEnded;
    
    protected void InvokeOnLeveStarted()
    {
        IsCompleted = false;
        Debug.Log("OnTutorialStarted");
        OnTutorialStarted?.Invoke();
    }  
    
    protected void InvokeOnLevelEnded()
    {
        IsCompleted = true;
        Debug.Log("OnTutorialEnded");
        OnTutorialEnded?.Invoke();
    }
}
