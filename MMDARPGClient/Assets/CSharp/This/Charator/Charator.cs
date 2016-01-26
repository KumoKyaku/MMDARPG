using UnityEngine;
using System.Collections;
using Poi;
using System;

/// <summary>
/// 角色
/// </summary>
public partial class Charator : MonoBehaviour, iCharator
{
    private CapsuleCollider feetcollider;
    private new Rigidbody rigidbody;


    public virtual bool IsPlayer { get { return false; } }
    public string IconPath { get; set; }
    public int ID { get; set; }
    public string Name { get; set; }
    public Data4Charator Data { get; set; }

    public string NiCheng { get; set; }

    public int InstanceID { get; set; }
    public CharatorType RigType { get; private set; }

    protected GameObject modelCenter;

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        InstanceID = MakeInstanceID.ID;

        InitData4Player();

        InitFromData();

        InitAnimator();

        InitState();

        InitOperation();

        InitCarema();
    }

    public void Set(iTransform transform_Poi)
    {
        throw new NotImplementedException();
    }

    public void Set(iData data)
    {
        Data = data as Data4Charator;
    }

    public void Set(iOperation operation)
    {
        throw new NotImplementedException();
    }

    public void Set(CharatorType rigType)
    {
        RigType = rigType;
    }

    /// <summary>
    /// 初始化数值
    /// </summary>
    private void InitFromData()
    {
        ID = Data.ID;
        Name = Data.Name;

        var modelCenter = new Vector3(0, Data.Height / 2, 0);

        GameObject cl = new GameObject("ModelCenter");
        cl.transform.SetParent(transform);
        cl.transform.localPosition = modelCenter;
        cl.transform.localRotation = Quaternion.Euler(0, 0, 0);
        this.modelCenter = cl;

        feetcollider = gameObject.AddComponent<CapsuleCollider>();
        feetcollider.radius = Data.Height / 8;
        feetcollider.height = Data.Height;
        feetcollider.center = new Vector3(0, feetcollider.height / 2 - 0.005f, 0);
        feetcollider.direction = 1;
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = true;

    }

    protected virtual void InitData4Player()
    {
    }


    
}
