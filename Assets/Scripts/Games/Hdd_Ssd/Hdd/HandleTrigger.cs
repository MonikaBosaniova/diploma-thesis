using Games.Hdd_Ssd;
using UnityEngine;

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
