using System;
using System.Collections;
using DG.Tweening;
using DialogueSystem;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float cameraXPos = 0;
    protected internal bool IsCompleted = false;
    protected event Action OnLevelStarted;
    protected internal event Action OnLevelEnded;

    public virtual void Init()
    {
        Transform camera = Camera.main.transform;
        if (camera != null && !Mathf.Approximately(cameraXPos, camera.position.x))
        {
            if(transform.GetSiblingIndex() != 0)
                camera.DOMoveX(cameraXPos, 0.8f).OnComplete(SetupDialogueSequence);
            else
            {
                camera.position = new Vector3(0,10,0);
                SetupDialogueSequence();
            }
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
        //Debug.Log("OnLeveStarted");
        OnLevelStarted?.Invoke();
    }  
    
    internal void InvokeOnLevelEnded()
    {
        IsCompleted = true;
        //Debug.Log("OnLeveStarted");
        OnLevelEnded?.Invoke();
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
        yield return new WaitForSeconds(.5f);
        HideEndDialogueSequence();
    }

    private void SetupDialogueSequence()
    {
        DialogueSequenceController ds = transform.GetComponent<DialogueSequenceController>();
        if (ds != null && ds.GetType() != typeof(EndDialogueSequenceController))
        {
            UIManager.Instance.ShowDialogWindowUI(ds.dialogueSequence);
            //Debug.Log("Show DIALOGUE");
        }
    }
    
}
