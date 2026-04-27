using Games.Cooling;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Games.Ram
{
    /// <summary>
    /// Level controller for the cooling minigame, manages CPU/GPU cooler sliders, scenarios and completion
    /// </summary>
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

        private CoolingGameManager _coolingGameManager;
        private ScenarioData _actualScenario;
        
        private bool _cpuIsOptimal = false;
        private bool _gpuIsOptimal = false;
        
        private Slider _cpuSlider;
        private Slider _gpuSlider;
        private float _cpuSliderToValueChangeCounter;
        private float _gpuSliderToValueChangeCounter;
        private float _cpuSliderNewValue;
        private float _gpuSliderNewValue;
        private bool _cpuSliderMoving;
        private bool _gpuSliderMoving;
        private const float MaxSliderToValueChangeCounter = 2.5f;
        private const float SliderUpdateStep = 0.001f;

        public override void Init()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            
            _coolingGameManager = FindFirstObjectByType<CoolingGameManager>();
            
            monitorController.SetScenario(startScenarioIndex);
            UpdateDataWhenScenarioChange();
            
            //GET SLIDERS
            _cpuSlider = sliders.transform.GetChild(0).gameObject.GetComponent<Slider>();
            _gpuSlider = sliders.transform.GetChild(1).gameObject.GetComponent<Slider>();
            _cpuSlider.enabled = !tutorialState;
            _gpuSlider.enabled = !tutorialState;
            _cpuSliderToValueChangeCounter = Random.Range(1, MaxSliderToValueChangeCounter);
            _gpuSliderToValueChangeCounter = Random.Range(1, MaxSliderToValueChangeCounter);
            
            sliders.gameObject.SetActive(showSliders);
            
            base.Init();
        }

        private void Update()
        {
            if(cpuController == null || gpuController == null ||
               _cpuSlider == null || _gpuSlider == null) return;

            if (numOfRepetitions == 0 && !tutorialState)
            {
                CallFinishState();
                numOfRepetitions = 3;
                return;
            }
            
            //RANDOM MOVING OF SLIDERS
            if (slidersMoving)
            {
                if (_cpuSliderMoving)
                {
                    if (Mathf.Abs(_cpuSlider.value - _cpuSliderNewValue) >= 0.01)
                    {
                        if(_cpuSlider.value < _cpuSliderNewValue)
                            _cpuSlider.value += SliderUpdateStep;
                        else
                        {
                            _cpuSlider.value -= SliderUpdateStep;
                        }
                    }
                    else
                    {
                        _cpuSliderMoving = false;
                        _cpuSlider.value = _cpuSliderNewValue;
                    }
                }
                else
                {
                    if (_cpuSliderToValueChangeCounter > 0)
                    {
                        _cpuSliderToValueChangeCounter -= SliderUpdateStep;
                    }
                    else
                    {
                        _cpuSliderMoving = true;
                        _cpuSliderToValueChangeCounter = Random.Range(1f, MaxSliderToValueChangeCounter);
                        _cpuSliderNewValue = Random.Range(0f, 1f);
                    }
                }
                
                if (_gpuSliderMoving)
                {
                    if (Mathf.Abs(_gpuSlider.value - _gpuSliderNewValue) >= 0.01)
                    {
                        if(_gpuSlider.value < _gpuSliderNewValue)
                            _gpuSlider.value += SliderUpdateStep;
                        else
                        {
                            _gpuSlider.value -= SliderUpdateStep;
                        }
                    }
                    else
                    {
                        _gpuSliderMoving = false;
                        _gpuSlider.value = _gpuSliderNewValue;
                    }
                }
                else
                {
                    if (_gpuSliderToValueChangeCounter > 0)
                    {
                        _gpuSliderToValueChangeCounter -= SliderUpdateStep;
                    }
                    else
                    {
                        _gpuSliderMoving = true;
                        _gpuSliderToValueChangeCounter = Random.Range(1f, MaxSliderToValueChangeCounter);
                        _gpuSliderNewValue = Random.Range(0f, 1f);
                    }
                }
            }

            if (tutorialState) return;
            
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

        /// <summary>
        /// Updates cooler controllers with new scenario temperature and offset data
        /// </summary>
        private void UpdateDataWhenScenarioChange()
        {
            if (numOfRepetitions == 0 && !tutorialState)
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
        
        /// <summary>
        /// Triggers level completion coroutine
        /// </summary>
        private void CallFinishState()
        {
            StartCoroutine(WaitToShowCompleteLevel());
        }
    }
}
