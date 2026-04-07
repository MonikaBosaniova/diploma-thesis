using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public Color ActiveColor;
    public Color InactiveColor;
    public Image flagCZ;
    public Image flagSK;
    public Image flagEN;

    private const string PLAYER_PREFS_KEY = "Language";
    void Start()
    {
        LocalizationSettings.InitializationOperation.Completed += (op) => {
            string language = PlayerPrefs.GetString(PLAYER_PREFS_KEY, "cs-CZ");
            switch (language)
            {
                case "sk-SK": SetLanguageSK(); break;
                case "en-US": SetLanguageEN(); break;
                case "cs-CZ":  default: SetLanguageCZ(); break;
            }
        };
    }
    
    public void SetLanguageCZ()
    {
        SetLanguage("cs-CZ");
        flagCZ.color = ActiveColor;
        flagSK.color = InactiveColor;
        flagEN.color = InactiveColor;
    }
    public void SetLanguageSK(){
        SetLanguage("sk-SK");
        flagCZ.color = InactiveColor;
        flagSK.color = ActiveColor;
        flagEN.color = InactiveColor;
    }
    public void SetLanguageEN(){
        SetLanguage("en-US");
        flagCZ.color = InactiveColor;
        flagSK.color = InactiveColor;
        flagEN.color = ActiveColor;
    }

    void SetLanguage(string languageIdentifier)
    {
        PlayerPrefs.SetString(PLAYER_PREFS_KEY, languageIdentifier);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(languageIdentifier);
        LoggerService.Instance.Log("LocalizationManager: Selected locale: " + languageIdentifier);
    }
}
