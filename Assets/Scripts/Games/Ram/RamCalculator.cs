using System;
using Gates;
using UnityEngine;

public class RamCalculator : MonoBehaviour
{
    [SerializeField] private double _decValue = 0;
    
    public Action<double> OnDecValueChanged;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    private void ShowHelperNumber(GameObject binHelperNumberParent, bool value)
    {
        binHelperNumberParent.transform.GetChild(0).gameObject.SetActive(!value);
        binHelperNumberParent.transform.GetChild(1).gameObject.SetActive(value);
    }

    private void ComputeDecValue(float index, bool value)
    {
        var newValue = Math.Pow(2f,index);
        if (value)
            _decValue += newValue;
        else
            _decValue -= newValue;
        VisualizeDecValue();
        OnDecValueChanged?.Invoke(_decValue);
    }

    private void VisualizeDecValue()
    {
    }

}
