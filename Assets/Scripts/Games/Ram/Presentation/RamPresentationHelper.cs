using Gates;
using UnityEngine;

public class RamPresentationHelper : MonoBehaviour
{
    public SwitchController switchController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(switchController != null)
            switchController.OnValueChanged += RamElectricInputChanged;
    }

    public void CallGC()
    {
        if (transform.childCount == 0) return;
        
        var random = Random.Range(0, transform.childCount);
        transform.GetChild(random).gameObject.SetActive(false);
    }
    
    private void RamElectricInputChanged(bool obj)
    {
        if (obj)
            GenerateNewObjects();
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void GenerateNewObjects()
    {
        var possibilityOfTurnedOn = 0.5f;
        
        foreach (Transform child in transform)
        {
            var random = Random.Range(0f, 1f);
            child.gameObject.SetActive(random > possibilityOfTurnedOn);
        }
        
    }
}
