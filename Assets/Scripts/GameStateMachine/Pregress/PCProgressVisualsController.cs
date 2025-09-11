using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

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
    
    private PCComponentVisuals CaseVisual;
    private PCComponentVisuals MotherBoardVisuals;
    private PCComponentVisuals PowerUnitVisuals;
    private PCComponentVisuals CPUVisuals;
    private PCComponentVisuals[] RAMVisuals;
    private PCComponentVisuals[] HDDVisuals;
    private List<PCComponentVisuals> CoolingUnitVisuals;
    private List<PCComponentVisuals> VentilatorsOffVisuals;
    private List<PCComponentVisuals> VentilatorsOnVisuals;
    private List<PCComponentVisuals> FrontVentilatorsVisuals;
    private List<PCComponentVisuals> GPUVisuals;
    private List<PCComponentVisuals> CablesVisuals;

    // [Header("DEBUG SAVED PROGRESS")] 
    // [SerializeField] private bool isCase = false;
    // [SerializeField] private bool isMotherBoard = false;
    // [SerializeField] private bool isPowerUnit = false;
    // [SerializeField] private bool isCPU = false;
    // [SerializeField] private bool isRAM = false;
    // [SerializeField] private bool isHDD = false;
    // [SerializeField] private bool isCoolingUnit = false;
    // [SerializeField] private bool isVentilatorsOff = false;
    // [SerializeField] private bool isVentilatorsOn = false;
    // [SerializeField] private bool isGPU = false;
    // [SerializeField] private bool isCables = false;

    private void Awake()
    {
        CaseVisual = Case.GetComponent<PCComponentVisuals>();
        MotherBoardVisuals = MotherBoard.GetComponent<PCComponentVisuals>();
        PowerUnitVisuals = PowerUnit.GetComponent<PCComponentVisuals>();
        CPUVisuals = CPU.GetComponent<PCComponentVisuals>();
        RAMVisuals = RAM.GetComponentsInChildren<PCComponentVisuals>();
        HDDVisuals = HDD.GetComponentsInChildren<PCComponentVisuals>();
    }

    private void Start()
    {
        //ShowCase(false, false, false);
        ShowMotherBoard(false, false, false);
        ShowPowerUnit(false, false, false);
        ShowCPU(false, false, false);
        ShowRAM(false, false, false);
        ShowHDD(false, false, false);
        ShowCoolingUnit(false, false, false);
        ShowVentilatorsOff(false, false, false);
        ShowVentilatorsOn(false, false, false);
        ShowGPU(false, false, false);
        ShowCables(false, false, false);

        // if (isVentilatorsOn)
        // {
        //     foreach (var animation in FrontVentilators.gameObject.GetComponentsInChildren<Animation>())
        //     {
        //         animation.enabled = true;
        //         animation.Play();
        //     }
        // }
        // else
        // {
        //     foreach (var animation in FrontVentilators.gameObject.GetComponentsInChildren<Animation>())
        //     {
        //         animation.Stop();
        //         animation.enabled = false;
        //     }
        // }
    }

    public void ComponentVisibility(PCComponent component, bool visible, bool holographic, bool outlined)
    {
        switch (component)
        {
            case PCComponent.Case:
                ShowCase(visible, holographic, outlined);
                break;
            case PCComponent.MotherBoard:
                ShowMotherBoard(visible, holographic, outlined);
                break;
            case  PCComponent.PowerUnit:
                ShowPowerUnit(visible, holographic, outlined);
                break;
            case  PCComponent.CPU:
                ShowCPU(visible, holographic, outlined);
                break;
            // case  PCComponent.RAM:
            //     ShowRAM(visible, holographic, outlined);
            //     break;
            // case  PCComponent.HDD:
            //     ShowHDD(visible, holographic, outlined);
            //     break;
            // case  PCComponent.CoolingUnit:
            //     ShowCoolingUnit(visible, holographic, outlined);
            //     break;
            // case PCComponent.VentilatorON:
            //     ShowVentilatorsOn(visible, holographic, outlined);
            //     break;
            // case  PCComponent.VentilatorOFF:
            //     ShowVentilatorsOff(visible, holographic, outlined);
            //     break;
            // case PCComponent.FrontVentilators:
            //     break;
            // case  PCComponent.GPU:
            //     ShowGPU(visible, holographic, outlined);
            //     break;
            // case  PCComponent.Cables:
            //     ShowCables(visible, holographic, outlined);
            //     break;
            // default:
            //     break;
        }
    }


    public void SetGameObjectVisuals(PCComponentVisuals visuals, bool active, bool holographic, bool outlined)
    {
        //gameObject.SetActive(active);
        visuals.SetState(active,holographic, outlined);
        //gameObject.GetComponent<PCComponentVisuals>().SetComponentVisibility(active);
    }
    
    #region SHOW methods
    
    public void ShowCase(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(CaseVisual, active, holographic, outlined);
        //SetGameObjectActive(FrontVentilators, active);
    }
    
    public void ShowMotherBoard(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(MotherBoardVisuals, active, holographic, outlined);
    }
    
    public void ShowPowerUnit(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(PowerUnitVisuals, active, holographic, outlined);
    }
    
    public void ShowCPU(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(CPUVisuals, active, holographic, outlined);
    }

    public void ShowRAM(bool active, bool holographic, bool outlined)
    {
        //SetGameObjectActive(RAMVisuals, active);
    }
    
    public void ShowHDD(bool active, bool holographic, bool outlined)
    {
        //SetGameObjectActive(HDD, active);
    }
    
    public void ShowCoolingUnit(bool active, bool holographic, bool outlined)
    {
        //SetGameObjectActive(CoolingUnit, active);
    }
    
    public void ShowVentilatorsOff(bool active, bool holographic, bool outlined)
    {
        //SetGameObjectActive(VentilatorsOff, active);
    }
    
    public void ShowVentilatorsOn(bool active, bool holographic, bool outlined)
    {
        //SetGameObjectActive(VentilatorsOn, active);
    }
    
    public void ShowGPU(bool active, bool holographic, bool outlined)
    {
        //SetGameObjectActive(GPU, active);
    }
    
    private void ShowCables(bool active, bool holographic, bool outlined)
    {
        //SetGameObjectActive(Cables, active);
    }
    
    #endregion
}
