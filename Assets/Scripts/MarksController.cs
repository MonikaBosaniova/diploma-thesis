using System;
using UnityEngine;

public class MarksController : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameObject.SetActive(false);
    }
}
