using UnityEngine;

/// <summary>
/// Debug utility that logs the name of objects hit by mouse raycast each frame
/// </summary>
public class CameraRayCastDebbuger : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
    }
}
