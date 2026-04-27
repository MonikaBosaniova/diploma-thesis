using UnityEngine;
using DG.Tweening;

/// <summary>
/// Controls camera position and rotation tweening between default view and skill tree view
/// </summary>
public class CameraTweening : MonoBehaviour
{
    [Header("Camera Positions")]
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Vector3 defaultRotation;
    [SerializeField] private Vector3 skillTreePosition;
    [SerializeField] private Vector3 skillTreeRotation;
    
    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        defaultPosition = camera.transform.position;
        defaultRotation = camera.transform.eulerAngles;
    }

    private void Start()
    {
        if(ProgressService.I != null)
            if (ProgressService.I.OpenSkillTree)
            {
                SpawnCameraToSkillTree();
            }
    }

    /// <summary>
    /// Immediately sets camera to skill tree position without animation
    /// </summary>
    private void SpawnCameraToSkillTree()
    {
        camera.transform.position = skillTreePosition;
        camera.transform.eulerAngles = skillTreeRotation;
    }

    /// <summary>
    /// Animates camera movement and rotation to the skill tree view position
    /// </summary>
    public void MoveCameraToSkillTree()
    {
        camera.transform.DOMove(skillTreePosition, 1.5f);
        camera.transform.DORotate(skillTreeRotation, 1.5f);
    }

    /// <summary>
    /// Animates camera movement and rotation back to the default (start) position
    /// </summary>
    public void MoveCameraToDefaultPosition()
    {
        camera.transform.DOMove(defaultPosition, 1.5f);
        camera.transform.DORotate(defaultRotation, 1.5f);
    }
    
}
