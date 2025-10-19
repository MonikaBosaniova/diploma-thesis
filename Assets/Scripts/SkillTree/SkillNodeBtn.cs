// Example: binding a button to play an unlocked level
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillNodeBtn : MonoBehaviour
{
    [SerializeField] private SkillNodeDef _node;
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _lockedOverlay;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private LevelStarsController _levelStarsController;

    private PCProgressVisualsController _progressVisualsController;

    void Start()
    {
        _progressVisualsController = FindFirstObjectByType<PCProgressVisualsController>();
        Refresh(ProgressService.I.CurrentNodeProgressChanged && (ProgressService.I.GetCurrentNodeID() == _node.Id));
        ProgressService.I.OnProgressChanged += HandleChange;
        _playButton.onClick.AddListener(() => {
            if (ProgressService.I.IsUnlocked(_node.Id))
            {
                ProgressService.I.SetCurrentNodeID(_node.Id);
                SceneManager.LoadScene(_node.SceneName);
            }
            
        });
    }

    void OnDestroy() => ProgressService.I.OnProgressChanged -= HandleChange;

    private void HandleChange(string nodeId, SkillProgress p)
    {
        if (nodeId == _node.Id || _node.PrerequisiteIds.Contains(nodeId)) Refresh(ProgressService.I.CurrentNodeProgressChanged);
    }

    private void Refresh(bool useTweening)
    {
        bool unlocked = ProgressService.I.IsUnlocked(_node.Id);
        bool done = ProgressService.I.Get(_node.Id).completed;
        int collectedStars = ProgressService.I.Get(_node.Id).bestStars;
        int newCollectedStars = 0;
        if (ProgressService.I.ChangedNodeId == _node.Id)
        {
            Debug.Log("Changed NodeId: " + _node.Id);
            newCollectedStars = ProgressService.I.NewStars;
            ProgressService.I.NewStars = 0;
            ProgressService.I.CurrentNodeProgressChanged = false;
            ProgressService.I.ChangedNodeId = "";
        }
        _lockedOverlay.SetActive(!unlocked);
        _playButton.interactable = unlocked;
        _text.text = _node.DisplayName;
        _progressVisualsController.ComponentVisibility(_node._component, unlocked, !done, false);
        _levelStarsController.ShowProgressStars(collectedStars, newCollectedStars, useTweening);
        Debug.Log("showing: " + gameObject.name + " " + useTweening + " - " + newCollectedStars);
        // if (unlocked && !done)
        // {
        //    //TODO
        //    //Make item holographic
        // }
    }
}