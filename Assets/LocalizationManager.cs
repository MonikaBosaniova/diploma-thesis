using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    public void SetLanguageCZ()
    {
        SetLanguage("cs-CZ");
    }
    public void SetLanguageSK(){
        SetLanguage("sk-SK");
    }
    public void SetLanguageEN(){
        SetLanguage("en-US");
    }

    void SetLanguage(string languageIdentifier)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(languageIdentifier);
        LoggerService.Instance.Log("Selected locale: " + languageIdentifier);
    }
}
