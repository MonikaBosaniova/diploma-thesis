using UnityEngine;
using UnityEngine.Events;

namespace Games.CPU
{
    public class InstructionData : MonoBehaviour
    {
        public bool done;
        public bool instructionWithoutRegisters;
        public RegTrigger regTrigger;

        public RegDataType NeededType;
        public float NeededValue;
        public float NeededValueForEq;

        public bool IfIamDoneAllBeforeAreDoneToo;
        public bool IamIndependedDone;
        public bool NeededToBeDone;
        
        public UnityEvent OnDone;
        public UnityEvent OnNotDone;
        public bool callOnDone = true;
        public bool callOnce;
        private bool calledOnce = false;
        
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
            {
                NeededValue = 1f;
                NeededValueForEq = 2f;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if ((regTrigger == null || regTrigger.snappedData == null) && !NeededToBeDone)
            {
                done = false;
                callOnDone = true;
                return;
            }

            if (!instructionWithoutRegisters)
            {
                if(regTrigger.snappedData.type != RegDataType.BiggerThanZeroRes)
                    done = (regTrigger.snappedData.type == NeededType) && (Mathf.Approximately(regTrigger.snappedData.value, NeededValue));
                else
                {
                    
                    done = (regTrigger.snappedData.type == NeededType) && (Mathf.Approximately(regTrigger.snappedData.value, NeededValue))
                        && (Mathf.Approximately(regTrigger.snappedData.eqValue, NeededValueForEq));;
                }
            }
            
            switch (done)
            {
                case false:
                    callOnDone = true;
                    break;
                case true when callOnDone:
                    if (callOnce)
                    {
                        if (!calledOnce)
                        {
                            OnDone.Invoke();
                            callOnDone = false;
                            calledOnce = true;
                        }
                    }
                    else
                    {
                        OnDone.Invoke();
                        callOnDone = false;
                    }
                    break;
            }
        }
        
        public void SetDone(bool done)
        {
            Debug.Log("SetDone" + gameObject.name);
            this.done = done;
        }
    }
}
