using UI;

/// <summary>
/// Level controller for reviews of minigame, used in testing in school
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
