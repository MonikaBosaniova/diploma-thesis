using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    protected internal bool IsCompleted = false;
    protected event Action OnLevelStarted;
    protected internal event Action OnLevelEnded;

    public virtual void Init()
    {
        
    }

    public virtual void Close()
    {
        
    }
    
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
