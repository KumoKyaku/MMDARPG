using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Poi;

public class UI_Ask: UI_Base<AskContext>
{

    [SerializeField]
    private Text message;

    string helpMessage;

    public Text Message
    {
        get
        {
            return message;
        }
    }

    // Use this for initialization
    void Start () {
        //SetUICamera(this);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Yes()
    {
        if (Context.Callback != null)
        {
            Context.Callback(true,this);
        }
    }

    public void No()
    {
        if (Context.Callback!= null)
        {
            Context.Callback(false, this);
        }
        else
        {
            UseDone();
        }
    }

    public override void Help()
    {
        if (Context.HelpCallback != null)
        {
            Context.HelpCallback();
        }
    }

    public override void Use(AskContext aContext)
    {
        Context = aContext;
        message.text = aContext.Message;
        helpMessage = aContext.HelpMessage;
    }
}

public class AskContext :aContext
{
    public Action<bool, UI_Ask> Callback { get; internal set; }
    public Action HelpCallback { get; internal set; }
    public string HelpMessage { get; internal set; }
    public string Message { get; internal set; }
}