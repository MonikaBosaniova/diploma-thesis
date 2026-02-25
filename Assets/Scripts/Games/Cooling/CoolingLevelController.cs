using System;
using System.Collections.Generic;
using System.Linq;
using Games.Cooling;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    public class CoolingLevelController : LevelController
    {
        [Header("Tutorial simulation")]
        public bool tutorialState = false;
        public bool showSliders = true;
        public bool slidersMoving = false;

        [Header("Minigame")] 
        [SerializeField] private int startScenarioIndex = 0;
        [SerializeField] internal GameObject sliders;
        [SerializeField] private MonitorController  monitorController;
        [SerializeField] private CoolerController  cpuController;
        [SerializeField] private CoolerController  gpuController;

        [SerializeField] private GameObject NoisyBubble;
        [SerializeField] private GameObject slowBubble;
        
        [SerializeField] private int numOfRepetitions;
       
        CoolingGameManager coolingGameManager;
        private ScenarioData actualScenario;
        
        private bool cpuIsOptimal = false;
        private bool gpuIsOptimal = false;
        
        private Slider cpuSlider;
        private Slider gpuSlider;
        private float cpuSliderToValueChangeCounter;
        private float gpuSliderToValueChangeCounter;
        private float cpuSliderNewValue;
        private float gpuSliderNewValue;
        private bool cpuSliderMoving;
        private bool gpuSliderMoving;
        private const float maxSliderToValueChangeCounter = 2.5f;
        private const float sliderUpdateStep = 0.001f;

        public override void Init()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
            coolingGameManager = FindFirstObjectByType<CoolingGameManager>();
            
            monitorController.SetScenario(startScenarioIndex);
            UpdateDataWhenScenarioChange();
            
            //GET SLIDERS
            cpuSlider = sliders.transform.GetChild(0).gameObject.GetComponent<Slider>();
            gpuSlider = sliders.transform.GetChild(1).gameObject.GetComponent<Slider>();
            cpuSlider.enabled = !tutorialState;
            gpuSlider.enabled = !tutorialState;
            cpuSliderToValueChangeCounter = Random.Range(1, maxSliderToValueChangeCounter);
            gpuSliderToValueChangeCounter = Random.Range(1, maxSliderToValueChangeCounter);
            
            sliders.gameObject.SetActive(showSliders);
            
            base.Init();
        }

        private void Update()
        {
            if(cpuController == null || gpuController == null ||
               cpuSlider == null || gpuSlider == null) return;

            if (numOfRepetitions == 0)
            {
                CallFinishState();
                return;
            }
            
            //RANDOM MOVING OF SLIDERS
            if (slidersMoving)
            {
                if (cpuSliderMoving)
                {
                    if (Mathf.Abs(cpuSlider.value - cpuSliderNewValue) >= 0.01)
                    {
                        if(cpuSlider.value < cpuSliderNewValue)
                            cpuSlider.value += sliderUpdateStep;
                        else
                        {
                            cpuSlider.value -= sliderUpdateStep;
                        }
                    }
                    else
                    {
                        cpuSliderMoving = false;
                        cpuSlider.value = cpuSliderNewValue;
                    }
                }
                else
                {
                    if (cpuSliderToValueChangeCounter > 0)
                    {
                        cpuSliderToValueChangeCounter -= sliderUpdateStep;
                    }
                    else
                    {
                        cpuSliderMoving = true;
                        cpuSliderToValueChangeCounter = Random.Range(1f, maxSliderToValueChangeCounter);
                        cpuSliderNewValue = Random.Range(0f, 1f);
                    }
                }
                
                if (gpuSliderMoving)
                {
                    if (Mathf.Abs(gpuSlider.value - gpuSliderNewValue) >= 0.01)
                    {
                        if(gpuSlider.value < gpuSliderNewValue)
                            gpuSlider.value += sliderUpdateStep;
                        else
                        {
                            gpuSlider.value -= sliderUpdateStep;
                        }
                    }
                    else
                    {
                        gpuSliderMoving = false;
                        gpuSlider.value = gpuSliderNewValue;
                    }
                }
                else
                {
                    if (gpuSliderToValueChangeCounter > 0)
                    {
                        gpuSliderToValueChangeCounter -= sliderUpdateStep;
                    }
                    else
                    {
                        gpuSliderMoving = true;
                        gpuSliderToValueChangeCounter = Random.Range(1f, maxSliderToValueChangeCounter);
                        gpuSliderNewValue = Random.Range(0f, 1f);
                    }
                }
            }
            
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
                slowBubble.SetActive(true);
            }
            else
            {
                slowBubble.SetActive(false);
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
            if (numOfRepetitions == 0)
            {
                CallFinishState();
                return;
            }
            var CPUValues = monitorController.GetCPUValuesForScenario();
            var GPUValues = monitorController.GetGPUValuesForScenario();
            cpuController.SetScenarioData(CPUValues[0], CPUValues[1]);
            gpuController.SetScenarioData(GPUValues[0], GPUValues[1]);
            numOfRepetitions--;
        }
        
        private void CallFinishState()
        {
            StartCoroutine(WaitToShowCompleteLevel());
        }

        // private float MakeSlidersToMoveRandomly(float sliderCounter, Slider slider)
        // {
        //     
        // }
    }
}
