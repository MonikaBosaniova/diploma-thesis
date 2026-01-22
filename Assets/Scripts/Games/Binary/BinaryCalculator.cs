using System;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private GameObject _wires;
    private List<SwitchController> _allSwitches;
    
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
    
    [Header("Wrong Answer")]
    [SerializeField] private GameObject _wrongAnswer;

    
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
        
        _allSwitches = new List<SwitchController>()
        {
            _switch0,
            _switch1,
            _switch2,
            _switch3,
        };
    }
    
    public void VisualizeDecValue(double value, bool settingResult)
    {
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
        digit01.GetChild((int)indexOfDecDigit_1).gameObject.SetActive(true);
    }

    public void GenerateRandomBinNumber()
    {
        HideWires();
        
        for (int i = 0; i < 4; i++)
        {
            var random = UnityEngine.Random.Range(0, 2);
            if (random == 1)
            {
                _allSwitches.ElementAt(i).SwitchValue();
            }
            _allSwitches.ElementAt(i).gameObject.SetActive(false);
        }
        
        _finalValue = _decValue;
        _decValue = 0;
        VisualizeDecValue(0, true);
    }

    public void AddToDecValue(int value)
    {
        _wrongAnswer.SetActive(false);
        _decValue += value;
        _decValue = Mathf.Clamp((int)_decValue, 0, 15);
        
        VisualizeDecValue(_decValue, true);
    }

    public void SubmitAnswer()
    {
        if (Math.Abs(_finalValue - _decValue) < 0.1)
        {
            Debug.Log("Submit answer: " + _finalValue + " - " + _decValue);
            OnDecValueChanged?.Invoke(_decValue);
            _finalValue = -1;
        }
        else
        {
            _wrongAnswer.SetActive(true);
        }
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
        if (isBinToDec)
        {
            VisualizeDecValue(_decValue, true);
        }
        else
        {
            VisualizeDecValue(_finalValue - _decValue, false);
            if (Math.Abs(_finalValue - _decValue) < 0.1)
            {
                Debug.Log("COMPUTE VALUE: " + _finalValue + " - " + _decValue);
                OnDecValueChanged?.Invoke(_decValue);
                _finalValue = -1;
            }
        }
    }

    private void HideWires()
    {
        foreach (var wire in _wires.GetComponentsInChildren<LightBulbController>())
        {
            wire.HideWire();
        }
    }
}
