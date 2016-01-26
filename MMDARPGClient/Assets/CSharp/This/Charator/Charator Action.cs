using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Poi;
using UnityEditor.Animations;

/// <summary>
/// 角色
/// </summary>
public partial class Charator 
{
    Animator AnimatorController;


    /// <summary>
    /// 初始化动作系统
    /// </summary>
    public void InitAnimator()
    {
        AnimatorOverrideController aoc = 
            Loader.Load<AnimatorOverrideController>(CFG.AnimatiorControllerPath + Data.Name + "Controller");
        AnimatorController cor =
            Loader.Load<AnimatorController>(CFG.ModelPath + "BaseAnim/AnimationController");
        AnimatorController = gameObject.GetComponentInChildren<Animator>();
        AnimatorController.runtimeAnimatorController = cor;
    }

    
    public void Move(float h, float v)
    {
        ///计算位移
        /// 前后位移 50取决于fixUpdate的频率，保证基准速度为1m/s，与动画匹配。
        Vector3 moveForward = transform.forward.NoY().normalized / 50;
        ///如果后退速度为4分之一？
        float backScale = 0.25f;
        if (v < 0)
        {
            v = v * backScale;
        }

        moveForward = moveForward * v * Data.Speed;
        ///应用位移
        transform.localPosition += moveForward;

        ///水平位移?需要跨步动画
        /// ToDo
        AnimatorController.SetFloat("Speed", v * Data.Speed, 0.1f, Time.deltaTime);
    }

    /// <summary>
    /// 左右旋转，前后旋转（暂留）
    /// </summary>
    /// <param name="lr"></param>
    /// <param name="fb"></param>
    public void Turn(float lr,float fb)
    {
        ///计算旋转
        float yR = 2 * lr;
        ///取得当前旋转
        Vector3 ea = transform.localRotation.eulerAngles;
        yR = ea.y + yR;
        if (yR > 360)
        {
            yR = yR - 360;
        }

        if (yR < -360)
        {
            yR = yR + 360;
        }
        ///设置计算后的旋转
        transform.localRotation = Quaternion.Euler(0, yR, 0);
        AnimatorController.SetFloat("Turn", lr, 0.1f, Time.deltaTime);
    }

    public void Jump()
    {
        AnimatorController.Play("Jump");
        Debuger.Log("Jump");
    }

    public void Attack()
    {
        AnimatorController.Play("Attack");
        Debuger.Log("Attack");
    }

    public void Select()
    {

    }

    public void OnHit()
    {

    }
}

