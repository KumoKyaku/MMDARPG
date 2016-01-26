using System;
using Poi;

namespace UnityEngine
{
    /// <summary>
    /// 为UnityEngine提供扩展
    /// </summary>
    public static partial class Extension
    {
        #region RectTransform

        /// <summary>
        /// 调整大小和位置
        /// </summary>
        /// <param name="_trans"></param>
        /// <param name="_anchor">锚点位置</param>
        /// <param name="offsetMin">左下脚坐标</param>
        /// <param name="offsetMax">右上角坐标</param>
        public static void Resize(this RectTransform _trans,Anchor _anchor, Vector2 offsetMin, Vector2 offsetMax)
        {
            _trans.Resize(_anchor.Min,_anchor.Max,offsetMin,offsetMax);
        }

        /// <summary>
        /// 调整大小和位置
        /// </summary>
        /// <param name="_trans"></param>
        /// <param name="anchormin"></param>
        /// <param name="anchormax"></param>
        /// <param name="offsetMin"></param>
        /// <param name="offsetMax"></param>
        public static void Resize(this RectTransform _trans,Vector2 anchormin, Vector2 anchormax
            , Vector2 offsetMin, Vector2 offsetMax)
        {
            _trans.anchorMin = anchormin;
            _trans.anchorMax = anchormax;
            _trans.offsetMin = offsetMin;
            _trans.offsetMax = offsetMax;
        }

        /// <summary>
        /// 设置锚点
        /// </summary>
        /// <param name="_trans"></param>
        /// <param name="_anchor"></param>
        public static void SetAnchor(this RectTransform _trans, Anchor _anchor)
        {
            _trans.anchorMin = _anchor.Min;
            _trans.anchorMax = _anchor.Max;
        }

        /// <summary>
        /// 坐标旋转归零，缩放归一。
        /// </summary>
        /// <param name="_trans"></param>
        public static void ReSet(this Transform _trans)
        {
            _trans.localPosition = Vector3.zero;
            _trans.localRotation = Quaternion.Euler(Vector3.zero);
            _trans.localScale = Vector3.one;
        }
        #endregion

        /// <summary>
        /// 测试方法Wait
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script"></param>
        /// <param name="para"></param>
        /// <param name="value"></param>
        /// <param name="Callback"></param>
        [TesT]
        public static void Wait(this MonoBehaviour script,Task para,Action Callback)
        {
            script.StartCoroutine(Wait(para,Callback));
        }

        public static void Wait(this MonoBehaviour script, YieldInstruction wait, Action Callback)
        {
            script.StartCoroutine(Wait(wait, Callback));
        }

        private static System.Collections.IEnumerator Wait(Task para, Action Callback)
        {
            while (!para.IsDone)
            {
                yield return null;
            }
            if (Callback != null)
            {
                Callback();
            }
        }

        private static System.Collections.IEnumerator Wait(YieldInstruction wait, Action Callback)
        {
            yield return wait;
            if (Callback != null)
            {
                Callback();
            }
        }

        public static void Wait<T>(this MonoBehaviour script, Task<T> para, Action Callback)
        {
            script.StartCoroutine(Wait(para, Callback));
        }

        private static System.Collections.IEnumerator Wait<T>(Task<T> para, Action Callback)
        {
            while (!para.IsDone)
            {
                yield return null;
            }
            if (Callback != null)
            {
                Callback();
            }
        }




        #region Vector3

        static readonly Vector3 noX = new Vector3(0, 1, 1);
        /// <summary>
        /// X轴清除
        /// </summary>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vector3 NoX(this Vector3 v3)
        {
            return Vector3.Scale(v3, noX);
        }

        static readonly Vector3 noY = new Vector3(1, 0, 1);
        /// <summary>
        /// Y轴清除
        /// </summary>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vector3 NoY(this Vector3 v3)
        {
            return Vector3.Scale(v3, noY);
        }

        static readonly Vector3 noZ = new Vector3(1, 1, 0);
        /// <summary>
        /// Z轴清除
        /// </summary>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vector3 NoZ(this Vector3 v3)
        {
            return Vector3.Scale(v3, noZ);
        }

        #endregion
    }
}
