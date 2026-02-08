using System;
using System.Collections.Generic;
using System.Linq;
using Games.Cooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    public class CoolingLevelController : LevelController
    {
        [SerializeField] private MonitorController  monitorController;
        [SerializeField] private CoolerController  cpuController;
        [SerializeField] private CoolerController  gpuController;

        [SerializeField] private GameObject NoisyBubble;
        [SerializeField] private GameObject SlowBubble;
       
        CoolingGameManager coolingGameManager;
        private ScenarioData actualScenario;
        
        private bool cpuIsOptimal = false;
        private bool gpuIsOptimal = false;
        
        public override void Init()
        {
            coolingGameManager = FindFirstObjectByType<CoolingGameManager>();
            
            monitorController.SetScenario(0);

           UpdateDataWhenScenarioChange();
            
            base.Init();
        }

        private void Update()
        {
            if(cpuController == null || gpuController == null) return;
            
            if (cpuController.optimal && gpuController.optimal)
            {
                monitorController.ChooseNewScenario();
                UpdateDataWhenScenarioChange();
            }

            if (cpuController.noisy || gpuController.noisy)
            {
                NoisyBubble.SetActive(true);
            }
            else
            {
                NoisyBubble.SetActive(false);
            }
            
            if (cpuController.slow || gpuController.slow)
            {
                SlowBubble.SetActive(true);
            }
            else
            {
                SlowBubble.SetActive(false);
            }
        }

        public override void Close()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            base.Close();
        }

        private void UpdateDataWhenScenarioChange()
        {
            var CPUValues = monitorController.GetCPUValuesForScenario();
            var GPUValues = monitorController.GetGPUValuesForScenario();
            cpuController.SetScenarioData(CPUValues[0], CPUValues[1]);
            gpuController.SetScenarioData(GPUValues[0], GPUValues[1]);
        }
        
        private void CheckFinishState(double newValue)
        {
            StartCoroutine(WaitToShowCompleteLevel());
        }
        
    }
}
