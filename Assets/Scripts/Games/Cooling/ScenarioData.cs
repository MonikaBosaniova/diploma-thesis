using UnityEngine;

namespace Games.Cooling
{
    public class ScenarioData : MonoBehaviour
    {
        [Tooltip("Value between 0 and 1")]
        [SerializeField] internal float optimumCoolerValueCPU;
        [SerializeField] internal float optimumCoolerValueGPU;
        
    }
    
}
