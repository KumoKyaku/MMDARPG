using UnityEngine;
using System.Collections;
using Poi;

/// <summary>
/// 第三视角跟随相机
/// </summary>
[RequireComponent(typeof(Camera))]
public class FollowThirdCamera : MonoBehaviour
{

    GameObject focus = null;

    public Camera Camera { get; private set; }
    /// <summary>
    /// 摄像机跟随的物体
    /// </summary>
    public GameObject focusfollow = null;

    public Vector3 defaultOffsetPos = new Vector3(0,0,-8);
    public Vector3 defaultOffsetRotation = new Vector3(20,0,0);
    [Range(0.1f, 3f)]
    public float distanceScale = 1f;
    [Range(0.1f, 3f)]
    public float rotationScale = 2f;
    [Range(0.1f, 3f)]
    public float translateScale = 1f;
    // Use this for initialization

    /// <summary>
    /// Y轴锁死？暂不启用
    /// </summary>
    private bool YLock = false;

    void Start()
    {

        Camera = gameObject.GetComponent<Camera>();
        CreateFocus();
    }

    void CreateFocus()
    {
        if (focus == null)
        {
            focus = new GameObject("LookCamera");
        }

        focus.transform.position = Vector3.zero;
        focus.transform.rotation = Quaternion.identity;

        transform.SetParent(focus.transform);
        Default();
    }

    void LateUpdate()
    {
        if (focusfollow && focus)
        {
            focus.transform.position = focusfollow.transform.position;
        }
    }

    public void Default()
    {
        ///XY轴清除。保证相机在初始状态是水平的。否则将导致相机无法看见正下方或者正上方
        transform.localPosition = defaultOffsetPos.NoX().NoY();

        if (focus)
        {
            transform.LookAt(focus.transform);
        }

        ///去掉焦点的Z轴旋转
        focus.transform.localEulerAngles = defaultOffsetRotation.NoZ();
    }

    public void mouseWheelEvent(float delta)
    {
        var now = transform.position - focus.transform.position;
        if (now.magnitude < 0.2)
        {
            now = Vector3.back;
        }
        ///本次变化规范化
        var d = now.normalized * delta * distanceScale;

        ///应用举例改变
        transform.Translate(d, Space.World);

        ///矫正摄像机
        transform.localEulerAngles = transform.localEulerAngles.NoZ();
    }

    /// <summary>
    /// 当前不支持拖拽
    /// </summary>
    /// <param name="vec"></param>
    public void cameraTranslate(Vector3 vec)
    {
        Vector3 tr = new Vector3(-vec.x, vec.y, 0) * translateScale;
        transform.Translate(tr);
    }

    public void cameraRotate(Vector3 eulerAngle)
    {
        focus.transform.localEulerAngles = (focus.transform.localEulerAngles + eulerAngle * rotationScale).NoZ();
    }
}
