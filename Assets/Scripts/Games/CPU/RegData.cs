using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

namespace Games.CPU
{
    public class RegData : MonoBehaviour
    {
        [SerializeField] internal bool snapped = false;
        [SerializeField] internal Transform _regParent;
        [SerializeField] internal RegDataType type;
        [SerializeField] internal float value;
        [SerializeField] internal float eqValue;
        
        public TMP_Text variableName;
        public TMP_Text valueText;
        public GameObject correctSprite;
        public GameObject incorrectSprite;
        
        Dictionary<RegDataType, string> variableNames = new Dictionary<RegDataType, string>()
        {
            { RegDataType.None , "None" },
            { RegDataType.ManaLeft , "Mana"},
            { RegDataType.Cost , "Cena"},
            { RegDataType.PlusRes , "Plus"},
            { RegDataType.MinusRes , "Minus"},
            { RegDataType.MultiplyRes , "Násobení"},
            { RegDataType.LesserThanZeroRes , "Míň než 0"},
            { RegDataType.BiggerThanZeroRes , "Víc než 0"},
            
        };
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //TODO POLISH
            //if(type == RegDataType.Cost || type == RegDataType.ManaLeft)
                //value = Random.Range(0, 16);
            //HARDCODED
            if (type == RegDataType.ManaLeft)
            {
                value = 12f;
            }

            if (type == RegDataType.Cost)
            {
                value = 10f;
            }
            
            // if (type == RegDataType.BiggerThanZeroRes)
            // {
            //     eqValue = value;
            // }
            
            valueText.text = value.ToString(CultureInfo.InvariantCulture);
            variableName.text = variableNames[type];
            
            if (type == RegDataType.LesserThanZeroRes || type == RegDataType.BiggerThanZeroRes)
            {
                if (Mathf.Approximately(value, 1))
                {
                    correctSprite.SetActive(true);
                    incorrectSprite.SetActive(false);
                }
                else
                {
                    correctSprite.SetActive(false);
                    incorrectSprite.SetActive(true);
                }
            }
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
