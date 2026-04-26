using System;
using System.Collections.Generic;

/// <summary>
/// Serializable data class tracking player's progress for a single skill node
/// </summary>
[Serializable]
public class SkillProgress
{
    public bool completed;    // true if at least one “win”
    public int bestStars;     // 0–3
    public int newStars;      // 0–3
    public float bestTimeSec; // optional metric
}
