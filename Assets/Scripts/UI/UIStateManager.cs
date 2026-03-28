using UnityEngine;
using DG.Tweening;

public class UIStateManager : MonoBehaviour
{
    [SerializeField] private UIStates CurrentUIState;
    [SerializeField] private RectTransform skillTree;
    [SerializeField] private Transform frontPCPanel;

    private GameObject menuStateParent;
    [SerializeField] private RectTransform skillTreeBackButton;
    private float playButtonPositionX;

    private float skillTreeHiddenPositionX;
    private float skillTreeBackButtonHiddenPositionX;
    private float frontPanelStartPositionZ;
    private void Start()
    {
        menuStateParent = transform.GetChild(0).gameObject;
        skillTreeHiddenPositionX = skillTree.anchoredPosition.x;
        skillTreeBackButtonHiddenPositionX = skillTreeBackButton.anchoredPosition.x;
        frontPanelStartPositionZ = frontPCPanel.localPosition.z;
        playButtonPositionX = menuStateParent.GetComponent<RectTransform>().anchoredPosition.x;
        
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
    }

    public void ChangeToMenu()
    {
        frontPCPanel.DOLocalMoveZ(frontPanelStartPositionZ, .8f);
        skillTreeBackButton.DOAnchorPosX(skillTreeBackButtonHiddenPositionX, .33f);
        
        var canvasGroup = menuStateParent.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1f, .33f);
        canvasGroup.interactable = true;
        
        var playButtonRect = menuStateParent.GetComponent<RectTransform>();
        playButtonRect.DOAnchorPosX( playButtonPositionX , .33f);
        
        skillTree.DOAnchorPosX(skillTreeHiddenPositionX, .33f).OnComplete(() =>UpdateState(UIStates.Menu));
    }
    
    public void ChangeToSkillTree(bool tween = true)
    {
        UpdateState(UIStates.SkillTree);
        skillTreeBackButton.DOAnchorPosX(-skillTreeBackButtonHiddenPositionX, .33f);
        skillTree.DOAnchorPosX(0, tween ? .33f : 0.0f);
        frontPCPanel.DOLocalMoveZ(-1f, tween ? .8f : 0.0f);
        
        var canvasGroup = menuStateParent.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, .33f);
        canvasGroup.interactable = false;
        
        var playButtonRect = menuStateParent.GetComponent<RectTransform>();
        playButtonRect.DOAnchorPosX(playButtonPositionX - 2 * skillTreeBackButtonHiddenPositionX, .33f);
    }

}
