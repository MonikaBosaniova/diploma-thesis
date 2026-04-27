using DialogueSystem;
using UI;
using UnityEngine;

/// <summary>
/// Singleton manager for UI Toolkit controllers, provides access to dialogue window display
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private DialogueSystemUIController DialogWindowUI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Activates and displays the dialogue window with the given dialogue sequence
    /// </summary>
    /// <param name="ds">Dialogue sequence to display</param>
    public void ShowDialogWindowUI(DialogueSequence ds)
    {
        DialogWindowUI.gameObject.SetActive(true);
        DialogWindowUI.ShowDialogue(ds);
    }
    
    /// <summary>
    /// Hides the dialogue window UI
    /// </summary>
    public void HideDialogWindowUI()
    {
        DialogWindowUI.gameObject.SetActive(false);
    }
}
