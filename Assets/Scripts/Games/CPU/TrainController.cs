using DG.Tweening;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public float startLocalPositionX = -2f;
    public float endLocalPositionX = 10f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localPosition = new Vector3(startLocalPositionX, transform.localPosition.y, transform.localPosition.z);
        transform.DOLocalMoveX(endLocalPositionX, 5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
