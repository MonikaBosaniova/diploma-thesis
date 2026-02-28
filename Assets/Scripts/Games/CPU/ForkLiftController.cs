using DG.Tweening;
using UnityEngine;

public class ForkLiftController : MonoBehaviour
{
    public Transform fork;
    public float startLocalPositionY = -0.15f;
    public float endLocalPositionY = 0.3f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fork.localPosition = new Vector3(fork.localPosition.x, startLocalPositionY, fork.localPosition.z);
        fork.DOLocalMoveY(endLocalPositionY, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
