using System;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public Action DragEnd;
    public bool dragging;

    [SerializeField] internal bool draggingEnabled = true;
    
    Camera cam;
    float dist;
    Vector3 offset;

    void Awake()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        if (!draggingEnabled || !enabled) return;
        
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

    private void OnDisable()
    {
        draggingEnabled = false;
    }
}

