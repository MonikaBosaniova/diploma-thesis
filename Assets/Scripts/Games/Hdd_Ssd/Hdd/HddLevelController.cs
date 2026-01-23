using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Games.HddSsd
{
    public class HddLevelController : LevelController
    {
        public GameObject smallDisk;
        public GameObject bigDisk;
        public GameObject topCover;
        
        HddSsdGameManager hddGameManager;
        private List<DiskController> diskControllers;
        
        [Header("DEBUG")]
        [SerializeField] private int _maxDataCollected = 5;
        [SerializeField] private int _dataCollected = 0;
        private int lastGeneratedRandom;
        bool finishLevel = false;
        
        public override void Init()
        {
            hddGameManager = FindFirstObjectByType<HddSsdGameManager>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
            smallDisk.gameObject.SetActive(true);
            diskControllers = smallDisk.transform.GetComponentsInChildren<DiskController>().ToList();
            GenerateRandomDataPoint();
            
            topCover.transform.DOLocalMoveZ(10.5f, 0.8f).OnComplete(() =>
            {
                base.Init();
            });
            
        }

        public void DataCollected()
        {
            _dataCollected++;
            if (_dataCollected >= _maxDataCollected)
            {
                _dataCollected = 0;
                ContinueInLevel();
            }
            else
            {
                GenerateRandomDataPoint();
            }
        }

        private void ContinueInLevel()
        {
            if (smallDisk.activeSelf)
            {
                smallDisk.SetActive(false);
                bigDisk.SetActive(true);
                diskControllers = bigDisk.transform.GetComponentsInChildren<DiskController>().ToList();
                GenerateRandomDataPoint();
                return;
            }

            if (bigDisk.activeSelf && !finishLevel)
            {
                SetSpeedRotation(50f);
                GenerateRandomDataPoint();
                finishLevel = true;
                return;
            }
            
            CheckFinishState(0);

        }

        private void GenerateRandomDataPoint()
        {
            var randomNum = Random.Range(0, diskControllers.Count);
            while (randomNum == lastGeneratedRandom)
            {
                randomNum = Random.Range(0, diskControllers.Count);
            }
            lastGeneratedRandom = randomNum;
            diskControllers[randomNum].CreateCube();
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

        private void SetSpeedRotation(float newSpeed)
        {
            foreach (var diskController in diskControllers)
            {
                diskController.SetSpeedRotation(newSpeed);
            }
        }
        
    }
}
