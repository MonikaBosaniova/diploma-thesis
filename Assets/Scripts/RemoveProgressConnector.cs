using UnityEngine;

public class RemoveProgressConnector : MonoBehaviour
{
    public void RemoveProgress()
    {
        ProgressService.I.RemoveProgress();
    }
}
