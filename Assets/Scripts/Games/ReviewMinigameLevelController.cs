using System;
using GameStateMachine;
using UI;
using UnityEngine;

/// <summary>
/// Level controller for review-type minigame levels that auto-complete when level-specific conditions are met
/// </summary>
public class ReviewMinigameLevelController : LevelController
{
    public override void Init()
    {
        base.Init();
        
        gameObject.SetActive(true);
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        
        OpinionUIController qc = transform.GetChild(0).GetComponent<OpinionUIController>();
        qc.ShowOpinionMenu();
    }
    
    public override void Close()
    {
        gameObject.SetActive(false);
    }
    
    
    
    
}
