using System;
using System.Collections.Generic;
using System.Linq;
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
    private List<PCComponentVisuals> RAMVisuals;
    private List<PCComponentVisuals> HDDVisuals;
    private List<PCComponentVisuals> CoolingUnitVisuals;
    private List<PCComponentVisuals> VentilatorsOffVisuals;
    private List<PCComponentVisuals> VentilatorsOnVisuals;
    private List<Animation> FrontVentilatorsVisuals;
    private List<PCComponentVisuals> GPUVisuals;
    private List<PCComponentVisuals> CablesVisuals;

    private void Awake()
    {
        CaseVisual = Case.GetComponent<PCComponentVisuals>();
        MotherBoardVisuals = MotherBoard.GetComponent<PCComponentVisuals>();
        PowerUnitVisuals = PowerUnit.GetComponent<PCComponentVisuals>();
        CPUVisuals = CPU.GetComponent<PCComponentVisuals>();
        RAMVisuals = RAM.GetComponentsInChildren<PCComponentVisuals>().ToList();
        HDDVisuals = HDD.GetComponentsInChildren<PCComponentVisuals>().ToList();
        CoolingUnitVisuals = CoolingUnit.GetComponentsInChildren<PCComponentVisuals>().ToList();
        VentilatorsOffVisuals = VentilatorsOff.GetComponentsInChildren<PCComponentVisuals>().ToList();
        VentilatorsOnVisuals =  VentilatorsOn.GetComponentsInChildren<PCComponentVisuals>().ToList();
        FrontVentilatorsVisuals = FrontVentilators.GetComponentsInChildren<Animation>().ToList();
        GPUVisuals = GPU.GetComponentsInChildren<PCComponentVisuals>().ToList();
        CablesVisuals = Cables.GetComponentsInChildren<PCComponentVisuals>().ToList();
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
            case  PCComponent.RAM:
                ShowRAM(visible, holographic, outlined);
                break;
            case  PCComponent.HDD:
                ShowHDD(visible, holographic, outlined);
                break;
            case  PCComponent.CoolingUnit:
                ShowCoolingUnit(visible, holographic, outlined);
                break;
            case PCComponent.VentilatorON:
                ShowVentilatorsOn(visible, holographic, outlined);
                break;
            case  PCComponent.VentilatorOFF:
                ShowVentilatorsOff(visible, holographic, outlined);
                break;
            case PCComponent.FrontVentilators:
                break;
            case  PCComponent.GPU:
                ShowGPU(visible, holographic, outlined);
                break;
            case  PCComponent.Cables:
                ShowCables(visible, holographic, outlined);
                break;
        }
    }


    public void SetGameObjectVisuals(List<PCComponentVisuals> visuals, bool active, bool holographic, bool outlined)
    {
        foreach (PCComponentVisuals visual in visuals)
        {
            visual.SetState(active,holographic, outlined);
        }
    }
    
    public void SetGameObjectVisual(PCComponentVisuals visual, bool active, bool holographic, bool outlined)
    {
        visual.SetState(active,holographic, outlined);
    }
    
    #region SHOW methods
    
    public void ShowCase(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisual(CaseVisual, active, holographic, outlined);
    }
    
    public void ShowMotherBoard(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisual(MotherBoardVisuals, active, holographic, outlined);
    }
    
    public void ShowPowerUnit(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisual(PowerUnitVisuals, active, holographic, outlined);
    }
    
    public void ShowCPU(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisual(CPUVisuals, active, holographic, outlined);
    }

    public void ShowRAM(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(RAMVisuals, active, holographic, outlined);
    }
    
    public void ShowHDD(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(HDDVisuals, active ,  holographic, outlined);
    }
    
    public void ShowCoolingUnit(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(CoolingUnitVisuals, active,  holographic, outlined);
    }
    
    public void ShowVentilatorsOff(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(VentilatorsOffVisuals, active,  holographic, outlined);
        if (active)
        {
            foreach (var frontVent in FrontVentilatorsVisuals)
            {
                frontVent.Stop();
            }
        }
    }
    
    public void ShowVentilatorsOn(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(VentilatorsOnVisuals, active,  holographic, outlined);
        if (active)
        {
            foreach (var frontVent in FrontVentilatorsVisuals)
            {
                frontVent.Play();
            }
        }
    }
    
    public void ShowGPU(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(GPUVisuals, active,  holographic, outlined);
    }
    
    private void ShowCables(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(CablesVisuals, active,  holographic, outlined);
    }
    
    #endregion
}
