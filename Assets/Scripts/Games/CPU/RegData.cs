using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Serialization;

namespace Games.CPU
{
    /// <summary>
    /// Represents data which can be draggable across cache
    /// </summary>
    public class RegData : MonoBehaviour
    {
        [SerializeField] internal bool snapped = false;
        [FormerlySerializedAs("_regParent")] [SerializeField] internal Transform regParent;
        [SerializeField] internal RegDataType type;
        [SerializeField] internal float value;
        [SerializeField] internal float eqValue;
        
        public TMP_Text variableName;
        public TMP_Text valueText;
        public GameObject correctSprite;
        public GameObject incorrectSprite;
        
        Dictionary<RegDataType, List<string>> _dataTranslations = new Dictionary<RegDataType, List<string>>()
        {
            {RegDataType.None, new List<string>(){"None", "None", "None"}},
            { RegDataType.ManaLeft, new List<string>(){"Mana", "Mana", "Mana"}},
            { RegDataType.Cost, new List<string>(){"Cena", "Cost", "Cena"}},
            { RegDataType.PlusRes, new List<string>(){"Plus", "Plus", "Plus"}},
            { RegDataType.MinusRes, new List<string>(){"Minus", "Minus", "Mínus"}},
            { RegDataType.MultiplyRes, new List<string>(){"Násobení", "Multiply", "Násobenie"}},
            { RegDataType.BiggerThanZeroRes, new List<string>(){"Víc než 0", "Bigger than 0", "Viac než 0"}},
            { RegDataType.LesserThanZeroRes , new List<string>(){"Míň než 0", "Less than 0", "Menej než 0"}},
        };
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //HARDCODED FOR SCENARIO
            if (type == RegDataType.ManaLeft)
            {
                value = 12f;
            }

            if (type == RegDataType.Cost)
            {
                value = 10f;
            }
            
            //Localization
            valueText.text = value.ToString(CultureInfo.InvariantCulture);
            var localeIndex = LocalizationSettings.AvailableLocales.Locales
                .IndexOf(LocalizationSettings.SelectedLocale);
            variableName.text = _dataTranslations[type].ElementAt(localeIndex);
            
            //showing image of value in boolean data
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
        }
    }
}
