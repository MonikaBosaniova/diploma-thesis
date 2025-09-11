using System;
using System.Collections.Generic;

[Serializable]
public class SkillProgress
{
    public bool completed;       // true if at least one “win”
    public int bestStars;        // 0–3
    public float bestTimeSec;    // optional metric
    public int attempts;
    public long lastPlayedUnix;  // DateTimeOffset.UtcNow.ToUnixTimeSeconds()
}
