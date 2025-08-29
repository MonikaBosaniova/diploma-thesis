using System;
using UnityEngine;

public class UIStateManager : MonoBehaviour
{
    [SerializeField] private UIStates CurrentUIState;

    private GameObject menuStateParent;
    private GameObject skillTreeParent;
    private void Start()
    {
        menuStateParent = transform.GetChild(0).gameObject;
        skillTreeParent = transform.GetChild(1).gameObject;
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
        UpdateState(UIStates.Menu);
    }
    
    public void ChangeToSkillTree()
    {
        UpdateState(UIStates.SkillTree);
    }

}
