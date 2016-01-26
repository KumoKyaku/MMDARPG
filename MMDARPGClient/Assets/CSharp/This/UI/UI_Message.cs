using UnityEngine;
using System.Collections;
using Poi;
using System;
using UnityEngine.UI;

public class UI_Message : UI_Base<MessageContext> {

    public Text message;
    const string pre = @"提示：";
    // Use this for initialization
    void Start () {
        //SetUICamera(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    double delay = 10;
    public override void Use(MessageContext aContext)
    {
        Context = aContext;
        message.text = pre + Context.Message;
        delay = 10;
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        while (delay > 0)
        {
            yield return new WaitForSeconds(1);
            delay--;
        }

        UseDone();
    }
}

public class MessageContext : aContext
{
    public string Message { get; internal set; }
}
