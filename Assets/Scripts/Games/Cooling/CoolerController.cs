using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolerController : MonoBehaviour
{
    [Header("ROTATION")]
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private Slider speedSlider;
    [SerializeField] bool rotateX = false;
    [SerializeField] bool rotateY = false;
    [SerializeField] bool rotateZ = false;
    [SerializeField] private float maxRotationSpeed = 3f;
    [SerializeField] private float minRotationSpeed = 0.5f;
    
    [Header("TEMPERATURE")]
    [SerializeField] private TMP_Text temperatureText;
    [SerializeField] private int startTemperature;
    [SerializeField] private int actualTemperature;
    [SerializeField] private int minTemperature;
    [SerializeField] private int maxTemperature;
    [SerializeField] private int midValuePoint;
    [SerializeField] private int highValuePoint;

    private Tween rotationTween;

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

    void UpdateRotationSpeed(float value)
    {
        rotationTween.timeScale = Mathf.Max(minRotationSpeed, value * maxRotationSpeed);
    }

    void OnDestroy()
    {
        rotationTween.Kill();
    }

    private void UpdateTemperatureText(int value)
    {
        if (value >= minTemperature && value <= maxTemperature)
        {
            temperatureText.text = value.ToString();
            actualTemperature = value;
        }
        
        //TODO FIND COLORS
        if(value > highValuePoint)
            temperatureText.color = Color.red;
        else if (value >= midValuePoint && value <= highValuePoint)
            temperatureText.color = Color.green;
        else if(value < midValuePoint)
            temperatureText.color = Color.blue;
    }
}
