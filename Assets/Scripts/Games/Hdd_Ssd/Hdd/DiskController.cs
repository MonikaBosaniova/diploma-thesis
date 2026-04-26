using DG.Tweening;
using UnityEngine;

/// <summary>
/// Implements the HDD disk rotation and data spawning
/// </summary>
public class DiskController : MonoBehaviour
{
    public GameObject cubeToSpawn;
    public GameObject Spawner;
    public float speed = 20f;

    private Tween rotatingTween;
    
    private void Start()
    {
        RandomRotate();
        StartRotating();
    }
    
    /// <summary>
    /// Instantiate data cube
    /// </summary>
    public void CreateCube()
    {
       Instantiate(cubeToSpawn, Spawner.transform);
    }
    
    /// <summary>
    /// Stops the disc rotation tween
    /// </summary>
    public void StopRotate()
    {
        rotatingTween.Kill();
    }

    /// <summary>
    /// Starts continuous Y-axis rotation of the disc
    /// </summary>
    private void StartRotating()
    {
        rotatingTween = transform.DORotate(
            new Vector3(0, 360 + transform.eulerAngles.y, 0),
            360f / speed,
            RotateMode.FastBeyond360
        ).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

    }

    /// <summary>
    /// Rotates the disc to a random initial angle
    /// </summary>
    private void RandomRotate()
    {
        var randomAngle = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, randomAngle, 0);
    }

    /// <summary>
    /// Changes the disc rotation speed by restarting the tween
    /// </summary>
    /// <param name="newSpeed">New rotation speed in degrees per second</param>
    public void SetSpeedRotation(float newSpeed)
    {
        rotatingTween.Kill();
        speed = newSpeed;
        StartRotating();
    }
}
