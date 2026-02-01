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

    private bool visible = false;
    private bool hologram = false;
    private bool outlined = false;

    void Start()
    {
        _progressVisualsController = FindFirstObjectByType<PCProgressVisualsController>();
        Refresh(ProgressService.I.ProgressChanged && (ProgressService.I.GetCurrentNodeID() == _node.Id));
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
        if (nodeId == _node.Id || _node.PrerequisiteIds.Contains(nodeId)) Refresh(ProgressService.I.ProgressChanged);
    }

    private void Refresh(bool useTweening)
    {
        bool unlocked = ProgressService.I.IsUnlocked(_node.Id);
        bool forceLock = ProgressService.I.IsLockedOnlyVisually(_node.Id);
        bool done = ProgressService.I.Get(_node.Id).completed;
        int collectedStars = ProgressService.I.Get(_node.Id).bestStars;
        int newCollectedStars = ProgressService.I.Get(_node.Id).newStars;
        // if (ProgressService.I.ChangedNodeId == _node.Id)
        // {
        //     newCollectedStars = ProgressService.I.NewStars;
        //     Debug.Log("Changed NodeId: " + _node.Id + "stars: " + newCollectedStars);
        //     ProgressService.I.NewStars = 0;
        //     ProgressService.I.CurrentNodeProgressChanged = false;
        //     ProgressService.I.ChangedNodeId = "";
        // }
        _lockedOverlay.SetActive(!unlocked);
        _lockedOverlay.SetActive(forceLock);
        _playButton.interactable = unlocked;
        _text.text = _node.DisplayName;
        
        visible = (forceLock || done || unlocked);
        hologram = (unlocked && !done);
        outlined = false;
        _progressVisualsController.ComponentVisibility(_node._component, visible, hologram, outlined);
        if (_node._component == PCComponent.CoolingUnit)
        {
            _progressVisualsController.ComponentVisibility(done ? PCComponent.VentilatorON : PCComponent.VentilatorOFF,
                visible, hologram, outlined);
            _progressVisualsController.ComponentVisibility(!done ? PCComponent.VentilatorON : PCComponent.VentilatorOFF,
                !visible, !hologram, !outlined);
        }
            
        Debug.Log("showing: " + gameObject.name + " " + useTweening + " - " + newCollectedStars);
        _levelStarsController.ShowProgressStars(collectedStars, newCollectedStars, useTweening);
        ProgressService.I.Get(_node.Id).newStars = 0;
    }

    public void SetOutLine(bool outlined)
    {
        //TODO BUG WITH OUTLINE, outlining other components and throwing errors to normals, all fbx were check to read/write true
        // if (_node._component == PCComponent.None) return;
        // Debug.Log("setting outline to: " + outlined + " --- " + _node._component);
        // _progressVisualsController.ComponentVisibility(_node._component, visible, hologram, outlined);
    }
}