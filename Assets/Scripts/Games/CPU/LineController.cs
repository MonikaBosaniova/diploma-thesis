using DG.Tweening;
using UnityEngine;

namespace Games.CPU
{
    /// <summary>
    /// Controls the line of instruction in CPU minigame
    /// </summary>
    public class LineController : MonoBehaviour
    {
        public InstructionData instructionData;
        [SerializeField] private float maxScaleX = 1;
        [SerializeField] private Transform scalingObject;
        [SerializeField] private GameObject checkMark;
        [SerializeField] private GameObject highlightObject;
        
        private Tween _scalingTween;
        
        private void Start()
        {
            scalingObject.DOScaleX(0f, 0f);
            checkMark.SetActive(false);
            SetHighlight(false);
        }

        /// <summary>
        /// Tween the scale of line
        /// </summary>
        /// <param name="undo">undone the instruction, if true</param>
        [ContextMenu("RemoveToDo")]
        public void ToDoRemove(bool undo = false)
        {
            if (undo)
            {
                if(_scalingTween != null && _scalingTween.active) _scalingTween.Kill();
                
                scalingObject.localScale = new Vector3(0f, scalingObject.localScale.y, scalingObject.localScale.z);
                checkMark.SetActive(false);
            }
            else
            {
                if(_scalingTween != null && _scalingTween.active) return;
                
                _scalingTween = scalingObject.DOScaleX(maxScaleX, 1.5f).OnComplete(() =>
                {
                    checkMark.SetActive(true);
                });
            }
        }
        
        /// <summary>
        /// show the background color, to visualize highlighting
        /// </summary>
        /// <param name="highlight"></param>
        public void SetHighlight(bool highlight)
        {
            highlightObject.SetActive(highlight);
        }
    }
}
