using System;
using System.Collections;
using DG.Tweening;
using DialogueSystem;
using UnityEngine;

/// <summary>
/// Base class for controlling a single level (tutorial step or minigame level)
/// Handles camera movement, dialogue sequences, and level lifecycle events
/// </summary>
public class LevelController : MonoBehaviour
{
    private float cameraXPos = 0;
    private float cameraZPos = 0;
    protected internal bool IsCompleted = false;
    protected event Action OnLevelStarted;
    protected internal event Action OnLevelEnded;
    protected internal event Action OnGoBackInTutorial;

    /// <summary>
    /// Initializes the level, moves camera to level position and sets up dialogue
    /// </summary>
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

    /// <summary>
    /// Closes the level, override to clean up level-specific resources
    /// </summary>
    public virtual void Close()
    {
        
    }
    
    /// <summary>
    /// Invokes the OnLevelStarted event and resets the IsCompleted flag
    /// </summary>
    internal void InvokeOnLevelStarted()
    {
        IsCompleted = false;
        OnLevelStarted?.Invoke();
    }  
    
    /// <summary>
    /// Invokes the OnLevelEnded event and sets IsCompleted to true
    /// </summary>
    internal void InvokeOnLevelEnded()
    {
        IsCompleted = true;
        OnLevelEnded?.Invoke();
    }  
    
    /// <summary>
    /// Invokes the OnGoBackInTutorial event and sets IsCompleted to true
    /// </summary>
    internal void InvokeGoBackInTutorial()
    {
        IsCompleted = true;
        OnGoBackInTutorial?.Invoke();
    }  
    
    /// <summary>
    /// Shows the end dialogue sequence UI after level completion
    /// </summary>
    private void ShowEndDialogueSequence()
    {
        IsCompleted = true;
        EndDialogueSequenceController es = transform.GetComponent<EndDialogueSequenceController>();
        if (es != null)
        {
            UIManager.Instance.ShowDialogWindowUI(es.dialogueSequence);
        }
    }

    /// <summary>
    /// Hides the end dialogue sequence UI and invokes OnLevelEnded
    /// </summary>
    private void HideEndDialogueSequence()
    {
        IsCompleted = true;
        EndDialogueSequenceController es = transform.GetComponent<EndDialogueSequenceController>();
        if (es != null)
        {
            UIManager.Instance.HideDialogWindowUI();
        }
        OnLevelEnded?.Invoke();
    }
    
    /// <summary>
    /// Coroutine that shows end dialogue, waits, then hides it and completes the level
    /// </summary>
     protected IEnumerator WaitToShowCompleteLevel()
    {
        ShowEndDialogueSequence();
        yield return new WaitForSeconds(1.5f);
        HideEndDialogueSequence();
    }

    /// <summary>
    /// Finds and displays the DialogueSequence attached to this level (excluding EndDialogueSequence)
    /// </summary>
    private void SetupDialogueSequence()
    {
        DialogueSequenceController ds = transform.GetComponent<DialogueSequenceController>();
        if (ds != null && ds.GetType() != typeof(EndDialogueSequenceController))
        {
            UIManager.Instance.ShowDialogWindowUI(ds.dialogueSequence);
        }
    }
    
}
