using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

/// <summary>
/// Manages language localization and flag UI visuals for Czech, Slovak and English languages
/// </summary>
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
    
    /// <summary>
    /// Sets the language to Czech and updates flag colors
    /// </summary>
    public void SetLanguageCZ()
    {
        SetLanguage("cs-CZ");
        flagCZ.color = ActiveColor;
        flagSK.color = InactiveColor;
        flagEN.color = InactiveColor;
    }
    /// <summary>
    /// Sets the language to Slovak and updates flag colors
    /// </summary>
    public void SetLanguageSK(){
        SetLanguage("sk-SK");
        flagCZ.color = InactiveColor;
        flagSK.color = ActiveColor;
        flagEN.color = InactiveColor;
    }
    /// <summary>
    /// Sets the language to English and updates flag colors
    /// </summary>
    public void SetLanguageEN(){
        SetLanguage("en-US");
        flagCZ.color = InactiveColor;
        flagSK.color = InactiveColor;
        flagEN.color = ActiveColor;
    }

    /// <summary>
    /// Sets the locale by identifier, saves to PlayerPrefs and logs the change
    /// </summary>
    /// <param name="languageIdentifier">Locale identifier (e.g. "cs-CZ", "sk-SK", "en-US")</param>
    void SetLanguage(string languageIdentifier)
    {
        PlayerPrefs.SetString(PLAYER_PREFS_KEY, languageIdentifier);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(languageIdentifier);
        LoggerService.Instance.Log("LocalizationManager: Selected locale: " + languageIdentifier);
    }
}
