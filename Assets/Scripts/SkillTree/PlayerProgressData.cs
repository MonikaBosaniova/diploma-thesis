using System;
using System.Collections.Generic;

/// <summary>
/// Serializable data container for player progress, maps node IDs to their progress
/// </summary>
[Serializable]
public class PlayerProgressData
{
    public string treeVersion;                      
    public Dictionary<string, SkillProgress> map = new();
}
