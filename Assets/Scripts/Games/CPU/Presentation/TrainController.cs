using DG.Tweening;
using UnityEngine;

namespace Games.CPU
{
    /// <summary>
    /// Animates the train in the CPU presentation
    /// </summary>
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
    }
}
