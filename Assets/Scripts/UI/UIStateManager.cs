using UnityEngine;
using DG.Tweening;

/// <summary>
/// Manages UI state transitions (Menu, SkillTree, Game) and controls visibility of UI panels
/// </summary>
public class UIStateManager : MonoBehaviour
{
    [SerializeField] private UIStates CurrentUIState;
    [SerializeField] private RectTransform skillTree;
    [SerializeField] private Transform frontPCPanel;

    private GameObject menuStateParent;
    [SerializeField] private RectTransform skillTreeBackButton;
    private float _playButtonPositionX;

    private float _skillTreeHiddenPositionX;
    private float _skillTreeBackButtonHiddenPositionX;
    private float _frontPanelStartPositionZ;
    private void Start()
    {
        menuStateParent = transform.GetChild(0).gameObject;
        _skillTreeHiddenPositionX = skillTree.anchoredPosition.x;
        _skillTreeBackButtonHiddenPositionX = skillTreeBackButton.anchoredPosition.x;
        _frontPanelStartPositionZ = frontPCPanel.localPosition.z;
        _playButtonPositionX = menuStateParent.GetComponent<RectTransform>().anchoredPosition.x;
        
        if (ProgressService.I.OpenSkillTree)
        {
            ChangeToSkillTree(false);
        }
        else
        {
            ChangeToMenu();
        }
    }
    
    /// <summary>
    /// Updates the current UI state enum value
    /// </summary>
    /// <param name="state">New UI state</param>
    public void UpdateState(UIStates state)
    {
        CurrentUIState = state;
    }

    /// <summary>
    /// Transitions UI to the main menu state with tween animations
    /// </summary>
    public void ChangeToMenu()
    {
        frontPCPanel.DOLocalMoveZ(_frontPanelStartPositionZ, .8f);
        skillTreeBackButton.DOAnchorPosX(_skillTreeBackButtonHiddenPositionX, .33f);
        
        var canvasGroup = menuStateParent.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1f, .33f);
        canvasGroup.interactable = true;
        
        var playButtonRect = menuStateParent.GetComponent<RectTransform>();
        playButtonRect.DOAnchorPosX( _playButtonPositionX , .33f);
        
        skillTree.DOAnchorPosX(_skillTreeHiddenPositionX, .33f).OnComplete(() =>UpdateState(UIStates.Menu));
    }
    
    /// <summary>
    /// Transitions UI to the skill tree state with optional tween animations
    /// </summary>
    /// <param name="tween">Whether to animate the transition (false for instant switch)</param>
    public void ChangeToSkillTree(bool tween = true)
    {
        UpdateState(UIStates.SkillTree);
        skillTreeBackButton.DOAnchorPosX(-_skillTreeBackButtonHiddenPositionX, .33f);
        skillTree.DOAnchorPosX(0, tween ? .33f : 0.0f);
        frontPCPanel.DOLocalMoveZ(-1f, tween ? .8f : 0.0f);
        
        var canvasGroup = menuStateParent.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, .33f);
        canvasGroup.interactable = false;
        
        var playButtonRect = menuStateParent.GetComponent<RectTransform>();
        playButtonRect.DOAnchorPosX(_playButtonPositionX - 2 * _skillTreeBackButtonHiddenPositionX, .33f);
    }
}
