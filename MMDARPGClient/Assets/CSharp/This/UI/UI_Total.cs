using UnityEngine;
using System.Collections;
using Poi;
using System;

public class UI_Total : UI_Base<TotalContext>
{
    public GameObject total;

    // Use this for initialization
    void Start () {
        //SetUICamera(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeNicheng()
    {

    }

    public void ChangeCharator()
    {

    }

    public void Exit()
    {
        aUIManager.GetUI<UI_Ask>().Use(new AskContext()
        {
            Message = "退出游戏？",
            Callback = (yn,ui) =>
            {
                ui.UseDone();
                if (yn)
                {
                    GM.Exit(1);
                }
            }
        });
    }

    public void Hidden(bool hidden)
    {
        total.SetActive(hidden);
        GM.LockOperationCharator = hidden;
    }

    public override void Use(TotalContext aContext)
    {
        Debuger.LogWarning("这个UI不应该使用");
    }

    public override void UseDone()
    {
    }
}

public class TotalContext:aContext
{

}
