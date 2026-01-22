using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Games.HddSsd
{
    public class SsdLevelController : LevelController
    {
        public SsdButtonsController VerticalButtonsController;
        public SsdButtonsController HorizontalButtonsController;
        [SerializeField] private List<GameObject> rows;
        
        HddSsdGameManager hddGameManager;
        
        [Header("---DEBUG---")]
        [SerializeField] private int _maxDataCollected = 5;
        [SerializeField] private int _dataCollected = 0;
        [SerializeField] private int numOfRepetitions = 3;
        [SerializeField] private SsdSegment generatedData;
        private Tuple<int,int> lastGeneratedRandom = new Tuple<int,int>(-1,-1);
        private Tuple<int, int> chosenPosition = new Tuple<int, int>(-1,-1);
        bool finishLevel = false;
        
        [Header("Dimensions")]
        [SerializeField] private int sectionXCount;
        [SerializeField] private int sectionYCount;
        
        List<SsdButton> horizontalButtons = new List<SsdButton>();
        List<SsdButton> verticalButtons = new List<SsdButton>();
        
        List<float> lineScalingSettings = new List<float>()
        {
            4.85f, 9.7f, 14.35f
        };
        
        public override void Init()
        {
            hddGameManager = FindFirstObjectByType<HddSsdGameManager>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            sectionXCount = rows.ElementAt(0).transform.childCount / numOfRepetitions;
            sectionYCount = rows.Count;
            
            horizontalButtons = HorizontalButtonsController.transform.GetComponentsInChildren<SsdButton>().ToList();
            verticalButtons = VerticalButtonsController.transform.GetComponentsInChildren<SsdButton>().ToList();
            
            VerticalButtonsController.OnSelectionChange += CheckVerticalCollision;
            HorizontalButtonsController.OnSelectionChange += CheckHorizontalCollision;
            
            ShowPartOfSections(numOfRepetitions);
            GenerateRandomDataPoint();
            
            base.Init();
        }

        private void CheckHorizontalCollision(int obj)
        {
            chosenPosition = new Tuple<int, int>(obj, chosenPosition.Item2);
            CheckCollision();
        }

        private void CheckVerticalCollision(int obj)
        {
            chosenPosition = new Tuple<int, int>(chosenPosition.Item1, obj);
            CheckCollision();
        }

        private void CheckCollision()
        {
            if (!Equals(chosenPosition, lastGeneratedRandom))
            {
                return;
            }
            DataCollected();
        }

        private void ShowPartOfSections(int i)
        {
            for (int j = 0; j < sectionYCount; j++)
            {
                for (int k = 0; k < rows.ElementAt(0).transform.childCount; k++)
                {
                    if (k >= sectionXCount * (i - 1))
                    {
                        rows.ElementAt(j).transform.GetChild(k).gameObject.SetActive(true);
                        horizontalButtons.ElementAt(k).gameObject.SetActive(true);
                    }
                    else
                    {
                        rows.ElementAt(j).transform.GetChild(k).gameObject.SetActive(false);
                        horizontalButtons.ElementAt(k).gameObject.SetActive(false);
                    }
                }

                var scalingIndex = lineScalingSettings.Count - i;
                var scaleZ = lineScalingSettings[scalingIndex];
                var posZ = scaleZ / 2f;
                verticalButtons.ElementAt(j).gameObject.GetComponent<SsdButton>().SetLineScaling(scaleZ, posZ);
            }
        }

        private void DataCollected()
        {
            _dataCollected++;
            generatedData.SetValue(false);
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
            if (numOfRepetitions == 1)
            {
                CheckFinishState(0);
                return;
            }
            numOfRepetitions--;
            ShowPartOfSections(numOfRepetitions);
            GenerateRandomDataPoint();

        }

        private void GenerateRandomDataPoint()
        {
            var randomNumX = UnityEngine.Random.Range(sectionXCount * (numOfRepetitions - 1), horizontalButtons.Count);
            var randomNumY = UnityEngine.Random.Range(0, sectionYCount);
            Tuple<int, int> randomPosition = new Tuple<int, int>(randomNumX, randomNumY);
            while (Equals(randomPosition, lastGeneratedRandom))
            {
                randomNumX = UnityEngine.Random.Range(sectionXCount * (numOfRepetitions - 1), horizontalButtons.Count);
                randomNumY = UnityEngine.Random.Range(0, sectionYCount);
                randomPosition = new Tuple<int, int>(randomNumX, randomNumY);
            }
            lastGeneratedRandom = randomPosition;
            generatedData = rows.ElementAt(randomPosition.Item2).transform.GetChild(randomPosition.Item1).gameObject.GetComponent<SsdSegment>();
            generatedData.SetValue(true);
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
    }
}
