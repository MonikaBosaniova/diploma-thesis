using System.Collections;
using System.Collections.Generic;
using Games.Ram;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShapeController : MonoBehaviour
{
    [Header("Debug Values - READ ONLY")] 
    [SerializeField] private Material snappedCubieMaterial;
    List<Vector3> ChildSpawnPointPositions = new List<Vector3>();
    Vector3 SpawnPoint;
    DraggableObject draggableObject;
    List<BgCubeTrigger> bgTriggers = new List<BgCubeTrigger>();
    RamLevelController ramLevelController;

    private void Start()
    {
        draggableObject = GetComponent<DraggableObject>();
        
        foreach (Transform child in transform)
        {
            ChildSpawnPointPositions.Add(child.localPosition);
        }
        SpawnPoint = transform.position;
        draggableObject.DragEnd += SnapObject;
    }

    public void AddBgTrigger(BgCubeTrigger bgTrigger)
    {
        bgTriggers.Add(bgTrigger);
    }
    
    public void RemoveBgTrigger(BgCubeTrigger bgTrigger)
    {
        if(bgTriggers.Contains(bgTrigger))
            bgTriggers.Remove(bgTrigger);
    }
    
    private void SnapObject()
    {
        if (bgTriggers.Count == transform.childCount && bgTriggers.Count != 0)
        {
            //Checking that the trigger is not CPU point
            if (bgTriggers[0].GetComponent<CPUWantedAddressController>() == null)
            {
                SnapObjectToPosition();
                draggableObject.enabled = false;
                
                if(ramLevelController != null)
                    ramLevelController.GenerateShape();
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
        
        foreach (BgCubeTrigger bgTrigger in bgTriggers)
            bgTrigger.ClearColoring();
        bgTriggers.Clear();
    }

    private void SnapToSpawnPoint()
    {
        transform.position = SpawnPoint;
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = ChildSpawnPointPositions[i];
        }
    }

    public void SnapObjectToPosition()
    {
        bgTriggers[0].transform.parent.gameObject.TryGetComponent<RamGridGenerator>(out var cubeGridEditor);
        if (cubeGridEditor == null) return;
        
        float spacing = cubeGridEditor.spacing;
        for(var i = 0; i < bgTriggers.Count; i++)
        {
            var trigger = bgTriggers[i];
            var snapChild = transform.GetChild(i);
            
            trigger.SetSnapped(true);
            snapChild.position = new Vector3(trigger.transform.position.x, snapChild.position.y, trigger.transform.position.z);
            snapChild.GetChild(0).gameObject.GetComponent<Renderer>().material = snappedCubieMaterial;
            
            float snappingRow = (trigger.transform.localPosition.x / spacing);
            float snappingColumn = (trigger.transform.localPosition.z / spacing);
            var snappedCubie = snapChild.gameObject.GetComponent<SnappedAddressCubieController>();
            snappedCubie.snapped = true;
            snappedCubie.SetPosition(snappingRow, snappingColumn);
            ramLevelController.AddSnappedCubieToList(snappedCubie);
        }
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<DraggableObject>());
        //GetComponent<DraggableObject>().enabled = false;
    }

    public void SetLevelController(RamLevelController ramLc)
    {
        ramLevelController = ramLc;
    }
}
