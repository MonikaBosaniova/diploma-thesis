using System;
using Gates;
using UnityEngine;

public class BinaryCalculator : MonoBehaviour
{
    [SerializeField] public double _finalValue = 0;
    [SerializeField] private double _decValue = 0;
    [SerializeField] private bool isBinToDec = true;
    
    [Header("Binary Inputs")]
    [SerializeField] private SwitchController _switch0;
    [SerializeField] private SwitchController _switch1;
    [SerializeField] private SwitchController _switch2;
    [SerializeField] private SwitchController _switch3;
    
    [Header("Binary Outputs")]
    [SerializeField] private GameObject _binaryValueHelper0;
    [SerializeField] private GameObject _binaryValueHelper1;
    [SerializeField] private GameObject _binaryValueHelper2;
    [SerializeField] private GameObject _binaryValueHelper3;
    
    [Header("Dec Outputs")]
    [SerializeField] private GameObject _dec_digit_1;
    [SerializeField] private GameObject _dec_digit_10;
    
    [Header("Remainder Outputs")]
    [SerializeField] private GameObject _remainder_digit_1;
    [SerializeField] private GameObject _remainder_digit_10;
    [SerializeField] private GameObject _remainder_minus;

    
    public Action<double> OnDecValueChanged;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _switch0.OnValueChanged += (newValue) => ComputeDecValue(0, newValue);
        _switch1.OnValueChanged += (newValue) => ComputeDecValue(1, newValue);
        _switch2.OnValueChanged += (newValue) => ComputeDecValue(2, newValue);
        _switch3.OnValueChanged += (newValue) => ComputeDecValue(3, newValue);
        
        _switch0.OnValueChanged += (newValue) => ShowHelperNumber(_binaryValueHelper0, newValue);
        _switch1.OnValueChanged += (newValue) => ShowHelperNumber(_binaryValueHelper1, newValue);
        _switch2.OnValueChanged += (newValue) => ShowHelperNumber(_binaryValueHelper2 ,newValue);
        _switch3.OnValueChanged += (newValue) => ShowHelperNumber(_binaryValueHelper3 ,newValue);
    }
    public void VisualizeDecValue(double value, bool settingResult)
    {
        Debug.Log("VISUALIZE DEC VALUE: " + value);
        Transform digit01;
        Transform digit10;
        
        if (settingResult)
        {
            digit01 = _dec_digit_1.transform;
            digit10 = _dec_digit_10.transform;
        }
        else
        {
            digit01 = _remainder_digit_1.transform;
            digit10 = _remainder_digit_10.transform;
            _remainder_minus.SetActive(value < 0);
            if(value < 0) value *= -1;
        }
            
        var indexOfDecDigit_1 = 0d;
        
        if (value >= 10)
        {
            digit10.GetChild(0).gameObject.SetActive(false);
            digit10.GetChild(1).gameObject.SetActive(true);
            indexOfDecDigit_1 = Math.Abs(value - 10);
        }
        else
        {
            digit10.GetChild(0).gameObject.SetActive(true);
            digit10.GetChild(1).gameObject.SetActive(false);
            indexOfDecDigit_1 = value;

        }

        for (int i = 0; i < digit01.childCount; i++)
        {
            digit01.GetChild(i).gameObject.SetActive(false);
        }
        Debug.Log(indexOfDecDigit_1 + " ... " + digit01.name + "  /  " + digit01.childCount);
        digit01.GetChild((int)indexOfDecDigit_1).gameObject.SetActive(true);
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
        if(isBinToDec)
            VisualizeDecValue(_decValue, true);
        else
            VisualizeDecValue(_finalValue - _decValue, false);
        
        OnDecValueChanged?.Invoke(_decValue);
    }
}
