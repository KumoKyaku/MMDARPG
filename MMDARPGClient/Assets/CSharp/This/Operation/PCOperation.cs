using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Poi
{
    public class PCOperation : Operation
    {
        enum MouseButton
        {
            LEFT = 0,
            RIGHT = 1,
            MIDDLE = 2,
        }


        Vector3 mousePos = Vector3.zero;
        public void Update()
        {
            if (GM.LockOperationCharator) return;

            if (Input.GetMouseButtonDown((int)MouseButton.LEFT) ||
                Input.GetMouseButtonDown((int)MouseButton.RIGHT) ||
                Input.GetMouseButtonDown((int)MouseButton.MIDDLE))
            {
                mousePos = Input.mousePosition;
            }

            UpdateCamera();
        }

        
        //public void FixedUpdate()
        //{
        //    if (GM.LockOperationCharator) return;

        //    float h = Input.GetAxisRaw("Horizontal");
        //    float v = Input.GetAxis("Vertical");
        //    ///优先旋转
        //    TurnEvent(h, 0);
        //    MoveEvent(0, v);


        //}

        //public void LateUpdate()
        //{
        //    if (GM.LockOperationCharator) return;
        //}

        /// <summary>
        /// 右键控制相机
        /// </summary>
        void UpdateCamera()
        {
            float delta = Input.GetAxis("Mouse ScrollWheel");
            if (delta != 0.0f)
            {
                CameraDistanceEvent(delta);
            }

            Vector3 now = Input.mousePosition;
            Vector3 diff = now - mousePos;

            if (Input.GetMouseButton((int)MouseButton.RIGHT))
            {
                if (diff.magnitude > Vector3.kEpsilon)
                {
                    int crh = (int)(diff.x) / 10;
                    ///取反
                    int crv = -(int)(diff.y) / 10;
                    CameraRotateEvent(new Vector3(crv, crh, 0.0f));
                }
            }

            mousePos = now;
        }
    }
}
