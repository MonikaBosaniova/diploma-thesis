using UnityEngine;

namespace Games.Cooling
{
    /// <summary>
    /// Data container for a cooling scenario with target temperatures and offsets for CPU and GPU
    /// </summary>
    public class ScenarioData : MonoBehaviour
    {
        [SerializeField] internal float finishCoolerValueCPU;
        [SerializeField] internal float finishCoolerValueGPU;
        [SerializeField] internal float cpuOffset;
        [SerializeField] internal float gpuOffset;
    }
}
