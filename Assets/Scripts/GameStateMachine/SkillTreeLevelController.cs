using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Deprecated level controller for the skill tree, stores score and parent/child level relationships
/// </summary>
public class SkillTreeLevelController : MonoBehaviour
{
    [SerializeField] int score = -1;
    [SerializeField] private SkillTreeLevelController parentLevel;
    [SerializeField] private List<SkillTreeLevelController> childLevels;
    [SerializeField] Button levelButton;
}
