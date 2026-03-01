using GameStateMachine;
using UI;
using UnityEngine;

public class StartQuizLevelController : LevelController
{
    internal float startQuizTime;
    internal float durationQuizTime;
    
    public override void Init()
    {
        startQuizTime = Time.time;
        gameObject.SetActive(true);
        
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     transform.GetChild(i).gameObject.SetActive(true);
        // }
        
        base.Init();
        
        QuizUIController qc = GetComponent<QuizUIController>();
        qc.ShowQuiz(qc.quizData);
    }
    
    public override void Close()
    {
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     transform.GetChild(i).gameObject.SetActive(false);
        // }
        
        //LOGGING BEFORE QUIZ TIME
        durationQuizTime = Time.time - startQuizTime;
        transform.parent.gameObject.GetComponent<TutorialState>().quizBeforeTime = durationQuizTime;
        LogProgress("QUIZBefore");
        gameObject.SetActive(false);
    }
    
    protected void LogProgress(string state)
    {
        if(LoggerService.Instance != null)
            LoggerService.Instance.LogProgressInLevel(state, durationQuizTime);
    }
    
    
}
