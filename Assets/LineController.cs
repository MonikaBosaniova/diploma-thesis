using System;
using DG.Tweening;
using UnityEngine;

namespace Games.CPU
{
    public class LineController : MonoBehaviour
    {
        [SerializeField] private float maxScaleX = 1;
        [SerializeField] private Transform scalingObject;
        [SerializeField] private GameObject checkMark;

        private void Start()
        {
            scalingObject.DOScaleX(0f, 0f);
            checkMark.SetActive(false);
        }

        [ContextMenu("RemoveToDo")]
        public void RemoveToDo()
        {
            scalingObject.DOScaleX(maxScaleX, 1.5f).OnComplete(() =>
            {
                checkMark.SetActive(true);
            });
        }

    }
    
}
