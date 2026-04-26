using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DialogueSystem;
using UnityEngine;

namespace Games.Hdd_Ssd
{
    /// <summary>
    /// Level controller for the HDD minigame, manages data collection, disk progression and speed changes
    /// </summary>
    public class HddLevelController : LevelController
    {
        public GameObject smallDisk;
        public GameObject bigDisk;
        public GameObject topCover;
        
        private List<DiskController> diskControllers;

        private DialogueSequenceController _dialogueSequenceController;
        
        [Header("DEBUG")]
        [SerializeField] private int _maxDataCollected = 5;
        [SerializeField] private int _dataCollected = 0;
        private int lastGeneratedRandom;
        bool finishLevel = false;
        
        public override void Init()
        {
            base.Init();
            
            _dialogueSequenceController = transform.GetComponent<DialogueSequenceController>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
            smallDisk.gameObject.SetActive(true);
            diskControllers = smallDisk.transform.GetComponentsInChildren<DiskController>().ToList();
            GenerateRandomDataPoint();
            
            topCover.transform.DOLocalMoveZ(10.5f, 0.8f);

        }

        /// <summary>
        /// Called when data is collected by the needle, tracks progress and generates new data
        /// </summary>
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

        /// <summary>
        /// Progresses the level through stages: small disk -> big disk -> fast speed -> finish
        /// </summary>
        private void ContinueInLevel()
        {
            if (smallDisk.activeSelf)
            {
                SetDialogueText(_dialogueSequenceController.afterMoreDialogueSequences.ElementAt(0));
                smallDisk.SetActive(false);
                bigDisk.SetActive(true);
                diskControllers = bigDisk.transform.GetComponentsInChildren<DiskController>().ToList();
                GenerateRandomDataPoint();
                return;
            }

            if (bigDisk.activeSelf && !finishLevel)
            {
                SetDialogueText(_dialogueSequenceController.afterMoreDialogueSequences.ElementAt(1));
                SetSpeedRotation(50f);
                GenerateRandomDataPoint();
                finishLevel = true;
                return;
            }
            
            CheckFinishState(0);
        }

        /// <summary>
        /// Spawns a data cube on a random disk platter
        /// </summary>
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

        private void SetDialogueText(DialogueSequence sequence)
        {
            if (_dialogueSequenceController != null)
            {
                UIManager.Instance.ShowDialogWindowUI(sequence);
            }
        }
    }
}
