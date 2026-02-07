using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    public class CoolingLevelController : LevelController
    {
        // [Header("Ram Level")] 
        // [SerializeField] private float Time;
        // [SerializeField] private GameObject Shape;
        // [SerializeField] private GameObject SpawnShapesParent;
        // [SerializeField] private GameObject ShapesDoneParent;
        // [SerializeField] private CPUWantedAddressController cpuWantedAddressController;
        //
        // [Header("Garbage collector")] 
        // [SerializeField] private bool gcEnabled = true;
        // [SerializeField] private bool gcBlocked = false;
        // [SerializeField] private float startTimeGC = 1;
        // [SerializeField] private float actualTimeGC;
        // [SerializeField] private float stepGC = 0.003f;
        // [SerializeField] private GameObject disabledGC;
        //
        // private int numberToGenerateAddress = 3;
        CoolingGameManager coolingGameManager;
        // private List<SnappedAddressCubieController> _allSnappedCubies = new List<SnappedAddressCubieController>();
        // private bool generateNewAddress = false;
        
        public override void Init()
        {
            coolingGameManager = FindFirstObjectByType<CoolingGameManager>();
            
            // for (int i = 0; i < transform.childCount; i++)
            // {
            //     transform.GetChild(i).gameObject.SetActive(true);
            // }
            //
            // cpuWantedAddressController.SetRamLevelController(this);
            // GenerateShape();
            
            base.Init();
        }

        private void Update()
        {
            // if (generateNewAddress)
            // {
            //     var allAddresses = GetAllAddresses();
            //     cpuWantedAddressController.GenerateWantedAddress(allAddresses);
            //     generateNewAddress = false;
            //     disabledGC.SetActive(true);
            //     gcBlocked = true;
            //     actualTimeGC = startTimeGC;
            // }
            //
            // if (gcBlocked || gcEnabled) return;
            //
            // actualTimeGC -= stepGC;
            // if (actualTimeGC <= 0)
            // {
            //     if(disabledGC != null)
            //         disabledGC.SetActive(false);
            //     gcEnabled = true;
            //     actualTimeGC = startTimeGC;
            // }
        }
        
        
        public override void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            base.Close();
        }
        
        private void CheckFinishState(double newValue)
        {
            StartCoroutine(WaitToShowCompleteLevel());
        }
        
    }
}
