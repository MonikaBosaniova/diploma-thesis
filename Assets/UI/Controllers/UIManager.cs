using DialogueSystem;
using UI;
using UnityEngine;

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
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        // var controllers = FindObjectsByType<UIControllerBase>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        //
        // foreach (var controller in controllers)
        // {
        //     controller.Initialize();
        // }
    }

    public void ShowDialogWindowUI(DialogueSequence ds)
    {
        DialogWindowUI.gameObject.SetActive(true);
        //DialogWindowUI.Initialize();
        DialogWindowUI.ShowDialogue(ds);
    }
    
    public void HideDialogWindowUI()
    {
        DialogWindowUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
