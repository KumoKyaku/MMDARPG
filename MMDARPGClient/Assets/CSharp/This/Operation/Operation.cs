using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Poi
{
    /// <summary>
    /// 操作
    /// </summary>
    public abstract class Operation : MonoBehaviour, iOperation
    {
        private Charator target;

        public OperationFrom OperationFrom { get; private set; }
        public void SetTarget(Charator target)
        {
            this.target = target;
        }

        public event Action AAA;
        public event Action AAB;

        public event Action Jump;
        public event Action<float,float> Move;
        public event Action<float, float> Turn;
        public event Action<float> CameraDistance;
        public event Action<Vector3> CameraDrag;
        public event Action<Vector3> CameraRotate;
        protected void JumpEvent()
        {
            if (Jump!= null)
            {
                Jump();
            }
        }

        protected void MoveEvent(float h,float v)
        {
            if (Move != null)
            {
                Move(h,v);
            }
        }

        protected void TurnEvent(float h, float v)
        {
            if (Turn != null)
            {
                Turn(h, v);
            }
        }

        protected void Attact()
        {
            if (AAA != null)
            {
                AAA();
            }
        }

        protected void CameraDistanceEvent(float d)
        {
            if (CameraDistance!= null)
            {
                CameraDistance(d);
            }
        }

        protected void CameraDragEvent(Vector3 v3)
        {
            if (CameraDrag != null)
            {
                CameraDrag(v3);
            }
        }

        protected void CameraRotateEvent(Vector3 v3)
        {
            if (CameraRotate != null)
            {
                CameraRotate(v3);
            }
        }
    }
}
