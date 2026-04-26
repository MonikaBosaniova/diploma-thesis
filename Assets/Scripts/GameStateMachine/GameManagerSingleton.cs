using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Persistent singleton that survives scene loads and tracks completed levels count
/// </summary>
public class GameManagerSingleton : MonoBehaviour
{
    public static GameManagerSingleton Instance { get; private set; }
    public int CompleteLevelsCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
