using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene transitions between menu, skill tree and game levels
/// </summary>
public class SceneSwitchController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Loads the main menu scene (build index 0)
    /// </summary>
    public void LoadToMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Loads the main menu and flags ProgressService to open the skill tree
    /// </summary>
    public void LoadToSkillTree()
    {
        ProgressService.I.OpenSkillTree = true;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Loads a scene by its build index
    /// </summary>
    /// <param name="sceneIndex">Build index of the scene to load</param>
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Loads a scene based on the child's sibling index (offset by 1)
    /// </summary>
    /// <param name="child">Transform whose sibling index determines the scene</param>
    public void LoadSceneByChildIndex(Transform child)
    {
        SceneManager.LoadScene(child.GetSiblingIndex() + 1);
    }
}
