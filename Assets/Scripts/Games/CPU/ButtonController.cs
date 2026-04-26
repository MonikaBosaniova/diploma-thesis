using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls a button's enabled/disabled visual state and click interaction
/// </summary>
public class ButtonController : MonoBehaviour
{
    public ClickableObject ClickableObject;
    public bool InitEnabledState;

    public GameObject EnabledButton;
    public GameObject DisabledButton;
    public TMP_Text ButtonText;
    
    public Color EnabledTextColor;
    public Color DisabledTextColor;

    private void Start()
    {
        SetButtonEnabled(InitEnabledState);
    }

    /// <summary>
    /// Sets the button enabled/disabled state, swaps visuals and text color
    /// </summary>
    /// <param name="enabled">Whether the button should be enabled</param>
    public void SetButtonEnabled(bool enabled)
    {
        EnabledButton.SetActive(enabled);
        DisabledButton.SetActive(!enabled);
        
        ButtonText.color = enabled ? EnabledTextColor : DisabledTextColor;
        
        ClickableObject.enabled = enabled;
    }
}
