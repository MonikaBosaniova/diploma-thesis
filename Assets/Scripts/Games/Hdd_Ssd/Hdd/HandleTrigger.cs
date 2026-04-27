using Games.Hdd_Ssd;
using UnityEngine;

/// <summary>
/// Figures the triggering the tip of the needle
/// </summary>
public class HandleTrigger : MonoBehaviour
{
    public HddLevelController hddLevelController;
    
    /// <summary>
    /// Called when the needle tip collides with a data object, notifies level controller and destroys the object
    /// </summary>
    /// <param name="other">Collider of the data object</param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        hddLevelController.DataCollected();
        Destroy(other.gameObject);
    }
}
