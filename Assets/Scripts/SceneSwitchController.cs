using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadToSkillTree()
    {
        ProgressService.I.OpenSkillTree = true;
        SceneManager.LoadScene(0);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneByChildIndex(Transform child)
    {
        SceneManager.LoadScene(child.GetSiblingIndex() + 1);
    }
}
