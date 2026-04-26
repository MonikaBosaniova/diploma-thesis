using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages a group of SSD selection buttons, handles selection state and broadcasts selection changes
/// </summary>
public class SsdButtonsController : MonoBehaviour
{
    public int indexOfSelection;
    
    public Action<int> OnSelectionChange;
    
    private List<SsdButton> _ssdButtons;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _ssdButtons = GetComponentsInChildren<SsdButton>().ToList();
    }

    /// <summary>
    /// Sets all SSD buttons to the specified visual state
    /// </summary>
    /// <param name="value">True to turn on, false to turn off</param>
    public void SetAllSSdButtonsToValue(bool value)
    {
        if (_ssdButtons == null) return;

        foreach (var ssdButton in _ssdButtons)
        {
            ssdButton.SetModelValue(value);
        }
    }

    /// <summary>
    /// Updates the selected index and invokes the selection change event
    /// </summary>
    /// <param name="index">Index of the selected button</param>
    public void SetIndexOfSelection(int index)
    {
        indexOfSelection = index;
        OnSelectionChange?.Invoke(index);
    }
}
