using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Games.CPU
{
    /// <summary>
    /// Controls all the instructions under him.
    /// </summary>
    public class InstructionsController : MonoBehaviour
    {
        private List<LineController> _lines;
        
        void Start()
        {
            _lines = transform.GetComponentsInChildren<LineController>().ToList();
            _lines.ElementAt(0).SetHighlight(true);
        }

        private void Update()
        {
            int possibleIndex = -1;
            bool foundedNotDone = false;
            for (int i = 0; i < _lines.Count; i++)
            {
                var instructionData = _lines[i].instructionData;
                if (!instructionData.done)
                {
                    foundedNotDone = true;
                }
                else
                {
                    if(!foundedNotDone || instructionData.ifIamDoneAllBeforeAreDoneToo)
                        possibleIndex = i;
                }
            }
            
            Visualize(possibleIndex);
        }
        
        /// <summary>
        /// Visualize crossed (done) instruction to the input index or
        /// if the instruction is independent
        /// </summary>
        /// <param name="possibleIndex"> index to which all the instructions are done</param>
        private void Visualize(int possibleIndex)
        {
            var indexOfLastForcedNotDoneTask = -1;
            for (int i = 0; i < _lines.Count; i++)
            {
                var instructionData = _lines[i].instructionData;
                _lines[i].SetHighlight(false);
                if ((i <= possibleIndex && !instructionData.neededToBeDone) || (instructionData.iamIndependedDone && instructionData.done))
                {
                    _lines[i].ToDoRemove();
                }
                else
                {
                    _lines[i].ToDoRemove(true);
                    instructionData.onNotDone?.Invoke();
                    if(instructionData.neededToBeDone && !instructionData.done)
                        indexOfLastForcedNotDoneTask = i - 1;
                }
                
            }

            var highlightedIndex = -1;
            highlightedIndex = (indexOfLastForcedNotDoneTask < possibleIndex) && indexOfLastForcedNotDoneTask != -1 ? 
                indexOfLastForcedNotDoneTask : possibleIndex;
            if(highlightedIndex  + 1 < _lines.Count)
                _lines[highlightedIndex + 1].SetHighlight(true);
        }
    }
}
