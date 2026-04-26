using UnityEngine;

/// <summary>
/// Controls a single SSD selection button with on/off visual states and connecting line
/// </summary>
public class SsdButton : MonoBehaviour
{
    [SerializeField] private SsdButtonsController controller;
    [SerializeField] private GameObject onModel;
    [SerializeField] private GameObject offModel;
    [SerializeField] private GameObject line;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = transform.parent.GetComponent<SsdButtonsController>();
        onModel.SetActive(false);
        offModel.SetActive(true);
        line.SetActive(false);
    }

    /// <summary>
    /// Activates this button, deactivates all others, and notifies the controller
    /// </summary>
    public void TurnOn()
    {
        controller.SetAllSSdButtonsToValue(false);
        controller.SetIndexOfSelection(transform.GetSiblingIndex());
        SetModelValue(true);
    }

    /// <summary>
    /// Sets the on/off visual state and line visibility
    /// </summary>
    /// <param name="isOn">Whether the button is active</param>
    public void SetModelValue(bool isOn)
    {
        onModel.SetActive(isOn);
        offModel.SetActive(!isOn);
        line.SetActive(isOn);
    }

    /// <summary>
    /// Adjusts the connecting line scale and position for proper visual alignment
    /// </summary>
    /// <param name="scaleX">X scale of the line</param>
    /// <param name="posZ">Z position of the line</param>
    public void SetLineScaling(float scaleX, float posZ)
    {
        var scale = line.transform.localScale;
        scale.x = scaleX;
        line.transform.localScale = scale;
        
        var pos = line.transform.localPosition;
        pos.z = posZ;
        line.transform.localPosition = pos;
    }
}
