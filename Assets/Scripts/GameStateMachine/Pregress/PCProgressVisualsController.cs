using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages visual state of all PC hardware components in the 3D PC model
/// Used to show/hide components based on player progress in the skill tree
/// </summary>
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
    
    private PCComponentVisuals _caseVisual;
    private PCComponentVisuals _motherBoardVisuals;
    private PCComponentVisuals _powerUnitVisuals;
    private PCComponentVisuals _cpuVisuals;
    private List<PCComponentVisuals> _ramVisuals;
    private List<PCComponentVisuals> _hddVisuals;
    private List<PCComponentVisuals> _coolingUnitVisuals;
    private List<PCComponentVisuals> _ventilatorsOffVisuals;
    private List<PCComponentVisuals> _ventilatorsOnVisuals;
    private List<Animation> _frontVentilatorsVisuals;
    private List<PCComponentVisuals> _gpuVisuals;
    private List<PCComponentVisuals> _cablesVisuals;
    
    private bool _ventilatorsAreOn = false;

    private void Awake()
    {
        _caseVisual = Case.GetComponent<PCComponentVisuals>();
        _motherBoardVisuals = MotherBoard.GetComponent<PCComponentVisuals>();
        _powerUnitVisuals = PowerUnit.GetComponent<PCComponentVisuals>();
        _cpuVisuals = CPU.GetComponent<PCComponentVisuals>();
        _ramVisuals = RAM.GetComponentsInChildren<PCComponentVisuals>().ToList();
        _hddVisuals = HDD.GetComponentsInChildren<PCComponentVisuals>().ToList();
        _coolingUnitVisuals = CoolingUnit.GetComponentsInChildren<PCComponentVisuals>().ToList();
        _ventilatorsOffVisuals = VentilatorsOff.GetComponentsInChildren<PCComponentVisuals>().ToList();
        _ventilatorsOnVisuals =  VentilatorsOn.GetComponentsInChildren<PCComponentVisuals>().ToList();
        _frontVentilatorsVisuals = FrontVentilators.GetComponentsInChildren<Animation>().ToList();
        _gpuVisuals = GPU.GetComponentsInChildren<PCComponentVisuals>().ToList();
        _cablesVisuals = Cables.GetComponentsInChildren<PCComponentVisuals>().ToList();
    }

    /// <summary>
    /// Sets visibility, holographic and outline state for a specific PC component
    /// </summary>
    /// <param name="component">The PC component to modify</param>
    /// <param name="visible">Whether the component is visible</param>
    /// <param name="holographic">Whether to use holographic material</param>
    /// <param name="outlined">Whether to show outline</param>
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

    /// <summary>
    /// Applies visual state to a list of PCComponentVisuals
    /// </summary>
    public void SetGameObjectVisuals(List<PCComponentVisuals> visuals, bool active, bool holographic, bool outlined)
    {
        foreach (PCComponentVisuals visual in visuals)
        {
            visual.SetState(active,holographic, outlined);
        }
    }
    
    /// <summary>
    /// Applies visual state to a single PCComponentVisuals
    /// </summary>
    public void SetGameObjectVisual(PCComponentVisuals visual, bool active, bool holographic, bool outlined)
    {
        visual.SetState(active,holographic, outlined);
    }
    
    #region SHOW methods
    
    public void ShowCase(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisual(_caseVisual, active, holographic, outlined);
    }
    
    public void ShowMotherBoard(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisual(_motherBoardVisuals, active, holographic, outlined);
    }
    
    public void ShowPowerUnit(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisual(_powerUnitVisuals, active, holographic, outlined);
    }
    
    public void ShowCPU(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisual(_cpuVisuals, active, holographic, outlined);
    }

    public void ShowRAM(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(_ramVisuals, active, holographic, outlined);
    }
    
    public void ShowHDD(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(_hddVisuals, active ,  holographic, outlined);
    }
    
    public void ShowCoolingUnit(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(_coolingUnitVisuals, active,  holographic, outlined);
        SetGameObjectVisuals(_ventilatorsAreOn ? _ventilatorsOnVisuals : _ventilatorsOffVisuals, 
            active, holographic, outlined);
    }
    
    public void ShowVentilatorsOff(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(_ventilatorsOffVisuals, active,  holographic, outlined);
        _ventilatorsAreOn = false;
        if (active)
        {
            foreach (var frontVent in _frontVentilatorsVisuals)
            {
                frontVent.Stop();
            }
        }
    }
    
    public void ShowVentilatorsOn(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(_ventilatorsOnVisuals, active,  holographic, outlined);
        _ventilatorsAreOn = true;
        if (active)
        {
            foreach (var frontVent in _frontVentilatorsVisuals)
            {
                frontVent.Play();
            }
        }
    }
    
    public void ShowGPU(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(_gpuVisuals, active,  holographic, outlined);
    }
    
    private void ShowCables(bool active, bool holographic, bool outlined)
    {
        SetGameObjectVisuals(_cablesVisuals, active,  holographic, outlined);
    }
    
    #endregion
}
