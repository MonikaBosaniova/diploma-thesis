using Gates;
using UnityEngine;

/// <summary>
/// Helper for the RAM presentation, simulates data volatility and garbage collection visuals
/// </summary>
public class RamPresentationHelper : MonoBehaviour
{
    public SwitchController switchController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(switchController != null)
            switchController.OnValueChanged += RamElectricInputChanged;
    }

    /// <summary>
    /// Simulates garbage collection by randomly hiding one child object
    /// </summary>
    public void CallGC()
    {
        if (transform.childCount == 0) return;
        
        var random = Random.Range(0, transform.childCount);
        transform.GetChild(random).gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Handles the switch input change, generates or clears RAM visualization
    /// </summary>
    /// <param name="obj">True for power on (generate), false for power off (clear)</param>
    private void RamElectricInputChanged(bool obj)
    {
        if (obj)
            GenerateNewObjects();
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Randomly activates/deactivates child objects to simulate RAM data
    /// </summary>
    private void GenerateNewObjects()
    {
        var possibilityOfTurnedOn = 0.5f;
        
        foreach (Transform child in transform)
        {
            var random = Random.Range(0f, 1f);
            child.gameObject.SetActive(random > possibilityOfTurnedOn);
        }
        
    }
}
