using UnityEngine;
using System.Collections.Generic;
using Poi;
using System;
using System.Text;

public class Debuger : MonoBehaviour
{
    public UnityEngine.UI.Text gtext;
    public Debuger Instance
    {
        get
        {
            return debuger;
        }
    }

    static Debuger debuger;

    void Awake()
    {
        debuger = this;
    }

    StringBuilder s = new StringBuilder();
    public void LateUpdate()
    {
        s = new StringBuilder();
        foreach (var item in loglist)
        {
            s.Append("\n");
            s.Append(item.Value);
        }

        gtext.text = s.ToString();
    }

    public void Clear()
    {
        lock (loglist)
        {
            loglist.Clear();
        }
    }

    public static event Action ClickEvent;

    public void Click()
    {
        if (ClickEvent != null)
        {
            ClickEvent();
        }
    }

    static List<KeyValuePair<LogType, object>> loglist = new List<KeyValuePair<LogType, object>>();

    static public void LogTest()
    {
#if UNITY_EDITOR || Development
        loglist.Add(new KeyValuePair<LogType, object>(LogType.Log, "Test"));
        Debug.Log("Test");
#endif
    }

    static public void Log(object _target)
    {
#if UNITY_EDITOR || Development
        loglist.Add(new KeyValuePair<LogType, object>(LogType.Log, _target));
        Debug.Log(_target);
#endif
    }

    static public void LogWarning(object _target)
    {
#if UNITY_EDITOR || Development
        loglist.Add(new KeyValuePair<LogType, object>(LogType.Warning, _target));
        Debug.LogWarning(_target);
#endif
    }

    static public void LogError(object _target)
    {
#if UNITY_EDITOR || Development
        loglist.Add(new KeyValuePair<LogType, object>(LogType.Error, _target));
        Debug.LogError(_target);
#endif
    }
}
