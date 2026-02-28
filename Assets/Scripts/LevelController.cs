using System;
using System.Collections;
using DG.Tweening;
using DialogueSystem;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private float cameraXPos = 0;
    private float cameraZPos = 0;
    protected internal bool IsCompleted = false;
    protected event Action OnLevelStarted;
    protected internal event Action OnLevelEnded;
    protected internal event Action OnGoBackInTutorial;

    public virtual void Init()
    {
        cameraXPos = transform.position.x;
        cameraZPos = transform.position.z;
        Transform camera = Camera.main.transform;
        
        if (camera != null)
        {
                camera.DOMove(new Vector3(cameraXPos,camera.position.y, cameraZPos), 0.8f).OnComplete(SetupDialogueSequence);
        }
        else
        {
            SetupDialogueSequence();
        }
    }

    public virtual void Close()
    {
        
    }
    
    internal void InvokeOnLevelStarted()
    {
        IsCompleted = false;
        OnLevelStarted?.Invoke();
    }  
    
    internal void InvokeOnLevelEnded()
    {
        IsCompleted = true;
        OnLevelEnded?.Invoke();
    }  
    
    internal void InvokeGoBackInTutorial()
    {
        IsCompleted = true;
        OnGoBackInTutorial?.Invoke();
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
    
     protected IEnumerator WaitToShowCompleteLevel()
    {
        ShowEndDialogueSequence();
        yield return new WaitForSeconds(1.5f);
        HideEndDialogueSequence();
    }

    private void SetupDialogueSequence()
    {
        DialogueSequenceController ds = transform.GetComponent<DialogueSequenceController>();
        if (ds != null && ds.GetType() != typeof(EndDialogueSequenceController))
        {
            UIManager.Instance.ShowDialogWindowUI(ds.dialogueSequence);
        }
    }
    
}
