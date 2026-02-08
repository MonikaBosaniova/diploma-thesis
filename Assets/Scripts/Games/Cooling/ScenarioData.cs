using UnityEngine;

namespace Games.Cooling
{
    public class ScenarioData : MonoBehaviour
    {
        //[Tooltip("Value between 0 and 1")]
        [SerializeField] internal float finishCoolerValueCPU;
        [SerializeField] internal float finishCoolerValueGPU;
        [SerializeField] internal float cpuOffset;
        [SerializeField] internal float gpuOffset;

    }
    
}
