using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poi;
using UnityEngine;

/// <summary>
/// 角色
/// </summary>
public partial class Charator:iStateMachineUser
{
    private StateMachine StateMachine;
    private readonly AvatarAsset asset = new AvatarAsset();

    public AvatarAsset AvatarAsset
    {
        get
        {
            return asset;
        }
    }

    private void InitState()
    {
        StateMachine = new StateMachine(this);

        StateMachine.AddState(new IdelState(StateMachine) { State = aStateState.WakeUp });
        StateMachine.AddState(new MoveState(StateMachine));

        StateMachine.Entry();
    }

    public void LateUpdate()
    {
         StateMachine.Update(Time.deltaTime);
    }

    /// <summary>
    /// 分层播放，如果那一层的骨骼使用者是当前状态，则当前状态能播放状态，否则忽略
    /// </summary>
    /// <param name="aState"></param>
    /// <param name="time">时间的调整</param>
    /// <returns></returns>
    public float Play(aState aState, float? time = null)
    {
        ///获取将要播放状态的时间
        /// 根据时间进行调整4个参数
        float normalizedTime = time ?? 0;


        AnimatorController.CrossFade(aState.HashName, 0.2f, 0, normalizedTime);



        return normalizedTime;
    }
}

