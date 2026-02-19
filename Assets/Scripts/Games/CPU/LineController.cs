using DG.Tweening;
using UnityEngine;

namespace Games.CPU
{
    public class LineController : MonoBehaviour
    {
        public InstructionData instructionData;
        [SerializeField] private float maxScaleX = 1;
        [SerializeField] private Transform scalingObject;
        [SerializeField] private GameObject checkMark;
        [SerializeField] private GameObject highlightObject;
        
        private Tween scalingTween;
        
        private void Start()
        {
            scalingObject.DOScaleX(0f, 0f);
            checkMark.SetActive(false);
            SetHighlight(false);
        }

        [ContextMenu("RemoveToDo")]
        public void ToDoRemove(bool undo = false)
        {
            if (undo)
            {
                if(scalingTween != null && scalingTween.active) scalingTween.Kill();
                
                scalingObject.localScale = new Vector3(0f, scalingObject.localScale.y, scalingObject.localScale.z);
                checkMark.SetActive(false);
            }
            else
            {
                if(scalingTween != null && scalingTween.active) return;
                
                scalingTween = scalingObject.DOScaleX(maxScaleX, 1.5f).OnComplete(() =>
                {
                    checkMark.SetActive(true);
                });
            }
        }

        public void SetHighlight(bool highlight)
        {
            highlightObject.SetActive(highlight);
        }
        

    }
}
