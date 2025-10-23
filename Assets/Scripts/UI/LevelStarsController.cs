using DG.Tweening;
using UnityEngine;

public class LevelStarsController : MonoBehaviour
{
    private StarController[] _stars;
    
    public void Start()
    {
        _stars = gameObject.GetComponentsInChildren<StarController>();
    }

    public void ShowProgressStars(int starsCount, int newStars, bool useTweening)
    {
        _stars ??= gameObject.GetComponentsInChildren<StarController>();
        Debug.Log("ShowProgressStars: " + newStars + "...." + useTweening);
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
        
        //ProgressService.I.NewStars = 0;
    }
    
}
