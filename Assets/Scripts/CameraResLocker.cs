using UnityEngine;

/// <summary>
/// Setting camera orthographic size to match the target resolution
/// </summary>
[ExecuteInEditMode] // Changes in Editor
[RequireComponent(typeof(Camera))]
public class CameraResLocker : MonoBehaviour
{
    [Header("Resolution goal (ex. Full HD)")]
    public float targetWidth = 19.2f;  // Wanted width in Unity units
    public float targetHeight = 10.8f; // Wanted height in Unity units

    private Camera _camera;

    void Awake() => _camera = GetComponent<Camera>();

    void LateUpdate()
    {
        Adjust();
    }

    void Adjust()
    {
        float targetAspect = targetWidth / targetHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        // Case A: Screen has bigger height (MacBook, Portrét)
        if (currentAspect < targetAspect)
        {
            _camera.orthographicSize = (targetWidth / currentAspect) * 0.5f;
        }
        // Case B: Screen has bigger width (Ultra-wide monitor)
        else
        {
            _camera.orthographicSize = targetHeight * 0.5f;
        }
    }
}