using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Games.CPU
{
    public class InstructionsController : MonoBehaviour
    {
        List<LineController> lines;
        
        void Start()
        {
            lines = transform.GetComponentsInChildren<LineController>().ToList();
            lines.ElementAt(0).SetHighlight(true);
        }
        
    }
    
}
