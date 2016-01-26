using UnityEngine;
using System.Collections;
using System;
using Poi;

/// <summary>
/// Just 4 example
/// </summary>
public class UIManager : aUIManager
{
    public void Awake()
    {
        Depth = transform.GetComponentInChildren<UI_Depth>();
        if (Depth)
        {
            
        }
        else
        {
            Debuger.LogError("UI层级缺失");
        }
    }

    void Start()
    {
        StartCoroutine(WaitSplash());
    }

    private IEnumerator WaitSplash()
    {
        while (!GM.SplashDone)
        {
            yield return new WaitForSeconds(1);
        }
        GetUI<UI_Total>();
    }
}
