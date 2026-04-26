using System.Collections.Generic;
using UnityEngine;

namespace Games.Cooling
{
    /// <summary>
    /// Controls scenario selection and provides CPU/GPU target values for the cooling minigame
    /// </summary>
    public class MonitorController : MonoBehaviour
    {
        [SerializeField] private Transform scenariosParent;

        [Header("---DEBUG---")] 
        [SerializeField] private int indexOfActualScenario = 0;
        
        /// <summary>
        /// Randomly selects a new scenario different from the current one
        /// </summary>
        [ContextMenu("New Scenario")]
        internal void ChooseNewScenario()
        {
            var random  = Random.Range(0, scenariosParent.childCount);
            while (random == indexOfActualScenario)
            {
                random = Random.Range(0, scenariosParent.childCount);
            }
            
            SetScenario(random);
        }

        /// <summary>
        /// Returns the target temperature and offset for the CPU cooler
        /// </summary>
        /// <returns>List with [0] finish temperature and [1] offset</returns>
        internal List<float> GetCPUValuesForScenario()
        {
            ScenarioData scenarioData = scenariosParent.GetChild(indexOfActualScenario).GetComponent<ScenarioData>();
            return new List<float>{scenarioData.finishCoolerValueCPU , scenarioData.cpuOffset};
        }
        
        /// <summary>
        /// Returns the target temperature and offset for the GPU cooler
        /// </summary>
        /// <returns>List with [0] finish temperature and [1] offset</returns>
        internal List<float> GetGPUValuesForScenario()
        {
            ScenarioData scenarioData = scenariosParent.GetChild(indexOfActualScenario).GetComponent<ScenarioData>();
            return new List<float>{scenarioData.finishCoolerValueGPU , scenarioData.gpuOffset};
        }
        
        /// <summary>
        /// Activates a specific scenario by index and deactivates all others
        /// </summary>
        /// <param name="index">Index of the scenario to activate</param>
        internal void SetScenario(int index)
        {
            foreach (Transform scenario in scenariosParent)
            {
                scenario.gameObject.SetActive(false);
            }
            
            indexOfActualScenario = index;
            scenariosParent.GetChild(index).gameObject.SetActive(true);
        }
    }
}
