using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// Singleton service managing player progress, persistence and skill tree unlocking logic
/// </summary>
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

    /// <summary>
    /// Returns the progress for a given node, creates a default entry if not found
    /// </summary>
    /// <param name="nodeId">Unique identifier of the skill node</param>
    /// <returns>SkillProgress for the node</returns>
    public SkillProgress Get(string nodeId)
    {
        if (!_data.map.TryGetValue(nodeId, out var p))
        {
            p = new SkillProgress { completed = false, bestStars = 0, bestTimeSec = 99999 };
            _data.map[nodeId] = p;
        }
        
        return p;
    }

    /// <summary>
    /// Checks if a node is unlocked based on prerequisite completion
    /// </summary>
    /// <param name="nodeId">Unique identifier of the skill node</param>
    /// <returns>True if all prerequisites are completed or node has no prerequisites</returns>
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

    /// <summary>
    /// Checks if a node should appear locked visually despite being unlocked
    /// </summary>
    /// <param name="nodeId">Unique identifier of the skill node</param>
    /// <returns>True if the node is forced to display locked visuals</returns>
    public bool IsLockedOnlyVisually(string nodeId)
    {
        var node = _skillTree.Nodes.FirstOrDefault(n => n.Id == nodeId);
        return !node || node._forceLockedWithFullyTexturedVisuals;
    }

    /// <summary>
    /// Returns all skill nodes that are currently unlocked
    /// </summary>
    public IEnumerable<SkillNodeDef> GetUnlockedNodes() =>
        _skillTree.Nodes.Where(n => IsUnlocked(n.Id));

    /// <summary>
    /// Calculates the percentage of completed nodes in the skill tree
    /// </summary>
    /// <returns>Completion percentage (0-100)</returns>
    public float PercentComplete()
    {
        int total = _skillTree.Nodes.Count;
        if (total == 0) return 0;
        int done = _skillTree.Nodes.Count(n => Get(n.Id).completed);
        return 100f * done / total;
    }

    /// <summary>
    /// Records level result and updates best stars and time if improved
    /// </summary>
    /// <param name="nodeId">Unique identifier of the skill node</param>
    /// <param name="stars">Number of stars earned (0-3)</param>
    /// <param name="timeSec">Time taken to complete in seconds</param>
    public void RecordLevelResult(string nodeId, int stars, float timeSec)
    {
        var p = Get(nodeId);
        DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        if (stars > p.bestStars)
        {
            p.newStars++;
            p.bestStars = Mathf.Clamp(stars, 0, 3);
            ProgressChanged = true;
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

    /// <summary>
    /// Sets the currently selected node ID for scene transition
    /// </summary>
    /// <param name="nodeId">Node ID to set as current</param>
    public void SetCurrentNodeID(string nodeId)
    {
        _currentNodeID = nodeId;
    }

    /// <summary>
    /// Gets the currently selected node ID
    /// </summary>
    /// <returns>Current node ID</returns>
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

    /// <summary>
    /// Resets all player progress, re-creates empty data and saves it
    /// </summary>
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