using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the cooler rotation speed and temperature simulation for the cooling minigame
/// </summary>
public class CoolerController : MonoBehaviour
{
    [Header("ROTATION")]
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private Slider speedSlider;
    [SerializeField] bool rotateX = false;
    [SerializeField] bool rotateY = false;
    [SerializeField] bool rotateZ = false;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float minRotationSpeed; 
    
    [Header("TEMPERATURE")]
    [SerializeField] private TMP_Text temperatureText;
    [SerializeField] private float startTemperature;
    [SerializeField] private float actualTemperature;
    [SerializeField] private float minTemperature;
    [SerializeField] private float maxTemperature;
    [SerializeField] private float midValuePoint;
    [SerializeField] private float highValuePoint;
    
    [SerializeField] private float temperatureChangeSpeed = 10f; // degrees per second

    [Header("---DEBUG---")] 
    public bool optimal;
    public bool noisy;
    public bool slow;

    private Tween rotationTween;
    private float finishTemperatureForScenario;
    private float offseteForScenario;
    private float tolerance =  10f;

    void Start()
    {
        rotationTween = rotatingPart.DOLocalRotate(new Vector3(rotateX ? 360 : 0, rotateY ? 360 : 0, rotateZ ? 360 : 0), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);

        UpdateRotationSpeed(speedSlider.value);
        actualTemperature = startTemperature;
        UpdateTemperatureText(startTemperature);

        speedSlider.onValueChanged.AddListener(UpdateRotationSpeed);
    }

    void Update()
    {
        optimal = false;
        //Debug.Log(gameObject.name + " : " + finishTemperatureForScenario + " " + actualTemperature);

        if (Mathf.Abs(ComputeFinishTempOfCooler() - actualTemperature) <= .1f)
        {
            // check win
            if (Mathf.Abs(actualTemperature - finishTemperatureForScenario) <= tolerance)
            {
                optimal = true;
                return;
            }
            
        }else if (ComputeFinishTempOfCooler() < actualTemperature)
        {
            actualTemperature -= temperatureChangeSpeed * Time.deltaTime;
        }
        else
        {
            actualTemperature += temperatureChangeSpeed * Time.deltaTime;
        }
        
        UpdateTemperatureText(actualTemperature);
    }

    /// <summary>
    /// Computes the final equilibrium temperature based on slider value and scenario offset
    /// </summary>
    /// <returns>Clamped temperature value between 20 and 100</returns>
    private float ComputeFinishTempOfCooler()
    {
        return Mathf.Min(100f, Mathf.Max(20f, 100f - speedSlider.value*100 + offseteForScenario));
    }

    /// <summary>
    /// Updates the rotation tween speed based on the slider value
    /// </summary>
    /// <param name="value">Slider value (0-1)</param>
    void UpdateRotationSpeed(float value)
    {
        rotationTween.timeScale = Mathf.Max(minRotationSpeed, value * maxRotationSpeed);
        
        //CheckState();
    }

    /// <summary>
    /// Updates the temperature text color based on current temperature zone
    /// </summary>
    private void VisualizeState()
    {
        //TODO FIND  better COLORS
        if (actualTemperature > highValuePoint)
        {
            temperatureText.color = Color.red;
            slow = true;
            noisy = false;
            return;
        }

        if (actualTemperature >= midValuePoint && actualTemperature <= highValuePoint)
        {
            temperatureText.color = Color.green;
            slow = false;
            noisy = false;
            return;
        }

        if (actualTemperature < midValuePoint)
        {
            temperatureText.color = Color.blue;
            slow = false;
            noisy = true;
        }
    }

    void OnDestroy()
    {
        rotationTween.Kill();
    }

    /// <summary>
    /// Sets the current temperature directly
    /// </summary>
    /// <param name="temperature">New temperature value</param>
    internal void SetTemperature(int temperature)
    {
        actualTemperature = temperature;
        UpdateTemperatureText(actualTemperature);
    }

    /// <summary>
    /// Sets the target temperature and offset for the current scenario
    /// </summary>
    /// <param name="finishTemperature">Target temperature for this scenario</param>
    /// <param name="offset">Temperature offset applied to calculation</param>
    internal void SetScenarioData(float finishTemperature, float offset)
    {
        finishTemperatureForScenario = finishTemperature;
        offseteForScenario = offset;
    }

    /// <summary>
    /// Updates the temperature UI text and clamps to valid range
    /// </summary>
    /// <param name="value">Temperature value to display</param>
    private void UpdateTemperatureText(float value)
    {
        var newValue = Mathf.RoundToInt(value);
        if (value >= minTemperature && value <= maxTemperature)
        {
            temperatureText.text = newValue.ToString();
            actualTemperature = value;
            VisualizeState();
        }
    }
}
