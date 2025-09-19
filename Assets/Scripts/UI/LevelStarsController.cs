using DG.Tweening;
using UnityEngine;

public class LevelStarsController : MonoBehaviour
{
    private StarController[] _stars;
    
    public void Start()
    {
        _stars = gameObject.GetComponentsInChildren<StarController>();
        //ShowProgressStars();
    }

    public void ShowProgressStars(int starsCount, bool useTweening)
    {
        for (int i = 0; i < starsCount; i++)
        {
            if (useTweening)
            {
                var i1 = i;
                DOVirtual.DelayedCall(0.5f*i1, () => _stars[i1].OnStarCollected());
            }
            else
            {
                _stars[i].ShowAlreadyCollectedStar();
            }
                
        }
        
    }
    
}
