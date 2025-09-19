using System;
using DG.Tweening;
using UnityEngine;

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

    public void ShowAlreadyCollectedStar()
    {
        _collectedStar.transform.localScale = new Vector3(1, 1, 1);
    }
    
    public void OnStarCollected()
    {
        _collectedStar.DORotate(new Vector3(0,0,360), .5f, RotateMode.FastBeyond360);
        _collectedStar.DOScaleX(1,.5f);
        _collectedStar.DOScaleY(1,.5f);
    }

}