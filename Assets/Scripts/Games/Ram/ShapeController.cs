using System.Collections;
using System.Collections.Generic;
using Games.Ram;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShapeController : MonoBehaviour
{
    [Header("Debug Values - READ ONLY")] 
    [SerializeField] internal bool canBeDestroyed = false;
    [SerializeField] private float timeToLive = 0f;
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
        timeToLive = Random.Range(5, 20);
        StartCoroutine(SetDestroyStateAfterTime());
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
        if (bgTriggers.Count == transform.childCount)
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
        for(var i = 0; i < bgTriggers.Count; i++)
        {
            bgTriggers[i].SetSnapped(true);
            transform.GetChild(i).position = new Vector3(bgTriggers[i].transform.position.x, transform.GetChild(i).position.y, bgTriggers[i].transform.position.z);
        }
        ramLevelController.AddShapeToSnappedList(this);
    }

    public void SetLevelController(RamLevelController ramLc)
    {
        ramLevelController = ramLc;
    }
    
    private IEnumerator SetDestroyStateAfterTime() {
        yield return new WaitForSeconds(timeToLive);
        canBeDestroyed = true;
    }

}
