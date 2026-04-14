using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Games.CPU
{
    /// <summary>
    /// Animates the CPU overheating problem in the CPU presentation
    /// </summary>
    public class ThermotrotlingController : MonoBehaviour
    {
        [FormerlySerializedAs("BoltsIn")] public GameObject boltsIn;
        [FormerlySerializedAs("BoltsOut")] public GameObject boltsOut;
        [FormerlySerializedAs("Heat")] public GameObject heat;

        public float duration = 1f;

        private readonly float _startPosBoltsIn = -5f;
        private readonly float _startPosBoltsOut = 0;
        private readonly float _startPosHeat = -1f;
    
        private readonly float _endPosBoltsIn = 0f;
        private readonly float _endPosBoltsOut = 5f;
        private readonly float _endPosHeat = 1f;
    
        void Start()
        {
            CreateSequence();
        }

        void CreateSequence()
        {
            // Create a new sequence
            Sequence mySequence = DOTween.Sequence();

            mySequence.Append(boltsIn.transform.DOLocalMoveX(_endPosBoltsIn, duration + 1f))
                .AppendCallback(() => boltsIn.SetActive(false));

            mySequence.Append(heat.transform.DOLocalMoveZ(_endPosHeat, duration))
                .AppendCallback(() => heat.SetActive(false));

            mySequence.Append(boltsOut.transform.DOLocalMoveX(_endPosBoltsOut, duration + 1f))
                .AppendCallback(() => boltsOut.SetActive(false));

            mySequence.OnComplete(() => {
                ResetObjects();
                CreateSequence();
            });
        }

        void ResetObjects()
        {
            boltsIn.transform.DOLocalMoveX(_startPosBoltsIn, 0f);
            heat.transform.DOLocalMoveZ(_startPosHeat, 0f);
            boltsOut.transform.DOLocalMoveX(_startPosBoltsOut, 0f);
        
            boltsIn.SetActive(true);
            heat.SetActive(true);
            boltsOut.SetActive(true);
        }
    }
}
