using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Games.CPU
{
    public class InstructionsController : MonoBehaviour
    {
        List<LineController> lines;

        private int indexOfActual = -1;
        
        void Start()
        {
            lines = transform.GetComponentsInChildren<LineController>().ToList();
            lines.ElementAt(0).SetHighlight(true);
        }

        private void Update()
        {
            int possibleIndex = -1;
            bool foundedNotDone = false;
            for (int i = 0; i < lines.Count; i++)
            {
                var instructionData = lines[i].instructionData;
                if (!instructionData.done)
                {
                    foundedNotDone = true;
                }
                else
                {
                    if(!foundedNotDone || instructionData.IfIamDoneAllBeforeAreDoneToo)
                        possibleIndex = i;
                }
            }
            
            Visualize(possibleIndex);
        }

        private void Visualize(int possibleIndex)
        {
            var indexOfLastForcedNotDoneTask = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                var instructionData = lines[i].instructionData;
                lines[i].SetHighlight(false);
                if ((i <= possibleIndex && !instructionData.NeededToBeDone) || (instructionData.IamIndependedDone && instructionData.done))
                {
                    lines[i].ToDoRemove();
                }
                else
                {
                    lines[i].ToDoRemove(true);
                    instructionData.OnNotDone?.Invoke();
                    if(instructionData.NeededToBeDone && !instructionData.done)
                        indexOfLastForcedNotDoneTask = i - 1;
                }
                
            }

            var highlightedIndex = -1;
            highlightedIndex = (indexOfLastForcedNotDoneTask < possibleIndex) && indexOfLastForcedNotDoneTask != -1 ? 
                indexOfLastForcedNotDoneTask : possibleIndex;
            if(highlightedIndex  + 1 < lines.Count)
                lines[highlightedIndex + 1].SetHighlight(true);
        }
    }
}
