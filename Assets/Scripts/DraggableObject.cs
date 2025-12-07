using System;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public Action DragEnd;
    
    Camera cam;
    float dist;
    Vector3 offset;
    bool dragging;

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
        transform.position = mouseWorld + offset;
    }

    void OnMouseUp()
    {
        if(dragging) DragEnd?.Invoke();
        dragging = false;
    }
}

