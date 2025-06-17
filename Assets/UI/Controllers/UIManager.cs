using UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        var controllers = FindObjectsByType<UIControllerBase>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var controller in controllers)
        {
            controller.Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
