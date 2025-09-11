// Assets/Game/SkillTree/ProgressService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ProgressService : MonoBehaviour
{
    public static ProgressService I { get; private set; }

    [Header("Config")]
    [SerializeField] private SkillTreeAsset _skillTree;

    public event Action<string, SkillProgress> OnProgressChanged; // nodeId, new progress

    private PlayerProgressData _data;

    private string SavePath =>
        Path.Combine(Application.persistentDataPath, "progress.json");

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        LoadOrCreate();
        EnsureVersion();
    }

    public SkillProgress Get(string nodeId)
    {
        if (!_data.map.TryGetValue(nodeId, out var p))
        {
            p = new SkillProgress { completed = false, bestStars = 0, bestTimeSec = float.PositiveInfinity, attempts = 0, lastPlayedUnix = 0 };
            _data.map[nodeId] = p;
        }
        return p;
    }

    public bool IsUnlocked(string nodeId)
    {
        var node = _skillTree.Nodes.FirstOrDefault(n => n.Id == nodeId);
        if (node == null) return false; // unknown node -> locked
        if (node.PrerequisiteIds == null || node.PrerequisiteIds.Count == 0) return true;
        foreach (var pre in node.PrerequisiteIds)
        {
            var p = Get(pre);
            if (!p.completed) return false;
        }
        return true;
    }

    public IEnumerable<SkillNodeDef> GetUnlockedNodes() =>
        _skillTree.Nodes.Where(n => IsUnlocked(n.Id));

    public float PercentComplete()
    {
        int total = _skillTree.Nodes.Count;
        if (total == 0) return 0;
        int done = _skillTree.Nodes.Count(n => Get(n.Id).completed);
        return 100f * done / total;
    }

    public void RecordLevelResult(string nodeId, int stars, float timeSec)
    {
        var p = Get(nodeId);
        p.attempts++;
        p.lastPlayedUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        if (stars > p.bestStars) p.bestStars = Mathf.Clamp(stars, 0, 3);
        if (timeSec >= 0 && timeSec < p.bestTimeSec) p.bestTimeSec = timeSec;
        if (stars > 0) p.completed = true;

        Save();
        OnProgressChanged?.Invoke(nodeId, p);
    }

    public SkillTreeAsset Tree => _skillTree;

    // -------- persistence --------
    private void LoadOrCreate()
    {
        try
        {
            if (File.Exists(SavePath))
            {
                var json = File.ReadAllText(SavePath);
                _data = JsonUtility.FromJson<PlayerProgressData>(json);
            }
        }
        catch (Exception e) { Debug.LogWarning($"Load progress failed: {e}"); }

        if (_data == null) _data = new PlayerProgressData { treeVersion = _skillTree.Version };
        if (_data.map == null) _data.map = new Dictionary<string, SkillProgress>();
    }

    private void Save()
    {
        try
        {
            _data.treeVersion = _skillTree.Version;
            var json = JsonUtility.ToJson(_data, prettyPrint: true);
            File.WriteAllText(SavePath, json);
        }
        catch (Exception e) { Debug.LogWarning($"Save progress failed: {e}"); }
    }

    private void EnsureVersion()
    {
        // If tree version changes (nodes added/removed), decide policy:
        // Here: keep existing entries; missing nodes will be created lazily in Get().
        // You could also implement migrations based on semantic versioning.
        if (_data.treeVersion != _skillTree.Version)
        {
            // Example: if nodes were removed, optionally prune:
            var validIds = new HashSet<string>(_skillTree.Nodes.Select(n => n.Id));
            var keysToRemove = _data.map.Keys.Where(id => !validIds.Contains(id)).ToList();
            foreach (var k in keysToRemove) _data.map.Remove(k);

            _data.treeVersion = _skillTree.Version;
            Save();
        }
    }
}
