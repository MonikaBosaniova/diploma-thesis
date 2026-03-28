using UnityEngine;
using DG.Tweening;

public class UIStateManager : MonoBehaviour
{
    [SerializeField] private UIStates CurrentUIState;
    [SerializeField] private RectTransform skillTree;
    [SerializeField] private Transform frontPCPanel;

    private GameObject menuStateParent;
    [SerializeField] private RectTransform skillTreeBackButton;
    
    private float skillTreeHiddenPositionX;
    private float skillTreeBackButtonHiddenPositionX;
    private float frontPanelStartPositionZ;
    private void Start()
    {
        menuStateParent = transform.GetChild(0).gameObject;
        skillTreeHiddenPositionX = skillTree.anchoredPosition.x;
        skillTreeBackButtonHiddenPositionX = skillTreeBackButton.anchoredPosition.x;
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
                break;
            case UIStates.Menu:
                menuStateParent.SetActive(true);
                break;
            case UIStates.SkillTree:
                menuStateParent.SetActive(false);
                break;
        }
    }

    public void ChangeToMenu()
    {
        frontPCPanel.DOLocalMoveZ(frontPanelStartPositionZ, .8f);
        skillTreeBackButton.DOAnchorPosX(skillTreeBackButtonHiddenPositionX, .33f);
        skillTree.DOAnchorPosX(skillTreeHiddenPositionX, .33f).OnComplete(() =>UpdateState(UIStates.Menu));
    }
    
    public void ChangeToSkillTree(bool tween = true)
    {
        UpdateState(UIStates.SkillTree);
        skillTreeBackButton.DOAnchorPosX(-skillTreeBackButtonHiddenPositionX, .33f);
        skillTree.DOAnchorPosX(0, tween ? .33f : 0.0f);
        frontPCPanel.DOLocalMoveZ(-1f, tween ? .8f : 0.0f);
    }

}
