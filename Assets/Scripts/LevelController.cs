using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    protected internal bool IsCompleted = false;
    protected event Action OnLevelStarted;
    protected internal event Action OnLevelEnded;
    
    protected void InvokeOnLeveStarted()
    {
        IsCompleted = false;
        Debug.Log("OnLeveStarted");
        OnLevelStarted?.Invoke();
    }  
    
    protected void InvokeOnLevelEnded()
    {
        IsCompleted = true;
        Debug.Log("OnLevelEnded");
        OnLevelEnded?.Invoke();
    }
    
}
