using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

/// <summary>
/// Startup locale selector that reads the preferred language from PlayerPrefs.
/// Automatically injects itself into LocalizationSettings startup selectors
/// before initialization, ensuring the correct locale is selected from the start
/// and preventing invalid operation handle errors in GameObjectLocalizer.
/// </summary>
public class PlayerPrefsLocaleSelector : IStartupLocaleSelector
{
    private const string PLAYER_PREFS_KEY = "Language";
    private const string DEFAULT_LOCALE = "cs-CZ";

    /// <summary>
    /// Returns the startup locale based on the saved language preference in PlayerPrefs.
    /// Called during localization system initialization.
    /// </summary>
    /// <param name="availableLocales">Provider of available locales in the project</param>
    /// <returns>The locale matching the saved language preference, or null if not found</returns>
    public Locale GetStartupLocale(ILocalesProvider availableLocales)
    {
        string language = PlayerPrefs.GetString(PLAYER_PREFS_KEY, DEFAULT_LOCALE);
        return availableLocales.GetLocale(language);
    }

    /// <summary>
    /// Injects the PlayerPrefsLocaleSelector at the top of the startup selectors list
    /// before the localization system initializes. Runs automatically before scene load.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InjectSelector()
    {
        // Access the instance directly to avoid triggering initialization via the static property
        var settings = LocalizationSettings.GetInstanceDontCreateDefault();
        if (settings == null)
            return;

        var selectors = settings.GetStartupLocaleSelectors();

        // Avoid duplicates if already injected
        foreach (var sel in selectors)
        {
            if (sel is PlayerPrefsLocaleSelector)
                return;
        }

        // Insert at position 0 so it has the highest priority
        selectors.Insert(0, new PlayerPrefsLocaleSelector());
    }
}