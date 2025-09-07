using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeLevelController : MonoBehaviour
{
    [SerializeField] int score = -1;
    [SerializeField] private SkillTreeLevelController parentLevel;
    [SerializeField] private List<SkillTreeLevelController> childLevels;
    [SerializeField] Button levelButton;
}
