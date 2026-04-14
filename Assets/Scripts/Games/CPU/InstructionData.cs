using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Games.CPU
{
    /// <summary>
    /// Information to set about instruction behaviour
    /// </summary>
    public class InstructionData : MonoBehaviour
    {
        public bool done;
        public bool instructionWithoutRegisters;
        public RegTrigger regTrigger;

        [FormerlySerializedAs("NeededType")] public RegDataType neededType;
        [FormerlySerializedAs("NeededValue")] public float neededValue;
        [FormerlySerializedAs("NeededValueForEq")] public float neededValueForEq;

        [FormerlySerializedAs("IfIamDoneAllBeforeAreDoneToo")] public bool ifIamDoneAllBeforeAreDoneToo;
        [FormerlySerializedAs("IamIndependedDone")] public bool iamIndependedDone;
        [FormerlySerializedAs("NeededToBeDone")] public bool neededToBeDone;
        
        [FormerlySerializedAs("OnDone")] public UnityEvent onDone;
        [FormerlySerializedAs("OnNotDone")] public UnityEvent onNotDone;
        public bool callOnDone = true;
        public bool callOnce;
        private bool _calledOnce = false;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //HARDCODED VALUES FOR SCENARIO OF CPU GAMES
            if (neededType == RegDataType.ManaLeft)
                neededValue = 12;
            if (neededType == RegDataType.Cost)
                neededValue = 10;
            if (neededType == RegDataType.PlusRes)
                neededValue = 22;
            if (neededType == RegDataType.MinusRes)
                neededValue = 2f;
            if (neededType == RegDataType.MultiplyRes)
                neededValue = 120f;
            if (neededType == RegDataType.BiggerThanZeroRes)
            {
                neededValue = 1f;
                neededValueForEq = 2f;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if ((regTrigger == null || regTrigger.snappedData == null) && !neededToBeDone)
            {
                done = false;
                callOnDone = true;
                return;
            }

            if (!instructionWithoutRegisters)
            {
                if(regTrigger.snappedData.type != RegDataType.BiggerThanZeroRes)
                    done = (regTrigger.snappedData.type == neededType) && (Mathf.Approximately(regTrigger.snappedData.value, neededValue));
                else
                {
                    
                    done = (regTrigger.snappedData.type == neededType) && (Mathf.Approximately(regTrigger.snappedData.value, neededValue))
                        && (Mathf.Approximately(regTrigger.snappedData.eqValue, neededValueForEq));;
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
                        if (!_calledOnce)
                        {
                            onDone.Invoke();
                            callOnDone = false;
                            _calledOnce = true;
                        }
                    }
                    else
                    {
                        onDone.Invoke();
                        callOnDone = false;
                    }
                    break;
            }
        }
        
        public void SetDone(bool value)
        {
            this.done = value;
        }
    }
}
