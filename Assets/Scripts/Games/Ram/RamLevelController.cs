using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    public class RamLevelController : LevelController
    {
        [Header("Ram Level")] 
        [SerializeField] private float Time;
        [SerializeField] private GameObject Shape;
        [SerializeField] private GameObject SpawnShapesParent;
        [SerializeField] private GameObject ShapesDoneParent;
        [SerializeField] private CPUWantedAddressController cpuWantedAddressController;

        private int numberToGenerateAddress = 3;
        RamGameManager ramGameManager;
        private List<SnappedAddressCubieController> _allSnappedCubies = new List<SnappedAddressCubieController>();
        private bool generateNewAddress = false;
        
        public override void Init()
        {
            ramGameManager = FindFirstObjectByType<RamGameManager>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
            cpuWantedAddressController.SetRamLevelController(this);
            GenerateShape();
            
            base.Init();
        }

        private void Update()
        {
            if (generateNewAddress)
            {
                var allAddresses = GetAllAddresses();
                cpuWantedAddressController.GenerateWantedAddress(allAddresses);
                generateNewAddress = false;
            }
        }

        public void GenerateShape()
        {
            if (Shape != null)
            {
                Shape.transform.parent = ShapesDoneParent.transform;
                Shape = null;
            }
            
            if (numberToGenerateAddress == 0 && cpuWantedAddressController.generateNewAddress)
            {
                numberToGenerateAddress = 3;
                generateNewAddress = true;
                EnableHighlightingAndDraggingForSnappedCubies(true);
            }
            else
            {
                var random = Random.Range(0, ramGameManager.allPossibleGeneratedShapes.Count);
                Shape = Instantiate(ramGameManager.allPossibleGeneratedShapes[random], SpawnShapesParent.transform);
                Shape.GetComponent<ShapeController>().SetLevelController(this);
                EnableHighlightingAndDraggingForSnappedCubies(false);
            }
            numberToGenerateAddress--;
        }

        private void EnableHighlightingAndDraggingForSnappedCubies(bool enabled)
        {
            Debug.Log(_allSnappedCubies.Count + "   " + enabled);
            foreach (var snappedCubie in _allSnappedCubies)
            {
                snappedCubie.enableHighlighting =  enabled;
                snappedCubie.gameObject.GetComponent<DraggableObject>().draggingEnabled = enabled;
            }
        }

        private List<Tuple<int, int>> GetAllAddresses()
        {
            List<Tuple<int, int>> allPossibleAddresses = new List<Tuple<int, int>>();
            foreach (var cubie in _allSnappedCubies)
            {
                Tuple<int, int> cubiePosition = cubie.GetAddress();
                allPossibleAddresses.Add(cubiePosition);
            }
            return allPossibleAddresses;
        }

        private List<Tuple<int, int>> GetAllCanBeDestroyedAddresses()
        {
            List<Tuple<int, int>> allPossibleAddresses = new List<Tuple<int, int>>();
            foreach (var cubie in _allSnappedCubies)
            {
                Tuple<int, int> cubiePosition = cubie.GetIfCanBeDestroyedAddress();
                if (cubiePosition != null)
                    allPossibleAddresses.Add(cubiePosition);
            }
            return allPossibleAddresses;
        }

        public void AddSnappedCubieToList(SnappedAddressCubieController snappedCubie)
        {
            _allSnappedCubies.Add(snappedCubie);
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
