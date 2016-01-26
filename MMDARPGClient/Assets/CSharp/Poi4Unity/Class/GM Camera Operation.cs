using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poi;

namespace UnityEngine
{
    /// <summary>
    /// 游戏管理
    /// </summary>
    public partial class GM
    {
        public static FollowThirdCamera MainCamera;
        public static Operation Operation;


        public static FollowThirdCamera InitCamera()
        {
            GameObject go = null;
            if (Camera.main)
            {
                go = Camera.main.gameObject;
            }
            else
            {
                go = new GameObject("Camera");
                go.AddComponent<FlareLayer>();
                go.AddComponent<GUILayer>();
                go.AddComponent<AudioListener>();
            }

            MainCamera = go.AddComponent<FollowThirdCamera>();
            go.AddComponent<DontDestroyOnLoad>();
            if (!Operation)
            {
                InitOperation();
            }

            Operation.CameraDistance += MainCamera.mouseWheelEvent;
            Operation.CameraDrag += MainCamera.cameraTranslate;
            Operation.CameraRotate += MainCamera.cameraRotate;
            return MainCamera;
        }

        public static Operation InitOperation()
        {
            GameObject go = new GameObject("Operation");
            go.AddComponent<DontDestroyOnLoad>();

            Operation = go.AddComponent<PCOperation>();
            return Operation;
        }
    }
}
