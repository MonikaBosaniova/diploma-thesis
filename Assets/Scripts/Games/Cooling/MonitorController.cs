using System.Collections.Generic;
using UnityEngine;

namespace Games.Cooling
{
    public class MonitorController : MonoBehaviour
    {
        [SerializeField] private Transform scenariosParent;

        [Header("---DEBUG---")] 
        [SerializeField] private int indexOfActualScenario = 0;
        
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

        internal List<float> GetCPUValuesForScenario()
        {
            ScenarioData scenarioData = scenariosParent.GetChild(indexOfActualScenario).GetComponent<ScenarioData>();
            return new List<float>{scenarioData.finishCoolerValueCPU , scenarioData.cpuOffset};
        }
        
        internal List<float> GetGPUValuesForScenario()
        {
            ScenarioData scenarioData = scenariosParent.GetChild(indexOfActualScenario).GetComponent<ScenarioData>();
            return new List<float>{scenarioData.finishCoolerValueGPU , scenarioData.gpuOffset};
        }
        
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
