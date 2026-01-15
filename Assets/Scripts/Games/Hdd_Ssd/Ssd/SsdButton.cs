using UnityEngine;

public class SsdButton : MonoBehaviour
{
    [SerializeField] private SsdButtonsController controller;
    [SerializeField] private GameObject onModel;
    [SerializeField] private GameObject offModel;
    [SerializeField] private GameObject line;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = transform.parent.GetComponent<SsdButtonsController>();
        onModel.SetActive(false);
        offModel.SetActive(true);
        line.SetActive(false);
    }

    public void TurnOn()
    {
        controller.SetAllSSdButtonsToValue(false);
        controller.SetIndexOfSelection(transform.GetSiblingIndex());
        SetModelValue(true);
    }

    public void SetModelValue(bool isOn)
    {
        onModel.SetActive(isOn);
        offModel.SetActive(!isOn);
        line.SetActive(isOn);
    }

    public void SetLineScaling(float scaleX, float posZ)
    {
        var scale = line.transform.localScale;
        scale.x = scaleX;
        line.transform.localScale = scale;
        
        var pos = line.transform.localPosition;
        pos.z = posZ;
        line.transform.localPosition = pos;
    }
}
