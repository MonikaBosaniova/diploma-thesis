using DG.Tweening;
using UnityEngine;

public class MetronomController : MonoBehaviour
{
    public Transform handle;
    private const float speed = 1f;
    
    private Tween _metronomTweeen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        handle.localRotation = Quaternion.Euler(0, 0, -10f);
        handle.DOLocalRotate(new Vector3(0, 0, 10f), speed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
