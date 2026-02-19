using UnityEngine;

namespace Games.CPU
{
    public class InstructionData : MonoBehaviour
    {
        public bool done;  
        public RegTrigger regTrigger;

        public RegDataType NeededType;

        public bool IfIamDoneAllBeforeAreDoneToo;
        public bool IamIndependedDone;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (regTrigger == null || regTrigger.snappedData == null)
            {
                done = false;
                return;
            }
            done = regTrigger.snappedData.type == NeededType;
        }
    }
    
}
