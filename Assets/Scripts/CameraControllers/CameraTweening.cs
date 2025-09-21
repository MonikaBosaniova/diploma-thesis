using System;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

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
        if (ProgressService.I.OpenSkillTree)
        {
            SpawnCameraToSkillTree();
        }
    }

    private void SpawnCameraToSkillTree()
    {
        camera.transform.position = skillTreePosition;
        camera.transform.eulerAngles = skillTreeRotation;
    }

    public void MoveCameraToSkillTree()
    {
        camera.transform.DOMove(skillTreePosition, 1.5f);
        camera.transform.DORotate(skillTreeRotation, 1.5f);
    }

    public void MoveCameraToDefaultPosition()
    {
        camera.transform.DOMove(defaultPosition, 1.5f);
        camera.transform.DORotate(defaultRotation, 1.5f);
    }
    
}
