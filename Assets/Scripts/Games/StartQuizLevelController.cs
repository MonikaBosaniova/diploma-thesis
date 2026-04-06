using UI;
using UnityEngine;

/// <summary>
/// LevelController used to measure the pre-test in schools
/// Not used in the game now.
/// </summary>
public class StartQuizLevelController : LevelController
{
    private float _startQuizTime;
    private float _durationQuizTime;
    
    public override void Init()
    {
        _startQuizTime = Time.time;
        gameObject.SetActive(true);
        
        base.Init();
        
        QuizUIController qc = GetComponent<QuizUIController>();
        qc.ShowQuiz(qc.quizData);
    }
    
    public override void Close()
    {
        //LOGGING BEFORE QUIZ TIME
        _durationQuizTime = Time.time - _startQuizTime;
        LogProgress("QUIZBefore");
        gameObject.SetActive(false);
    }
    
    private void LogProgress(string state)
    {
        if(LoggerService.Instance != null)
            LoggerService.Instance.LogProgressInLevel(state, _durationQuizTime);
    }
}
