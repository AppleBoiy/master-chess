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

    private void HandleLog(string logString, string stackTrace, LogType type) {
        _myLogQueue.Enqueue("[" + type + "] : " + logString);
        if (type == LogType.Exception)
            _myLogQueue.Enqueue(stackTrace);
        while (_myLogQueue.Count > 20)
            _myLogQueue.Dequeue();
    }

    private void OnGUI() {
        GUILayout.BeginArea(new Rect(Screen.width - 230, 0, 600, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", _myLogQueue.ToArray()));
        GUILayout.EndArea();
    }
}