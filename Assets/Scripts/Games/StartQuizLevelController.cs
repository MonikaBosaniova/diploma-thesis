using UI;
using UnityEngine;

public class StartQuizLevelController : LevelController
{
    public override void Init()
    {
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
        base.Close();
        gameObject.SetActive(false);
    }
    
    
}
