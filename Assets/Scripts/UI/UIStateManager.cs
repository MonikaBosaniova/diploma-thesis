using UnityEngine;
using DG.Tweening;

public class UIStateManager : MonoBehaviour
{
    [SerializeField] private UIStates CurrentUIState;
    [SerializeField] private RectTransform skillTree;
    [SerializeField] private Transform frontPCPanel;

    private GameObject menuStateParent;
    private GameObject skillTreeParent;
    
    private float skillTreeHiddenPositionX;
    private float frontPanelStartPositionZ;
    private void Start()
    {
        menuStateParent = transform.GetChild(0).gameObject;
        skillTreeParent = transform.GetChild(1).gameObject;
        skillTreeHiddenPositionX = skillTree.anchoredPosition.x;
        frontPanelStartPositionZ = frontPCPanel.localPosition.z;
        if (ProgressService.I.OpenSkillTree)
        {
            ChangeToSkillTree(false);
        }
        else
        {
            ChangeToMenu();
        }
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
        frontPCPanel.DOLocalMoveZ(frontPanelStartPositionZ, .8f);
        skillTree.DOAnchorPosX(skillTreeHiddenPositionX, .33f).OnComplete(() =>UpdateState(UIStates.Menu));
    }
    
    public void ChangeToSkillTree(bool tween = true)
    {
        UpdateState(UIStates.SkillTree);
        if (tween)
        {
            skillTree.DOAnchorPosX(0, .33f);
            frontPCPanel.DOLocalMoveZ(-1f, .8f);
        }
        else
        {
            skillTree.DOAnchorPosX(0, 0);
            frontPCPanel.DOLocalMoveZ(-1f, 0);
        }
    }

}
