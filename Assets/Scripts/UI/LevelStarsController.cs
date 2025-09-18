using UnityEngine;

public class LevelStarsController : MonoBehaviour
{
    [SerializeField] private int _starsCount;
    private StarController[] _stars;
    
    public void Start()
    {
        _stars = gameObject.GetComponentsInChildren<StarController>();
        ShowProgressStars();
    }

    public void ShowProgressStars()
    {
        foreach (var star in _stars)
        {
            star.ShowAlreadyCollectedStar();
        }
    }
    
}
