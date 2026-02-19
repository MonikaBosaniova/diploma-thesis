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
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].SetHighlight(false);
                if (i <= possibleIndex || (lines[i].instructionData.IamIndependedDone && lines[i].instructionData.done))
                {
                    lines[i].ToDoRemove();
                }
                else
                {
                    lines[i].ToDoRemove(true);
                }
                
            }
            if(possibleIndex  + 1 < lines.Count)
                lines[possibleIndex + 1].SetHighlight(true);
        }
    }
}
