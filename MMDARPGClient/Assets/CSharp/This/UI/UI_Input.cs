using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UI_Input : UI_Base<InputContext>
{
    InputField inputmessag;

    // Use this for initialization
    void Start()
    {
        
        //StartCoroutine(LOG());
    }

    private IEnumerator LOG()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            Debuger.Log(inputmessag.caretPosition + "----"+ inputmessag.selectionAnchorPosition + "----" + inputmessag.selectionFocusPosition);
        }
    }

    public override void Awake()
    {
        base.Awake();
        //SetUICamera(this);

        inputmessag = gameObject.GetComponentInChildren<InputField>();
        if (inputmessag)
        {
            inputmessag.onEndEdit.AddListener(EndEdit);
            inputmessag.onValueChange.AddListener(OnValueChange);
        }
    }

    void OnValueChange(string value)
    {
        if (Context && Context.OnValueChange != null)
        {
            Context.OnValueChange(value);
        }
    }

    void EndEdit(string value)
    {
        if (Context && Context.OnSubmit != null)
        {
            Context.OnSubmit(value, this);
        }
    }

    public override void Use(InputContext aContext)
    {
        Context = aContext;
        inputmessag.text = Context.DefaultString;
        Refresh();
        gameObject.SetActive(true);
    }

}

public class InputContext : aContext
{
    public string DefaultString { get; internal set; }
    public Action<string, UI_Input> OnSubmit { get; set; }
    public Action<string> OnValueChange { get; set; }

}
