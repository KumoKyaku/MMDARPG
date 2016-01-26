using System;
using System.Collections;
using Poi;
using UnityEngine;

public abstract class UI_Base<T> : MonoBehaviour, iUI where T : aContext
{
    [SerializeField]
    private int _ID;
    [SerializeField]
    private string _Name;

    [SerializeField]
    private GameObject touchMask;

    [Range(0, 99)]
    [SerializeField]
    private int depth;
    public int ID
    {
        get
        {
            return _ID;
        }

        set
        {
            _ID = value;
        }
    }

    public string Name
    {
        get
        {
            return _Name;
        }

        set
        {
            _Name = value;
        }
    }

    [SerializeField]
    private Only isOnly = Only.Only;

    public Only IsOnly
    {
        get
        {
            return isOnly;
        }
    }

    public string IconPath
    {
        get;

        set;
    }

    public int Depth
    {
        get
        {
            return depth;
        }
    }

    public RectTransform RectTransform { get; private set; }

    public virtual void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        SetUICamera(this);
    }

    /// <summary>
    /// 设置UI像机=====注册
    /// </summary>
    protected void SetUICamera<K>(K _UIChild) where K : iUI
    {
        if (IsOnly == Only.Only)
        {
            foreach (var item in aUIManager.UIList)
            {
                if (item.GetType() == typeof(T) && !item.Equals(this))
                {
                    Debuger.Log("销毁一个重复的UI：" + gameObject.ToString());
                    Destroy(gameObject);
                    item.gameObject.SetActive(true);
                }
            }
        }

        aUIManager.AddUI(_UIChild);
        SetDepth();
        RectTransform.offsetMax = Vector2.zero;
        RectTransform.offsetMin = Vector2.zero;
    }

    /// <summary>
    /// 设定父子关系
    /// </summary>
    public void SetDepth()
    {
        transform.SetParent(aUIManager.Depth[Depth]);
    }

    public void SetFront()
    {
        if (aUIManager.FrontUI != null)
        {
            aUIManager.FrontUI.SetDepth();
        }
        transform.SetParent(aUIManager.Depth.Front);
        aUIManager.FrontUI = this;
    }

    /// <summary>
    /// 刷新
    /// </summary>
    public virtual void Refresh()
    {
        ReText rt = gameObject.GetComponent<ReText>();
        if (rt && rt.Language != CFG.Language)
        {
            rt.ReSetText();
        }
    }

    protected T Context;
    public abstract void Use(T aContext);

    public virtual void UseDone()
    {
        StartCoroutine(Close());
    }
    /// <summary>
    /// 结局某些UI在当前帧结束会有显示不正确的问题例如Iuput会有选中色块遗留
    /// </summary>
    /// <returns></returns>
    IEnumerator Close()
    {
        yield return new WaitForEndOfFrame();
        Context = null;
        gameObject.SetActive(false);
    }

    public virtual void Help() { }


    public virtual void Test()
    {
        Debuger.Log("已响应/n" + this.ToString());
    }
}
