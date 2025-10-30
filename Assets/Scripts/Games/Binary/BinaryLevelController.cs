using System;
using System.Collections.Generic;
using Gates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Binary
{
    public class BinaryLevelController : LevelController
    {
        [Header("Level values")]
        [SerializeField] protected int decFinalNumber = 0;
        [SerializeField] public bool isBinToDec = true;
        [SerializeField] public GameObject displayNumbersParent;
        
        private List<GameObject> allDisplayNumbers;
        private BinaryCalculator _binaryCalculator;

        private const int numOfBits = 4;

        public override void Init()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            if(decFinalNumber == 0)
                decFinalNumber =  Random.Range(1, (int)Math.Pow(2, numOfBits));
            
            _binaryCalculator = gameObject.GetComponent<BinaryCalculator>();
            if(_binaryCalculator != null)
                _binaryCalculator.OnDecValueChanged += CheckFinishState;
            base.Init();
        }

        private void CheckFinishState(double newValue)
        {
            if (Math.Abs(decFinalNumber - newValue) > 0.01) return;
            _binaryCalculator.OnDecValueChanged -= CheckFinishState;
            StartCoroutine(WaitToShowCompleteLevel());
        }

        public override void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            base.Close();
        }

        private void ShowBinNumber(int numToShow)
        {
            var binary = Convert.ToString(numToShow, 2);
            if (binary.Length < numOfBits)
            {
                binary = new string('0', numOfBits - binary.Length) + binary;
            }
            for (int i = 0; i < binary.Length; i++)
            {
                Int32.TryParse(binary[i].ToString(), out int value);
                
            }
            //for (var i = 0; binary)
  
        }

        private void ShowDecNumber(int numToShow)
        {
            
        }

        private void ShowNumberOnSpecificIndex(int indexInDisplay, int numToShow)
        {

        }
        
        
    }
}
