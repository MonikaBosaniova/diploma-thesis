using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class LoggerService : MonoBehaviour
{
    public static LoggerService Instance { get; private set; }

    private static string _sessionId;
    private static string _environment;
    private static string _version;

    private static readonly string ObfuscatedAPIKey = "ZXUwMXh4NDk4MTY2ZGUyZDY4M2VlNGVlOWNlNzg1NjZGRkZGTlJBTA==";
    private static readonly string APIKey = Encoding.UTF8.GetString(Convert.FromBase64String(ObfuscatedAPIKey));
    private static readonly string NewRelicApiUrl = "https://log-api.eu.newrelic.com/log/v1?Api-Key=" + APIKey;

    private int rateLimitingCountdown = 1000;

    private static readonly Regex[] IgnoreFilters = new Regex[]
    {
        new Regex(@"^some random rule$", RegexOptions.Compiled),
    };
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _sessionId = System.Guid.NewGuid().ToString();
        _version = Application.version;

#if UNITY_EDITOR
        _environment = "Development";
#else
        _environment = "Production";
#endif

        Application.logMessageReceived += (message, stackTrace, type) =>
        {
            if (type == LogType.Error || type == LogType.Exception)
                LogError(message, stackTrace);

            if (type == LogType.Warning || type == LogType.Assert)
                LogWarning(message, stackTrace);
        };

        DontDestroyOnLoad(gameObject);

        Log("Game Started");
    }

    private Dictionary<string, object> CreateLogBase(string severity, string message)
    {
        return new Dictionary<string, object>
        {
            { "sessionId", _sessionId },
            { "environment", _environment },
            { "version", _version },
            { "sceneName", SceneManager.GetActiveScene().name },
            { "severity", severity },
            { "message", message }
        };
    }

    private void SendLog(Dictionary<string, object> data)
    {
        if (data.ContainsKey("message") && data["message"] is string message && ShouldIgnore(message))
            return;

        string json = DictionaryToJson(data);
        StartCoroutine(PostRequestCoroutine(json));
    }

    public void Log(string message, Dictionary<string, object> attributes = null)
    {
        var data = CreateLogBase("log", message);
        
        if(attributes != null)
            data.AddRange(attributes);
        
        SendLog(data);
    }

    public void LogProgressInLevel(string state, float time)
    {
        var data = CreateLogBase("log", "progress_in_level");

        data["state"] = state;
        data["time"] = time;

        SendLog(data);
    }

    public void LogQuizAnswer(bool quizBeforeGame, int questionId, bool isCorrect, int answerId)
    {
        var data = CreateLogBase("log", "quiz_answer");

        data["questionIndex"] = questionId;
        data["isCorrectAnswer"] = isCorrect;
        data["answerIndex"] = answerId;
        data["quizBeforeGame"] = quizBeforeGame;

        SendLog(data);
    }

    public void LogWarning(string message, string stackTrace)
    {
        if(rateLimitingCountdown-- < 0) return;
        
        var data = CreateLogBase("warning", message);
        
        data["stackTrace"] = stackTrace;

        SendLog(data);
    }

    public void LogError(string message, string stackTrace)
    {
        if(rateLimitingCountdown-- < 0) return;
        
        var data = CreateLogBase("error", message);
        
        data["stackTrace"] = stackTrace;

        SendLog(data);
    }

    private string DictionaryToJson(Dictionary<string, object> dictionary)
    {
        StringBuilder json = new StringBuilder();
        json.Append("{");

        bool first = true;

        foreach (KeyValuePair<string, object> kvp in dictionary)
        {
            if (!first)
                json.Append(",");

            first = false;

            json.Append("\"");
            json.Append(EscapeString(kvp.Key));
            json.Append("\":");
            json.Append(ConvertValueToJson(kvp.Value));
        }

        json.Append("}");

        string result = json.ToString();
        Debug.Log(result);

        return result;
    }

    private string EscapeString(string str) => str
        .Replace("\\", "\\\\")
        .Replace("\"", "\\\"")
        .Replace("\n", "\\n")
        .Replace("\r", "\\r")
        .Replace("\t", "\\t");

    private string ConvertValueToJson(object value)
    {
        if (value == null)
            return "null";

        if (value is string)
            return "\"" + EscapeString(value.ToString()) + "\"";

        if (value is bool)
            return ((bool)value) ? "true" : "false";

        if (value is int || value is float || value is double || value is long)
            return value.ToString();

        return "\"" + EscapeString(value.ToString()) + "\"";
    }
    
    private bool ShouldIgnore(string message)
    {
        if (string.IsNullOrEmpty(message))
            return false;

        foreach (var regex in IgnoreFilters)
        {
            if (regex.IsMatch(message))
                return true;
        }

        return false;
    }
    
    private IEnumerator PostRequestCoroutine(string jsonBody)
    {
        var request = new UnityWebRequest(NewRelicApiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
    }
    
}