using UnityEngine;

namespace Games.CPU
{
    public class InstructionData : MonoBehaviour
    {
        public bool done;  
        public RegTrigger regTrigger;

        public RegDataType NeededType;
        public float NeededValue;

        public bool IfIamDoneAllBeforeAreDoneToo;
        public bool IamIndependedDone;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //TODO HARDCODED
            if (NeededType == RegDataType.ManaLeft)
                NeededValue = 12;
            if (NeededType == RegDataType.Cost)
                NeededValue = 10;
            if (NeededType == RegDataType.PlusRes)
                NeededValue = 22;
            if (NeededType == RegDataType.MinusRes)
                NeededValue = 2f;
            if (NeededType == RegDataType.MultiplyRes)
                NeededValue = 120f;
            if (NeededType == RegDataType.BiggerThanZeroRes)
                NeededValue = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            if (regTrigger == null || regTrigger.snappedData == null)
            {
                done = false;
                return;
            }

            done = (regTrigger.snappedData.type == NeededType) && (Mathf.Approximately(regTrigger.snappedData.value, NeededValue));
        }
    }
    
}
