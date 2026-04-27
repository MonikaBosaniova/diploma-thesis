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

        /// <summary>
        /// Creates the overheating animation sequence using DOTween
        /// </summary>
        void CreateSequence()
        {
            // Create a new sequence
            Sequence mySequence = DOTween.Sequence();

            mySequence.Append(boltsIn.transform.DOLocalMoveX(_endPosBoltsIn, duration + 1f))
                .AppendCallback(() => boltsIn.SetActive(false));

            float parallelStartTime = duration + 1f;

            mySequence.Insert(parallelStartTime, heat.transform.DOLocalMoveZ(_endPosHeat, duration))
                .InsertCallback(parallelStartTime + duration, () => heat.SetActive(false));

            mySequence.Insert(parallelStartTime, boltsOut.transform.DOLocalMoveX(_endPosBoltsOut, duration + 1f))
                .InsertCallback(parallelStartTime + duration + 1f, () => boltsOut.SetActive(false));

            mySequence.OnComplete(() => {
                ResetObjects();
                CreateSequence();
            });
        }

        /// <summary>
        /// Resets all animated objects to their start position and re-enables them
        /// </summary>
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
