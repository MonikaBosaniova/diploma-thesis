using System;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public Action DragEnd;
    public bool dragging;

    public bool fixX = false;
    public bool fixZ = false;
    
    public float minFixValue; 
    public float maxFixValue; 
    
    Camera cam;
    float dist;
    Vector3 offset;

    void Awake()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        dist = Vector3.Distance(cam.transform.position, transform.position);
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * dist);
        offset = transform.position - mouseWorld;
        dragging = true;
    }

    void OnMouseDrag()
    {
        if (!dragging) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * dist);
        Vector3  newPos = mouseWorld + offset;
        newPos.y = transform.position.y;
        newPos.x = fixX ? transform.position.x : Math.Clamp(newPos.x, minFixValue, maxFixValue);
        newPos.z = fixZ ? transform.position.z : Math.Clamp(newPos.z, minFixValue, maxFixValue);
        transform.position =  newPos;
    }

    void OnMouseUp()
    {
        if(dragging) DragEnd?.Invoke();
        dragging = false;
    }
}

