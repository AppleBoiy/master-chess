using UnityEngine;
using System.Collections;

public class DebugLog : MonoBehaviour
{
    private readonly Queue _myLogQueue = new();

    private void Start() {
        Debug.Log("Started up logging.");
    }

    private void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable() {
        Application.logMessageReceived -= HandleLog;
    }

    /// <summary>
    /// > It takes the log string, stack trace, and log type, and adds them to a queue
    /// </summary>
    /// <param name="logString">The string you want to log.</param>
    /// <param name="stackTrace">The stack trace of the log message.</param>
    /// <param name="type">The type of log message.</param>
    private void HandleLog(string logString, string stackTrace, LogType type) {
        _myLogQueue.Enqueue("[" + type + "] : " + logString);
        if (type == LogType.Exception)
            _myLogQueue.Enqueue(stackTrace);
        while (_myLogQueue.Count > 20)
            _myLogQueue.Dequeue();
    }

    private void OnGUI() {
        GUILayout.BeginArea(new Rect(Screen.width - 330, 0, 600, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", _myLogQueue.ToArray()));
        GUILayout.EndArea();
    }
}