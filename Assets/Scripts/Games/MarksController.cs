using UnityEngine;

/// <summary>
/// Controller that hides marks (checkmarks) on Start and OnEnable
/// </summary>
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
