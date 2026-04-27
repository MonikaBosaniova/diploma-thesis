using DG.Tweening;
using UnityEngine;

/// <summary>
/// Controls the display of star indicators for a skill tree level button
/// </summary>
public class LevelStarsController : MonoBehaviour
{
    private StarController[] _stars;
    
    public void Start()
    {
        _stars = gameObject.GetComponentsInChildren<StarController>();
    }

    /// <summary>
    /// Updates the visible star count by activating/deactivating star GameObjects
    /// </summary>
    /// <param name="starsCount">Number of stars to display (0-3)</param>
    /// <param name="newStars">Number of new stars to animate</param>
    /// <param name="useTweening">Whether to animate the star appearance</param>
    public void ShowProgressStars(int starsCount, int newStars, bool useTweening)
    {
        _stars ??= gameObject.GetComponentsInChildren<StarController>();
        for (int i = 0; i < starsCount; i++)
        {
            if (useTweening && i >= (starsCount - newStars))
            {
                var i1 = i;
                DOVirtual.DelayedCall(0.5f*i1, () => _stars[i1].OnStarCollected());
            }
            else
            {
                _stars[i].ShowAlreadyCollectedStar();
            }
        }

        for (int i = starsCount; i < 3; i++)
        {
            _stars[i].OnStarrRemoved();
        }
    }
}
