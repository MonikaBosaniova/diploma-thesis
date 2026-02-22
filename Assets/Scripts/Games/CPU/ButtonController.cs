using System;
using TMPro;
using UnityEngine;

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

    public void SetButtonEnabled(bool enabled)
    {
        EnabledButton.SetActive(enabled);
        DisabledButton.SetActive(!enabled);
        
        ButtonText.color = enabled ? EnabledTextColor : DisabledTextColor;
        
        ClickableObject.enabled = enabled;
    }
}
