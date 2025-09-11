// Assets/Game/SkillTree/SkillTreeAsset.cs
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTree", menuName = "Game/SkillTree/Skill Tree", order = 1)]
public class SkillTreeAsset : ScriptableObject
{
    [SerializeField] private string _version = "1.0.0";
    [SerializeField] private List<SkillNodeDef> _nodes = new();

    public string Version => _version;
    public IReadOnlyList<SkillNodeDef> Nodes => _nodes;

// #if UNITY_EDITOR
//     private void OnValidate()
//     {
//         // Check duplicate IDs
//         var dups = _nodes.GroupBy(n => n.Id).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
//         if (dups.Count > 0)
//             Debug.LogError($"Duplicate SkillNode IDs in SkillTree: {string.Join(", ", dups)}");
//
//         // Optional: cycle detection to keep the graph acyclic
//         if (HasCycle(out var cyclePath))
//             Debug.LogError($"SkillTree has a cycle: {string.Join(" -> ", cyclePath)}");
//     }
//
//     private bool HasCycle(out List<string> path)
//     {
//         var visiting = new HashSet<string>();
//         var visited  = new HashSet<string>();
//         var stack = new Stack<string>();
//         var dict = _nodes.ToDictionary(n => n.Id, n => n);
//
//         bool Dfs(string id)
//         {
//             visiting.Add(id);
//             stack.Push(id);
//             foreach (var pre in dict[id].PrerequisiteIds)
//             {
//                 if (!dict.ContainsKey(pre)) continue;
//                 if (visiting.Contains(pre)) { stack.Push(pre); return true; }
//                 if (!visited.Contains(pre) && Dfs(pre)) return true;
//             }
//             visiting.Remove(id);
//             visited.Add(id);
//             stack.Pop();
//             return false;
//         }
//
//         foreach (var n in _nodes)
//         {
//             if (!visited.Contains(n.Id))
//             {
//                 if (Dfs(n.Id))
//                 {
//                     path = stack.Reverse().ToList();
//                     return true;
//                 }
//             }
//         }
//         path = null;
//         return false;
//     }
// #endif
}
