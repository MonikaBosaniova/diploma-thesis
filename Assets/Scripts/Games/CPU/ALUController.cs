using System;
using DG.Tweening;
using UnityEngine;

namespace Games.CPU
{
    public class ALUController : MonoBehaviour
    {
        public GameObject aluResultPrefab;
        public Transform outputSpawn;
        [SerializeField] private RegTrigger reg1_in;
        [SerializeField] private RegTrigger reg2_in;
        [SerializeField] private RegTrigger reg3_out;
        
        [Header("Dispalys")]
        [SerializeField] private GameObject display1_10;
        [SerializeField] private GameObject display1_1;
        [SerializeField] private GameObject display2_10;
        [SerializeField] private GameObject display2_1;

        private void Start()
        {
            reg1_in.snap += () => { VisualizeDecValue(0); };
            reg2_in.snap += () => { VisualizeDecValue(1); };
        }

        private void Update()
        {
            if (reg1_in.transform.childCount == 0)
            {
                CleanDisplay(0);
            }

            if (reg2_in.transform.childCount == 0)
            {
                CleanDisplay(1);
            }
        }

        public void Plus()
        {
            var value = 0f;
            if(reg1_in.transform.childCount > 0)
                value += reg1_in.transform.GetChild(0).GetComponent<RegData>().value;
            if (reg2_in.transform.childCount > 0)
                value += reg2_in.transform.GetChild(0).GetComponent<RegData>().value;
            
            InstantiateNewOutData(value, RegDataType.PlusRes);
            Debug.Log("PLUS: " + value);
        }

        public void Minus()
        {
            var value = 0f;
            if(reg1_in.transform.childCount > 0)
                value = reg1_in.transform.GetChild(0).GetComponent<RegData>().value;
            if (reg2_in.transform.childCount > 0)
                value -= reg2_in.transform.GetChild(0).GetComponent<RegData>().value;
            
            InstantiateNewOutData(value, RegDataType.MinusRes);
            Debug.Log("Minus: " + value);
        }

        public void Multiply()
        {
            var value = 1f;
            if(reg1_in.transform.childCount > 0)
                value *= reg1_in.transform.GetChild(0).GetComponent<RegData>().value;
            else value = 0f;
            if (reg2_in.transform.childCount > 0)
                value *= reg2_in.transform.GetChild(0).GetComponent<RegData>().value;
            else value = 0f;
            
            InstantiateNewOutData(value, RegDataType.MultiplyRes);
            Debug.Log("Multiply: " + value);
        }

        public void LessThanZero()
        {
            var value = 0f;
            if (reg1_in.transform.childCount > 0)
            {
                if (reg1_in.transform.GetChild(0).GetComponent<RegData>().value < 0)
                {
                    value = 1f;
                }
            }
            
            InstantiateNewOutData(value, RegDataType.LesserThanZeroRes);
        }
        
        public void BiggerThanZero()
        {
            var value = 0f;
            var eqValue = 0f;
            if (reg1_in.transform.childCount > 0)
            {
                if (reg1_in.transform.GetChild(0).GetComponent<RegData>().value > 0)
                {
                    value = 1f;
                }
                
                //HARDCODED
                if(Mathf.Approximately(reg1_in.transform.GetChild(0).GetComponent<RegData>().value, 2f))
                    eqValue = 2f;
            }
            
            InstantiateNewOutData(value, RegDataType.BiggerThanZeroRes, eqValue);
        }

        private void InstantiateNewOutData(float value, RegDataType type, float eqValue = 0f)
        {
            var newDataObject = Instantiate(aluResultPrefab, outputSpawn);
            DraggableObject draggable = newDataObject.gameObject.GetComponent<DraggableObject>();
            var data = newDataObject.GetComponent<RegData>();
            data.value = value;
            data.eqValue = eqValue;
            data.type = type;
            data._regParent = reg3_out.transform;
            
            Transform outputReg = reg3_out.transform;
            if (outputReg.childCount > 0)
            {
                Destroy(outputReg.GetChild(0).gameObject);
            }
            newDataObject.transform.parent = reg3_out.transform;
            newDataObject.transform.DOLocalMove(new Vector3(0,0.25f,0), 1.5f).OnComplete(() =>
            {
                draggable.draggingEnabled = true;
            });
            reg3_out.snappedData = data;
            draggable.DragEnd += () => reg3_out.SnapDataToReg(data);
        }

        private void CleanDisplay(int display)
        {
            Transform digit01;
            Transform digit10;
            
            if (display == 0)
            {
                digit01 = display1_1.transform;
                digit10 = display1_10.transform;
            }
            else
            {
                digit01 = display2_1.transform;
                digit10 = display2_10.transform;
            }
            
            digit10.GetChild(0).gameObject.SetActive(true);
            digit10.GetChild(1).gameObject.SetActive(false);
            for (int i = 0; i < digit01.childCount; i++)
            {
                digit01.GetChild(i).gameObject.SetActive(false);
            }
            digit01.GetChild(0).gameObject.SetActive(true);
        }
        
        public void VisualizeDecValue(int display = 0)
        {
            Transform digit01;
            Transform digit10;

            double value = 0;

            if (display == 0)
            {
                digit01 = display1_1.transform;
                digit10 = display1_10.transform;
                value = reg1_in.transform.childCount == 1 ? reg1_in.transform.GetChild(0).gameObject.GetComponent<RegData>().value : 
                    reg1_in.transform.GetChild(1).gameObject.GetComponent<RegData>().value;
            }
            else
            {
                digit01 = display2_1.transform;
                digit10 = display2_10.transform;
                value =  reg2_in.transform.childCount == 1 ? reg2_in.transform.GetChild(0).gameObject.GetComponent<RegData>().value : 
                    reg2_in.transform.GetChild(1).gameObject.GetComponent<RegData>().value;

            }
            
            var indexOfDecDigit_1 = 0d;
        
            if (value >= 10)
            {
                digit10.GetChild(0).gameObject.SetActive(false);
                digit10.GetChild(1).gameObject.SetActive(true);
                indexOfDecDigit_1 = Math.Abs(value - 10);
            }
            else
            {
                digit10.GetChild(0).gameObject.SetActive(true);
                digit10.GetChild(1).gameObject.SetActive(false);
                indexOfDecDigit_1 = value;

            }

            for (int i = 0; i < digit01.childCount; i++)
            {
                digit01.GetChild(i).gameObject.SetActive(false);
            }
            digit01.GetChild((int)indexOfDecDigit_1).gameObject.SetActive(true);
        }
    }
    
}
