using System;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public Action DragEnd;
    public bool dragging;

    [Header("Rotation limits (0–360 degrees)")]
    public float minZ;
    public float maxZ;

    [Header("Drag")]
    public float degreesPerPixel = 0.3f;
    public bool invert;

    float lastMouseX;
    float z;                        // ALWAYS 0–360
    Quaternion baseRotation;

    void Awake()
    {
        z = Normalize360(transform.localEulerAngles.z);
        z = Mathf.Clamp(z, minZ, maxZ);

        CaptureBase();
        Apply();
    }

    void OnMouseDown()
    {
        lastMouseX = Input.mousePosition.x;
        dragging = true;

        CaptureBase();
    }

    void OnMouseDrag()
    {
        if (!dragging) return;

        float mouseX = Input.mousePosition.x;
        float delta = mouseX - lastMouseX;
        lastMouseX = mouseX;

        if (invert) delta = -delta;

        z = Mathf.Clamp(z + delta * degreesPerPixel, minZ, maxZ);
        Apply();
    }

    void OnMouseUp()
    {
        if (dragging) DragEnd?.Invoke();
        dragging = false;
    }

    void CaptureBase()
    {
        // factor out current Z safely
        Quaternion current = transform.localRotation;
        Quaternion zRot = Quaternion.AngleAxis(z, Vector3.forward);
        baseRotation = current * Quaternion.Inverse(zRot);
    }

    void Apply()
    {
        transform.localRotation = baseRotation * Quaternion.AngleAxis(z, Vector3.forward);
    }

    static float Normalize360(float a)
    {
        a %= 360f;
        if (a < 0f) a += 360f;
        return a;
    }
}
