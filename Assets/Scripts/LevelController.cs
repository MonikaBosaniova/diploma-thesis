using System;
using DialogueSystem;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    protected internal bool IsCompleted = false;
    protected event Action OnLevelStarted;
    protected internal event Action OnLevelEnded;

    public virtual void Init()
    {
        DialogueSequenceController ds = transform.GetComponent<DialogueSequenceController>();
        if (ds != null)
        {
            UIManager.Instance.ShowDialogWindowUI(ds.dialogueSequence);
            Debug.Log("Show DIALOGUE");
        }
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
