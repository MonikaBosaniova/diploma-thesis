using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public void SetAllSSdButtonsToValue(bool value)
    {
        if (_ssdButtons == null) return;

        foreach (var ssdButton in _ssdButtons)
        {
            ssdButton.SetModelValue(value);
        }
    }

    public void SetIndexOfSelection(int index)
    {
        indexOfSelection = index;
        OnSelectionChange?.Invoke(index);
    }
}
