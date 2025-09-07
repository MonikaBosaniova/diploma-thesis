using UnityEngine;

public class PCProgressVisualsController : MonoBehaviour
{
    [SerializeField] private GameObject Case;
    [SerializeField] private GameObject MotherBoard;
    [SerializeField] private GameObject PowerUnit;
    [SerializeField] private GameObject CPU;
    [SerializeField] private GameObject RAM;
    [SerializeField] private GameObject HDD;
    [SerializeField] private GameObject CoolingUnit;
    [SerializeField] private GameObject VentilatorsOff;
    [SerializeField] private GameObject VentilatorsOn;
    [SerializeField] private GameObject FrontVentilators;
    [SerializeField] private GameObject GPU;
    [SerializeField] private GameObject Cables;

    [Header("DEBUG SAVED PROGRESS")] 
    [SerializeField] private bool isCase = false;
    [SerializeField] private bool isMotherBoard = false;
    [SerializeField] private bool isPowerUnit = false;
    [SerializeField] private bool isCPU = false;
    [SerializeField] private bool isRAM = false;
    [SerializeField] private bool isHDD = false;
    [SerializeField] private bool isCoolingUnit = false;
    [SerializeField] private bool isVentilatorsOff = false;
    [SerializeField] private bool isVentilatorsOn = false;
    [SerializeField] private bool isGPU = false;
    [SerializeField] private bool isCables = false;

    private void Start()
    {
        ShowCase(isCase);
        ShowMotherBoard(isMotherBoard);
        ShowPowerUnit(isPowerUnit);
        ShowCPU(isCPU);
        ShowRAM(isRAM);
        ShowHDD(isHDD);
        ShowCoolingUnit(isCoolingUnit);
        ShowVentilatorsOff(isVentilatorsOff);
        ShowVentilatorsOn(isVentilatorsOn);
        ShowGPU(isGPU);
        ShowCables(isCables);

        if (isVentilatorsOn)
        {
            foreach (var animation in FrontVentilators.gameObject.GetComponentsInChildren<Animation>())
            {
                animation.enabled = true;
                animation.Play();
            }
        }
        else
        {
            foreach (var animation in FrontVentilators.gameObject.GetComponentsInChildren<Animation>())
            {
                animation.Stop();
                animation.enabled = false;
            }
        }
    }


    public void SetGameObjectActive(GameObject gameObject, bool active = true)
    {
        gameObject.SetActive(active);
    }
    
    #region SHOW methods
    
    public void ShowCase(bool active)
    {
        SetGameObjectActive(Case, active);
        SetGameObjectActive(FrontVentilators, active);
    }
    
    public void ShowMotherBoard(bool active)
    {
        SetGameObjectActive(MotherBoard, active);
    }
    
    public void ShowPowerUnit(bool active)
    {
        SetGameObjectActive(PowerUnit, active);
    }
    
    public void ShowCPU(bool active)
    {
        SetGameObjectActive(CPU, active);
    }

    public void ShowRAM(bool active)
    {
        SetGameObjectActive(RAM, active);
    }
    
    public void ShowHDD(bool active)
    {
        SetGameObjectActive(HDD, active);
    }
    
    public void ShowCoolingUnit(bool active)
    {
        SetGameObjectActive(CoolingUnit, active);
    }
    
    public void ShowVentilatorsOff(bool active)
    {
        SetGameObjectActive(VentilatorsOff, active);
    }
    
    public void ShowVentilatorsOn(bool active)
    {
        SetGameObjectActive(VentilatorsOn, active);
    }
    
    public void ShowGPU(bool active)
    {
        SetGameObjectActive(GPU, active);
    }
    
    private void ShowCables(bool active)
    {
        SetGameObjectActive(Cables, active);
    }
    
    #endregion
}
