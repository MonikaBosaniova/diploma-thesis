using System;
using System.Collections.Generic;

[Serializable]
public class PlayerProgressData
{
    public string treeVersion;                      
    public Dictionary<string, SkillProgress> map = new();
}
