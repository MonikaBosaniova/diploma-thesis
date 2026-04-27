using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    /// <summary>
    /// Level controller for the RAM minigame, manages shape generation, snapping, garbage collection and address queries
    /// </summary>
    public class RamLevelController : LevelController
    {
        [Header("Ram Level")] 
        [SerializeField] private float _time;
        [SerializeField] private GameObject Shape;
        [SerializeField] private GameObject SpawnShapesParent;
        [SerializeField] private GameObject ShapesDoneParent;
        [SerializeField] private CPUWantedAddressController cpuWantedAddressController;

        [Header("Garbage collector")] 
        [SerializeField] private bool gcEnabled = true;
        [SerializeField] private bool gcBlocked = false;
        [SerializeField] private float startTimeGC = 1;
        [SerializeField] private float actualTimeGC;
        [SerializeField] private float stepGC = 0.05f;
        [SerializeField] private GameObject disabledGC;
        
        private int _numberToGenerateAddress = 3;
        private RamGameManager _ramGameManager;
        private List<SnappedAddressCubieController> _allSnappedCubies = new List<SnappedAddressCubieController>();
        private bool _generateNewAddress = false;
        private int _numOfGCCalled = 0;
        
        public override void Init()
        {
            _ramGameManager = FindFirstObjectByType<RamGameManager>();
            
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
            if (_generateNewAddress)
            {
                var allAddresses = GetAllAddresses();
                cpuWantedAddressController.GenerateWantedAddress(allAddresses);
                _numOfGCCalled = 0;
                _generateNewAddress = false;
                disabledGC.SetActive(true);
                gcBlocked = true;
            }
            
            if (gcBlocked || gcEnabled) return;

            actualTimeGC -= stepGC * Time.deltaTime;
            if (actualTimeGC <= 0)
            {
                if(disabledGC != null)
                    disabledGC.SetActive(false);
                gcEnabled = true;
            }
        }

        /// <summary>
        /// Generates a new random shape for the player to place, or triggers address query mode
        /// </summary>
        public void GenerateShape()
        {
            if (Shape != null)
            {
                Shape.transform.parent = ShapesDoneParent.transform;
                Shape = null;
            }
            
            if (_numberToGenerateAddress == 0 && cpuWantedAddressController.generateNewAddress)
            {
                _numberToGenerateAddress = 4;
                _generateNewAddress = true;
                EnableHighlightingAndDraggingForSnappedCubies(true);
            }
            else
            {
                var random = Random.Range(0, _ramGameManager.allPossibleGeneratedShapes.Count);
                Shape = Instantiate(_ramGameManager.allPossibleGeneratedShapes[random], SpawnShapesParent.transform);
                Shape.GetComponent<ShapeController>().SetLevelController(this);
                EnableHighlightingAndDraggingForSnappedCubies(false);
                gcBlocked = false;
                
            }
            _numberToGenerateAddress--;
        }
        
        /// <summary>
        /// Registers a snapped cubie in the tracking list and disables garbage collection temporarily
        /// </summary>
        /// <param name="snappedCubie">The cubie controller that was snapped into the grid</param>
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

        /// <summary>
        /// Runs the garbage collector, randomly removing a percentage of destroyable cubies
        /// </summary>
        public void CallGarbageCollector()
        {
            gcEnabled = false;
            var allDestroyableCubies = GetAllCanBeDestroyedCubies();
            
            //LOGIC FOR DESTROYING
            //removing 10% randomly
            float probbilityToRemoved = 0.2f;
            if(_numOfGCCalled > 1)  probbilityToRemoved = 0.4f;
            int numOfToBeDestroyedAddresses = Mathf.RoundToInt(allDestroyableCubies.Count * probbilityToRemoved);

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
            
            actualTimeGC = startTimeGC;
            _numOfGCCalled++;
        }

        private void EnableHighlightingAndDraggingForSnappedCubies(bool enabled)
        {
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
        
        /// <summary>
        /// Triggers level completion coroutine
        /// </summary>
        public void CallFinishState()
        {
            StartCoroutine(WaitToShowCompleteLevel());
        }
    }
}
