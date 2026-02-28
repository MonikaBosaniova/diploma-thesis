using DG.Tweening;
using UnityEngine;

public class ThermotrotlingController : MonoBehaviour
{
    public GameObject BoltsIn;
    public GameObject BoltsOut;
    public GameObject Heat;

    public float duration = 1f;

    private float _startPosBoltsIn = -5f;
    private float _startPosBoltsOut = 0;
    private float _startPosHeat = -1f;
    
    private float _endPosBoltsIn = 0f;
    private float _endPosBoltsOut = 5f;
    private float _endPosHeat = 1f;
    
    void Start()
    {
        CreateSequence();
    }

    void CreateSequence()
    {
        // Create a new sequence
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(BoltsIn.transform.DOLocalMoveX(_endPosBoltsIn, duration + 1f))
            .AppendCallback(() => BoltsIn.SetActive(false));

        mySequence.Append(Heat.transform.DOLocalMoveZ(_endPosHeat, duration))
            .AppendCallback(() => Heat.SetActive(false));

        mySequence.Append(BoltsOut.transform.DOLocalMoveX(_endPosBoltsOut, duration + 1f))
            .AppendCallback(() => BoltsOut.SetActive(false));

        mySequence.OnComplete(() => {
            ResetObjects();
            CreateSequence();
        });
    }

    void ResetObjects()
    {
        BoltsIn.transform.DOLocalMoveX(_startPosBoltsIn, 0f);
        Heat.transform.DOLocalMoveZ(_startPosHeat, 0f);
        BoltsOut.transform.DOLocalMoveX(_startPosBoltsOut, 0f);
        
        BoltsIn.SetActive(true);
        Heat.SetActive(true);
        BoltsOut.SetActive(true);

        // Optional: Reset their positions if they aren't snapping back automatically
        // objA.transform.position = Vector3.zero; 
    }
}
