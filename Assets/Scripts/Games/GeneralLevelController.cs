/// <summary>
/// General purpose level controller that activates/deactivates child objects on Init/Close
/// </summary>
public class GeneralLevelController : LevelController
{
    public override void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        
        base.Init();
    }
    
    public override void Close()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        base.Close();
    }
}
