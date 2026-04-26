using DG.Tweening;
using UnityEngine;

/// <summary>
/// Animates the HDD handle random movement in the presentation
/// </summary>
public class HDDTutorialAnimation : MonoBehaviour
{

    public Transform topDiskPanel;
    public Transform handle;
    public GameObject TextBox;

    private bool handleMoving;
    private Tween showTween;
    
    void OnEnable()
    {
        Reset();
        showTween = topDiskPanel.DOLocalMoveZ(12f, 3f).OnComplete(() =>
        {
            TextBox.SetActive(true);
            MoveHandle();
        });
    }

    /// <summary>
    /// Resets all animation elements to their initial state
    /// </summary>
    void Reset()
    {
        showTween.Kill();
        handle.localRotation = Quaternion.identity;
        topDiskPanel.localPosition = Vector3.zero;
        TextBox.SetActive(false);
    }

    /// <summary>
    /// Recursively moves the HDD handle to random angles for the tutorial animation
    /// </summary>
    private void MoveHandle()
    {
        var randomAngle = Random.Range(0f, 75f);
        handle.DOLocalRotate(new Vector3(0, 0, randomAngle), 1f).OnComplete(MoveHandle);
    }
}
