using System;
using System.Collections.Generic;
using System.Linq;
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

        [Header("Garbage collector")] 
        [SerializeField] private bool gcEnabled = true;
        [SerializeField] private bool gcBlocked = false;
        [SerializeField] private float startTimeGC = 1;
        [SerializeField] private float actualTimeGC;
        [SerializeField] private float stepGC = 0.003f;
        [SerializeField] private GameObject disabledGC;
        
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
                disabledGC.SetActive(true);
                gcBlocked = true;
                actualTimeGC = startTimeGC;
            }
            
            if (gcBlocked || gcEnabled) return;

            actualTimeGC -= stepGC;
            if (actualTimeGC <= 0)
            {
                if(disabledGC != null)
                    disabledGC.SetActive(false);
                gcEnabled = true;
                actualTimeGC = startTimeGC;
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
                gcBlocked = false;
                
            }
            numberToGenerateAddress--;
        }
        
        public void AddSnappedCubieToList(SnappedAddressCubieController snappedCubie)
        {
            _allSnappedCubies.Add(snappedCubie);
            gcEnabled = false;
        }
        
        public override void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            base.Close();
        }

        public void CallGarbageCollector()
        {
            gcEnabled = false;
            var allDestroyableCubies = GetAllCanBeDestroyedCubies();
            
            //LOGIC FOR DESTROYING
            //removing 10% randomly
            int numOfToBeDestroyedAddresses = Mathf.RoundToInt(allDestroyableCubies.Count * 0.1f);
            Debug.Log("REMOVING: " + numOfToBeDestroyedAddresses);

            if (allDestroyableCubies.Count < numOfToBeDestroyedAddresses)
            {
                foreach (var cubie in _allSnappedCubies.ToList())
                {
                    if (cubie != null && cubie.canBeDestroyed)
                    {
                        _allSnappedCubies.Remove(cubie);
                        Destroy(cubie.gameObject);
                    }
                }
            }
            else
            {
                for (int i = 0; i < numOfToBeDestroyedAddresses; i++)
                {
                    var random = Random.Range(0, allDestroyableCubies.Count);
                    var randomCubie = allDestroyableCubies.ElementAt(random);
                    SnappedAddressCubieController toBeRemovedCubie = _allSnappedCubies.Find(c => c == randomCubie);
                    _allSnappedCubies.Remove(toBeRemovedCubie);
                    if(toBeRemovedCubie != null)
                        Destroy(toBeRemovedCubie.gameObject);
                }
            }
                
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

        private List<SnappedAddressCubieController> GetAllCanBeDestroyedCubies()
        {
            List<SnappedAddressCubieController> allPossibleACubies = new List<SnappedAddressCubieController>();
            foreach (var cubie in _allSnappedCubies)
            {
                if(cubie.canBeDestroyed)
                    allPossibleACubies.Add(cubie);
            }
            return allPossibleACubies;
        }
        
        private void CheckFinishState(double newValue)
        {
            StartCoroutine(WaitToShowCompleteLevel());
        }
        
    }
}
