using NUnit.Framework.Constraints;
using UnityEngine;

namespace Games.CPU
{
    public class RegData : MonoBehaviour
    {
        internal bool snapped = false;
        internal Transform _regParent;
        internal RegDataType type;
        internal float value;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            value = Random.Range(0, 16);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
