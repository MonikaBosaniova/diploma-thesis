using System.Linq;
using UnityEngine;

/// <summary>
/// Controls the visual state of a PC component (visibility, holographic mode, outline)
/// </summary>
[DisallowMultipleComponent]
public class PCComponentVisuals : MonoBehaviour
{
    [Header("Initial State")]
    [SerializeField] private bool _isVisible = false;
    [SerializeField] private bool _isHologram = false;
    [SerializeField] private bool _isOutlined = false;

    [Header("Materials")]
    [Tooltip("Material used in normal mode (slot 0). If empty, the current slot 0 at startup is used.")]
    [SerializeField] private Material _defaultMaterial;

    [Tooltip("Material used in hologram mode (slot 0). Assign a Hologram/Unlit/etc.")]
    [SerializeField] private Material _hologramMaterial;

    [Header("Advanced")]
    [Tooltip("Use sharedMaterials to avoid runtime instancing. (Most games can leave this OFF.)")]
    [SerializeField] private bool _useSharedMaterials = false;

    MeshRenderer _renderer;
    Outline _outline;
    Material _capturedDefault;

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        if (_renderer == null)
        {
            Debug.LogError($"{nameof(PCComponentVisuals)}: No Renderer found on {name}.");
            enabled = false;
            return;
        }

        var mats = _useSharedMaterials ? _renderer.sharedMaterials : _renderer.materials;
        if (mats == null || mats.Length == 0 || mats[0] == null)
        {
            Debug.LogError($"{nameof(PCComponentVisuals)}: Renderer on {name} has no materials.");
            enabled = false;
            return;
        }

        _capturedDefault = mats[0];
        if (_defaultMaterial == null) _defaultMaterial = _capturedDefault;
        
        //OUTLINE SETUP
        _outline = gameObject.AddComponent<Outline>();
        _outline.enabled = false;
        _outline.OutlineMode = Outline.Mode.OutlineAll;
        _outline.OutlineWidth = 7f;
        _outline.OutlineColor = new Color(1,(float)0.5592139,0,1);
    }

    /// <summary>
    /// Sets visibility of all MeshRenderers in this component and its children
    /// </summary>
    /// <param name="isVisible">Whether the component should be visible</param>
    public void SetComponentVisibility(bool isVisible)
    {
        var allRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var r in allRenderers)
        {
            r.enabled = isVisible;
        }
        _renderer.enabled = isVisible;
    }

    /// <summary>
    /// Switches all renderers between default and holographic material, toggles shadow casting
    /// </summary>
    /// <param name="isHologram">Whether to use holographic material</param>
    public void SetComponentHolographic(bool isHologram)
    {
        if (_renderer == null) return;

        var allRenderers = GetComponentsInChildren<MeshRenderer>().ToList();
        allRenderers.Add(_renderer);
        
        foreach (var r in allRenderers)
        {
            var mats = _useSharedMaterials ? r.sharedMaterials : r.materials;

            if (mats.Length == 0)
            {
                mats = new Material[1];
            }

            Material target = isHologram ? _hologramMaterial : _defaultMaterial;
            if (target == null)
            {
                Debug.LogWarning($"{nameof(PCComponentVisuals)} on {name}: Target material missing. Falling back to captured default.");
                target = _capturedDefault;
            }
        
            mats[0] = target;

            if (_useSharedMaterials)
                r.sharedMaterials = mats;
            else
                r.materials = mats;

            r.shadowCastingMode = isHologram
                ? UnityEngine.Rendering.ShadowCastingMode.Off
                : UnityEngine.Rendering.ShadowCastingMode.On;
            r.receiveShadows = !isHologram;
        }

    }

    /// <summary>
    /// Enables or disables the outline effect on this component
    /// </summary>
    /// <param name="isOutlined">Whether the outline should be active</param>
    public void SetComponentOutline(bool isOutlined)
    {
        if (_outline == null)
        {
            return;
        }
        
        _outline.enabled = isOutlined;
    }

    /// <summary>
    /// Sets all visual states at once (visibility, hologram, outline)
    /// </summary>
    /// <param name="visible">Whether the component is visible</param>
    /// <param name="hologram">Whether to use holographic material</param>
    /// <param name="outlined">Whether to show outline</param>
    public void SetState(bool visible, bool hologram, bool outlined)
    {
        SetComponentVisibility(visible);
        SetComponentHolographic(hologram);
        SetComponentOutline(outlined);
    }
}
