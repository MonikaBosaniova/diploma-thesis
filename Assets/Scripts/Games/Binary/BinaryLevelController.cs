using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Binary
{
    /// <summary>
    /// Controls a single binary conversion level, handling initialization, calculator setup and finish detection
    /// </summary>
    public class BinaryLevelController : LevelController
    {
        [Header("Level values")]
        [SerializeField] protected int decFinalNumber = 0;
        [SerializeField] public bool isBinToDec = true;
        [SerializeField] public bool isFreeMode = false;
        [SerializeField] public GameObject displayNumbersParent;
        
        [SerializeField] private bool showHelpingNumbers = true;
        [SerializeField] private GameObject _helpingNumbers;
        
        private List<GameObject> allDisplayNumbers;
        private BinaryCalculator _binaryCalculator;

        private const int NumOfBits = 4;

        public override void Init()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
            if(decFinalNumber == 0)
                decFinalNumber =  Random.Range(1, (int)Math.Pow(2, NumOfBits));
            
            _binaryCalculator = gameObject.GetComponent<BinaryCalculator>();
            if (_binaryCalculator != null)
            {
                _binaryCalculator.OnDecValueChanged += StartFinishState;
                _binaryCalculator._finalValue = decFinalNumber;
                if (!isBinToDec)
                {
                    _binaryCalculator.VisualizeDecValue(decFinalNumber, true);
                    _binaryCalculator.VisualizeDecValue(decFinalNumber, false);
                }
                else
                {
                    if (!isFreeMode)
                    {
                        _binaryCalculator.GenerateRandomBinNumber();
                    }
                }
            }
            
            if(_helpingNumbers != null)
                _helpingNumbers.SetActive(showHelpingNumbers);
            
            base.Init();
        }

        /// <summary>
        /// Handles level completion when the correct binary value is entered
        /// </summary>
        /// <param name="newValue">The decimal value that was correctly entered</param>
        private void StartFinishState(double newValue)
        {
            _binaryCalculator.OnDecValueChanged -= StartFinishState;
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
    }
}