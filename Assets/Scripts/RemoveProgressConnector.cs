using UnityEngine;

/// <summary>
/// UI connector that allows resetting player progress via button click
/// </summary>
public class RemoveProgressConnector : MonoBehaviour
{
    /// <summary>
    /// Delegates progress removal to the ProgressService singleton
    /// </summary>
    public void RemoveProgress()
    {
        ProgressService.I.RemoveProgress();
    }
}
