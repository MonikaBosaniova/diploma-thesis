using System;
using Gates;
using UnityEngine;

public class BinaryCalculator : MonoBehaviour
{
    [Header("Binary Inputs")]
    [SerializeField] private SwitchController _switch0;
    [SerializeField] private SwitchController _switch1;
    [SerializeField] private SwitchController _switch2;
    [SerializeField] private SwitchController _switch3;
    
    [Header("Dec Outputs")]
    [SerializeField] private GameObject _dec_digit_1;
    [SerializeField] private GameObject _dec_digit_10;

    [SerializeField] private double _decValue = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _switch0.OnValueChanged += (newValue) => ComputeDecValue(0, newValue);
        _switch1.OnValueChanged += (newValue) => ComputeDecValue(1, newValue);
        _switch2.OnValueChanged += (newValue) => ComputeDecValue(2, newValue);
        _switch3.OnValueChanged += (newValue) => ComputeDecValue(3, newValue);
    }

    private void ComputeDecValue(float index, bool value)
    {
        var newValue = Math.Pow(2f,index);
        if (value)
            _decValue += newValue;
        else
            _decValue -= newValue;
        Debug.Log("Dec Value: " + _decValue + ", Index: " + index + ", Value: " + value);
        VisualizeDecValue();
    }

    private void VisualizeDecValue()
    {
        var indexOfDecDigit_1 = 0d;
        if (_decValue >= 10)
        {
            _dec_digit_10.transform.GetChild(0).gameObject.SetActive(false);
            _dec_digit_10.transform.GetChild(1).gameObject.SetActive(true);
            indexOfDecDigit_1 = Math.Abs(_decValue - 10);
        }
        else
        {
            _dec_digit_10.transform.GetChild(0).gameObject.SetActive(true);
            _dec_digit_10.transform.GetChild(1).gameObject.SetActive(false);
            indexOfDecDigit_1 = _decValue;

        }

        for (int i = 0; i < _dec_digit_1.transform.childCount; i++)
        {
            _dec_digit_1.transform.GetChild(i).gameObject.SetActive(false);
        }
        _dec_digit_1.transform.GetChild((int)indexOfDecDigit_1).gameObject.SetActive(true);
    }
    
}
