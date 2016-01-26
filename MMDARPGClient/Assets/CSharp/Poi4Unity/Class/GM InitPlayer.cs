using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poi;

namespace UnityEngine
{
    /// <summary>
    /// 游戏管理
    /// </summary>
    public partial class GM : MonoBehaviour
    {
        ///这里初始化玩家
        enum InitProcess
        {
            GetName,
            Waiting,
            InitModel,
            Finish,
        }

        InitProcess initPlayProcess = InitProcess.GetName;

        void Update_initPlayer()
        {
            switch (initPlayProcess)
            {
                case InitProcess.GetName:
                    ///先取得昵称和使用的角色
                    initPlayProcess = InitProcess.Waiting;
                    StartCoroutine(GetName());
                    break;
                case InitProcess.Waiting:
                    break;
                case InitProcess.InitModel:
                    ///在场景中实例化角色
                    ///这个方法中途可能会中断，例如中途加载DLC中的资源，或者下载
                    /// 所以，实例化一个角色可能是同步也可能是异步    
                    
                    ///继续等待
                    initPlayProcess = InitProcess.Waiting;
                    ///异步实例化模型
                    StartCoroutine(InitCharator());
                    break;
                case InitProcess.Finish:
                    ///加载玩家全部完成
                    GameState = GameState.Running;
                    break;
                default:
                    break;
            }
        }

        private IEnumerator InitCharator()
        {
            initPlayProcess = InitProcess.Waiting;
            Task async = CharatorManager.InstantiatePlayer();

            while (!async.IsDone)
            {
                yield return null;
            }
            initPlayProcess = InitProcess.Finish;
        }

        #region GetName

        private IEnumerator GetName()
        {
            while (!SplashDone)
            {
                ///等待闪屏结束
                yield return null;
            }
            yield return StartCoroutine(GetNiCheng());
            yield return StartCoroutine(ChooseCharator());
            ///名字已经拿到，初始化模型吧
            initPlayProcess = InitProcess.InitModel;

        }

        /// <summary>
        /// 从上次的用户数据或者根据输入来取得昵称
        /// </summary>
        /// <returns></returns>
        private IEnumerator GetNiCheng()
        {
            ///开始取得昵称
            if (string.IsNullOrEmpty(Data4Player.NiChengName))
            {
                ///如果用户数据没有上次的昵称：
                GetNiChengFromInput();
            }

            while (string.IsNullOrEmpty(Data4Player.NiChengName))
            {
                yield return null;
            }
            ///取得昵称完成
        }

        /// <summary>
        /// 使用一个InputUI获取玩家输入
        /// </summary>
        void GetNiChengFromInput()
        {
            aUIManager.GetUI<UI_Input>().Use(
                ///new 一个输入UI的上下文作为参数
                new InputContext()
                {
                    ///输入UI的默认值
                    DefaultString = "初音未来",
                    ///输入UI提交时的回调方法Action<string,UI_Input>
                    OnSubmit = (value, input) =>
                    {
                        if (string.IsNullOrEmpty(value.Trim()))
                        {
                            ///如果用户输入的昵称是空串：
                        }
                        else
                        {
                            ///得到有效的昵称，保存的用户数据中
                            Data4Player.NiChengName = value;
                            ///结束输入UI的使用
                            input.UseDone();
                        }
                    }
                }
                
                );
        }


        private IEnumerator ChooseCharator()
        {
            ///开始取得角色
            if (string.IsNullOrEmpty(Data4Player.CharatorName))
            {
                ChooseCharatorFromUI();
            }
            else
            {
                if (!CharatorManager.CheckCharatorName(Data4Player.CharatorName))
                {
                    ChooseCharatorFromUI();
                }
            }

            while (string.IsNullOrEmpty(Data4Player.CharatorName))
            {
                yield return null;
            }

            if (CharatorManager.CheckCharatorName(Data4Player.CharatorName))
            {
                ///取得角色完成
            }
            else
            {
                ///取得角色不合法 递归获取
                ChooseCharator();
            }
        }

        private void ChooseCharatorFromUI()
        {
            Data4Player.CharatorName = "Ethan";
        }

        #endregion
    }
}
