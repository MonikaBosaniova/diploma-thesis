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
        if (ds != null && ds.GetType() != typeof(EndDialogueSequenceController))
        {
            UIManager.Instance.ShowDialogWindowUI(ds.dialogueSequence);
            //Debug.Log("Show DIALOGUE");
        }
    }

    public virtual void Close()
    {
        
    }
    
    protected void InvokeOnLeveStarted()
    {
        IsCompleted = false;
        //Debug.Log("OnLeveStarted");
        OnLevelStarted?.Invoke();
    }  
    
    protected void ShowEndDialogueSequence()
    {
        IsCompleted = true;
        EndDialogueSequenceController es = transform.GetComponent<EndDialogueSequenceController>();
        if (es != null)
        {
            UIManager.Instance.ShowDialogWindowUI(es.dialogueSequence);
        }
    }

    protected void HideEndDialogueSequence()
    {
        IsCompleted = true;
        EndDialogueSequenceController es = transform.GetComponent<EndDialogueSequenceController>();
        if (es != null)
        {
            UIManager.Instance.HideDialogWindowUI();
        }
        OnLevelEnded?.Invoke();
    }
    
}
