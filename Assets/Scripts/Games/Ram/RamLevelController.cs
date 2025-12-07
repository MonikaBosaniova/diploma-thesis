using System;
using System.Collections.Generic;
using Gates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    public class RamLevelController : LevelController
    {
        [Header("Ram Level")] 
        [SerializeField] private GameObject ShapePrefab;
        [SerializeField] private GameObject SpawnShapesParent;
        
        public override void Init()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            GenerateShape();
            
            base.Init();
        }

        public void GenerateShape()
        {
            Debug.Log("Generate shape");
        }

        private void CheckFinishState(double newValue)
        {
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

        private void ShowDecNumber(int numToShow)
        {
            
        }

        private void ShowNumberOnSpecificIndex(int indexInDisplay, int numToShow)
        {

        }
        
    }
}
