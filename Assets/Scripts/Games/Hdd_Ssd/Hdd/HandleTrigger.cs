using Games.Hdd_Ssd;
using UnityEngine;

/// <summary>
/// Figures the triggering thi tip of the needle
/// </summary>
public class HandleTrigger : MonoBehaviour
{
    public HddLevelController hddLevelController;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        hddLevelController.DataCollected();
        Destroy(other.gameObject);
    }
}
