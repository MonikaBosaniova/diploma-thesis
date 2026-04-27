using System.Collections.Generic;
using Games.Ram;
using UnityEngine;

/// <summary>
/// Controls a draggable shape in the RAM minigame, handles snapping to grid cells
/// </summary>
public class ShapeController : MonoBehaviour
{
    [Header("Debug Values - READ ONLY")] 
    [SerializeField] private Material snappedCubieMaterial;

    private readonly List<Vector3> _childSpawnPointPositions = new List<Vector3>();
    private Vector3 _spawnPoint;
    private DraggableObject _draggableObject;
    private readonly List<BgCubeTrigger> _bgTriggers = new List<BgCubeTrigger>();
    private RamLevelController _ramLevelController;

    private void Start()
    {
        _draggableObject = GetComponent<DraggableObject>();
        
        foreach (Transform child in transform)
        {
            _childSpawnPointPositions.Add(child.localPosition);
        }
        _spawnPoint = transform.position;
        _draggableObject.DragEnd += SnapObject;
    }

    /// <summary>
    /// Adds a background trigger to the overlap tracking list
    /// </summary>
    /// <param name="bgTrigger">Trigger zone that the shape overlaps</param>
    public void AddBgTrigger(BgCubeTrigger bgTrigger)
    {
        _bgTriggers.Add(bgTrigger);
    }
    
    /// <summary>
    /// Removes a background trigger from the overlap tracking list
    /// </summary>
    /// <param name="bgTrigger">Trigger zone to remove</param>
    public void RemoveBgTrigger(BgCubeTrigger bgTrigger)
    {
        if(_bgTriggers.Contains(bgTrigger))
            _bgTriggers.Remove(bgTrigger);
    }
    
    private void SnapObject()
    {
        if (_bgTriggers.Count == transform.childCount && _bgTriggers.Count != 0)
        {
            //Checking that the trigger is not CPU point
            if (_bgTriggers[0].GetComponent<CPUWantedAddressController>() == null)
            {
                SnapObjectToPosition();
                _draggableObject.enabled = false;
                
                if(_ramLevelController != null)
                    _ramLevelController.GenerateShape();
            }
            else
            {
                SnapToSpawnPoint();
            }
        }
        else
        {
            SnapToSpawnPoint();
        }
        
        foreach (BgCubeTrigger bgTrigger in _bgTriggers)
            bgTrigger.ClearColoring();
        _bgTriggers.Clear();
    }

    private void SnapToSpawnPoint()
    {
        transform.position = _spawnPoint;
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = _childSpawnPointPositions[i];
        }
    }

    /// <summary>
    /// Snaps all shape children to their corresponding grid trigger positions
    /// </summary>
    public void SnapObjectToPosition()
    {
        _bgTriggers[0].transform.parent.gameObject.TryGetComponent<RamGridGenerator>(out var cubeGridEditor);
        if (cubeGridEditor == null) return;
        
        float spacing = cubeGridEditor.spacing;
        for(var i = 0; i < _bgTriggers.Count; i++)
        {
            var trigger = _bgTriggers[i];
            var snapChild = transform.GetChild(i);
            
            trigger.SetSnapped(true);
            snapChild.position = new Vector3(trigger.transform.position.x, snapChild.position.y, trigger.transform.position.z);
            snapChild.GetChild(0).gameObject.GetComponent<Renderer>().material = snappedCubieMaterial;
            
            float snappingRow = (trigger.transform.localPosition.x / spacing);
            float snappingColumn = (trigger.transform.localPosition.z / spacing);
            var snappedCubie = snapChild.gameObject.GetComponent<SnappedAddressCubieController>();
            snappedCubie.snapped = true;
            snappedCubie.SetPositionAndTrigger(snappingRow, snappingColumn, trigger);
            _ramLevelController.AddSnappedCubieToList(snappedCubie);
        }
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<DraggableObject>());
    }

    /// <summary>
    /// Sets the reference to the parent level controller
    /// </summary>
    /// <param name="ramLc">Ram level controller instance</param>
    public void SetLevelController(RamLevelController ramLc)
    {
        _ramLevelController = ramLc;
    }
}
