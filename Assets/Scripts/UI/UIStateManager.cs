using UnityEngine;
using DG.Tweening;

public class UIStateManager : MonoBehaviour
{
    [SerializeField] private UIStates CurrentUIState;
    [SerializeField] private RectTransform skillTree;

    private GameObject menuStateParent;
    private GameObject skillTreeParent;
    
    private float skillTreeHiddenPositionX;
    private void Start()
    {
        menuStateParent = transform.GetChild(0).gameObject;
        skillTreeParent = transform.GetChild(1).gameObject;
        skillTreeHiddenPositionX = skillTree.anchoredPosition.x;
        UpdateState(UIStates.Menu);
    }

    public void UpdateState(UIStates state)
    {
        CurrentUIState = state;
        switch (state)
        {
            case UIStates.None:
                menuStateParent.SetActive(false);
                skillTreeParent.SetActive(false);
                break;
            case UIStates.Menu:
                menuStateParent.SetActive(true);
                skillTreeParent.SetActive(false);
                break;
            case UIStates.SkillTree:
                menuStateParent.SetActive(false);
                skillTreeParent.SetActive(true);
                break;
        }
    }

    public void ChangeToMenu()
    {
        skillTree.DOAnchorPosX(skillTreeHiddenPositionX, .33f).OnComplete(() =>UpdateState(UIStates.Menu));
    }
    
    public void ChangeToSkillTree()
    {
        UpdateState(UIStates.SkillTree);
        skillTree.DOAnchorPosX(0, .33f);
    }

}
