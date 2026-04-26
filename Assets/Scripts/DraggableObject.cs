using System;
using UnityEngine;

/// <summary>
/// Allows a GameObject to be dragged by mouse input in world space
/// </summary>
public class DraggableObject : MonoBehaviour
{
    public Action DragEnd;
    public Action DragStart;
    public bool dragging;

    [SerializeField] internal bool draggingEnabled = true;
    
    Camera cam;
    float dist;
    Vector3 offset;

    void Awake()
    {
        cam = Camera.main;
    }

    /// <summary>
    /// Handles mouse button press, calculates the offset between mouse and object position
    /// </summary>
    void OnMouseDown()
    {
        if (!draggingEnabled || !enabled) return;
        
        dist = Vector3.Distance(cam.transform.position, transform.position);
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * dist);
        offset = transform.position - mouseWorld;
        dragging = true;
        DragStart?.Invoke();
    }

    /// <summary>
    /// Handles mouse drag, updates object position based on mouse world position and the initial offset
    /// </summary>
    void OnMouseDrag()
    {
        if (!dragging) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * dist);
        transform.position = mouseWorld + offset;
    }

    /// <summary>
    /// Handles mouse button release, invokes DragEnd callback and stops dragging
    /// </summary>
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

