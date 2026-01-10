using DG.Tweening;
using UnityEngine;

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

    public void CreateCube()
    {
       Instantiate(cubeToSpawn, Spawner.transform);
    }
    
    public void StopRotate()
    {
        rotatingTween.Kill();
    }

    private void StartRotating()
    {
        rotatingTween = transform.DORotate(
            new Vector3(0, 360 + transform.eulerAngles.y, 0),
            360f / speed,
            RotateMode.FastBeyond360
        ).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

    }

    private void RandomRotate()
    {
        var randomAngle = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, randomAngle, 0);
    }

    public void SetSpeedRotation(float newSpeed)
    {
        rotatingTween.Kill();
        speed = newSpeed;
        StartRotating();
    }
    
}
