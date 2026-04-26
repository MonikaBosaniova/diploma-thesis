using System;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Controls the visual animation of a single star indicator using DOTween
/// </summary>
public class StarController : MonoBehaviour
{
    [SerializeField] private bool _collected;
    private Transform _collectedStar;

    private void Awake()
    {
        _collectedStar = transform.GetChild(0).GetComponent<RectTransform>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (_collected)
        {
            OnStarCollected();
            _collected = false;
        }
    }
    #endif

    /// <summary>
    /// Shows the star with existing collected state, no animation
    /// </summary>
    public void ShowAlreadyCollectedStar()
    {
        _collectedStar.transform.localScale = new Vector3(1, 1, 1);
    }
    
    /// <summary>
    /// Animates the star with rotation and scale tween when newly collected
    /// </summary>
    public void OnStarCollected()
    {
        Debug.Log("OnStarCollected TWEENING");
        _collectedStar.DORotate(new Vector3(0,0,360), .5f, RotateMode.FastBeyond360);
        _collectedStar.DOScaleX(1,.5f);
        _collectedStar.DOScaleY(1,.5f);
    }

    /// <summary>
    /// Instantly hides the star by resetting scale and rotation
    /// </summary>
    public void OnStarrRemoved()
    {
        Debug.Log("OnStarrRemoved TWEENING");
        _collectedStar.DOScaleX(0, 0f); 
        _collectedStar.DOScaleY(0, 0f);
        _collectedStar.rotation = Quaternion.Euler(0,0,0);
    }

}