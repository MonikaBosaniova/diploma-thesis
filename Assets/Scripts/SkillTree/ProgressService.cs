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
    public bool ProgressChanged = false;
    public bool OpenSkillTree = false;
    //public int NewStars = 0;
    public string ChangedNodeId = "";
    
    private PlayerProgressData _data;

    private string SavePath =>
        Path.Combine(Application.persistentDataPath, "progress.json");

    private string _currentNodeID;

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        LoadOrCreate();
    }

    public SkillProgress Get(string nodeId)
    {
        if (!_data.map.TryGetValue(nodeId, out var p))
        {
            p = new SkillProgress { completed = false, bestStars = 0, bestTimeSec = 99999 };
            _data.map[nodeId] = p;
        }
        
        return p;
    }

    public bool IsUnlocked(string nodeId)
    {
        var node = _skillTree.Nodes.FirstOrDefault(n => n.Id == nodeId);
        if (!node) 
            return false; // unknown node -> locked

        if (node.PrerequisiteIds == null || node.PrerequisiteIds.Count == 0 || node._makeUnlockedAtStart)
            return true;

        foreach (var pre in node.PrerequisiteIds)
        {
            var p = Get(pre);
            if (!p.completed)
                return false;
        }

        return true;
    }

    public bool IsLockedOnlyVisually(string nodeId)
    {
        var node = _skillTree.Nodes.FirstOrDefault(n => n.Id == nodeId);
        return !node || node._forceLockedVisuals;
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
        DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        if (stars > p.bestStars)
        {
            p.newStars++;
            p.bestStars = Mathf.Clamp(stars, 0, 3);
            ProgressChanged = true;
            //ChangedNodeId =  nodeId;
        }
        else
        {
            p.newStars = 0;
            ProgressChanged = false;
        }
        if (timeSec >= 0 && timeSec < p.bestTimeSec) p.bestTimeSec = timeSec;
        if (stars > 0) p.completed = true;
        
        Save();
        OnProgressChanged?.Invoke(nodeId, p);
    }

    public void SetCurrentNodeID(string nodeId)
    {
        _currentNodeID = nodeId;
    }

    public string GetCurrentNodeID()
    {
        return _currentNodeID;
    }

    public SkillTreeAsset Tree => _skillTree;

    // -------- persistence --------
    private const string PLAYER_PREFS_KEY = "PLAYER_PROGRESS_JSON";

    private void LoadOrCreate()
    {
        if (PlayerPrefs.HasKey(PLAYER_PREFS_KEY))
        {
            string json = PlayerPrefs.GetString(PLAYER_PREFS_KEY);

            try
            {
                var file = JsonUtility.FromJson<ProgressFile>(json);

                if (file != null)
                {
                    _data = new PlayerProgressData
                    {
                        treeVersion = file.version,
                        map = new Dictionary<string, SkillProgress>()
                    };

                    foreach (var entry in file.data)
                    {
                        _data.map[entry.id] = entry.value;
                    }

                    LoggerService.Instance.Log("ProgressService: Save data successfully loaded");
                    
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("ProgressService: Save data failed to load, creating new, error: " + e);
            }
        }

        // fallback
        _data = new PlayerProgressData
        {
            treeVersion = _skillTree != null ? _skillTree.Version : "1.0",
            map = new Dictionary<string, SkillProgress>()
        };

        Save();
    }

    private void Save()
    {
        if (_data == null) return;

        var file = new ProgressFile
        {
            version = _data.treeVersion,
            data = new List<NodeEntry>()
        };

        foreach (var kv in _data.map)
        {
            file.data.Add(new NodeEntry
            {
                id = kv.Key,
                value = kv.Value
            });
        }

        string json = JsonUtility.ToJson(file);
        PlayerPrefs.SetString(PLAYER_PREFS_KEY, json);
        PlayerPrefs.Save();
    }

    public void RemoveProgress()
    {
        // Reset runtime flags
        ProgressChanged = false;
        OpenSkillTree = false;
        ChangedNodeId = "";
        _currentNodeID = null;

        // Recreate fresh data
        _data = new PlayerProgressData
        {
            treeVersion = _skillTree != null ? _skillTree.Version : "1.0",
            map = new Dictionary<string, SkillProgress>()
        };

        // Pre-create entries for all nodes (clean state)
        if (_skillTree != null)
        {
            foreach (var node in _skillTree.Nodes)
            {
                _data.map[node.Id] = new SkillProgress
                {
                    completed = false,
                    bestStars = 0,
                    newStars = 0,
                    bestTimeSec = 99999,
                };
            }
        }

        Save();

        if (_skillTree != null)
        {
            foreach (var node in _skillTree.Nodes)
            {
                OnProgressChanged?.Invoke(node.Id, _data.map[node.Id]);
            }
        }
        
        LoggerService.Instance.Log("ProgressService: Progress reset and saved");
    }
}

[Serializable]
public class ProgressFile
{
    public string version;
    public List<NodeEntry> data = new();
}

[Serializable]
public class NodeEntry
{
    public string id;
    public SkillProgress value;
}