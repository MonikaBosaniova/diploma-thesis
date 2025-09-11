// Assets/Game/SkillTree/SkillNodeDef.cs
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SkillNode", menuName = "Game/SkillTree/Skill Node", order = 0)]
public class SkillNodeDef : ScriptableObject
{
    [SerializeField] private string _id = Guid.NewGuid().ToString();   // Stable GUID
    [SerializeField] private string _displayName;
    [SerializeField] private string _sceneName; // name from Build Settings
    [SerializeField] internal PCComponent _component;
    [SerializeField] private List<string> _prerequisiteIds = new();

    public string Id => _id;
    public string DisplayName => _displayName;
    public string SceneName => _sceneName;
    public IReadOnlyList<string> PrerequisiteIds => _prerequisiteIds;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!string.IsNullOrWhiteSpace(_sceneName))
        {
            bool sceneInBuild = EditorBuildSettings.scenes
                .Any(s => s.enabled && Path.GetFileNameWithoutExtension(s.path) == _sceneName);

            if (!sceneInBuild)
                Debug.LogWarning($"Scene '{_sceneName}' not found (enabled) in Build Settings for node '{name}'.");
        }
    }
#endif
}