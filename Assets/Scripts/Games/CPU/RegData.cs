using NUnit.Framework.Constraints;
using UnityEngine;

namespace Games.CPU
{
    public class RegData : MonoBehaviour
    {
        [SerializeField] internal bool snapped = false;
        [SerializeField] internal Transform _regParent;
        [SerializeField] internal RegDataType type;
        [SerializeField] internal float value;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //TODO POLISH
            //if(type == RegDataType.Cost || type == RegDataType.ManaLeft)
                //value = Random.Range(0, 16);
            //HARDCODED
                if (type == RegDataType.ManaLeft)
                    value = 12f;
                if(type == RegDataType.Cost)
                    value = 10f;
                // if (type == RegDataType.PlusRes)
                //     value = 22;
                // if (type == RegDataType.MinusRes)
                //     value = 2f;
                // if (type == RegDataType.MultiplyRes)
                //     value = 120f;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
