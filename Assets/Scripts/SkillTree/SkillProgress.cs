using System;
using System.Collections.Generic;

[Serializable]
public class SkillProgress
{
    public bool completed;    // true if at least one “win”
    public int bestStars;     // 0–3
    public int newStars;      // 0–3
    public float bestTimeSec; // optional metric
}
