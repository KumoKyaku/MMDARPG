using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Poi
{
    public enum aStateState
    {
        Sleep = 0,
        WakeUp,
        WaitStart,
        OnUpdate,
        WaitExit,
        /// <summary>
        /// 应该被过滤掉的状态
        /// </summary>
        Filtered,

    }

    public enum Loop
    {
        /// <summary>
        /// 单次
        /// </summary>
        Once,
        /// <summary>
        /// 循环
        /// </summary>
        Loop,  
    }

    public enum CanInterrupt
    {
        No = 0,
        Yes = 1,
    }

    public abstract class aState :iState
    {
        protected StateMachine machine;
        protected iStateMachineUser Charator { get { return machine.Charator; } }

        /// <summary>
        /// 状态所必须的资源
        /// </summary>
        public abstract AvatarTarget[] MustAsset { get; }
        public abstract int HashName { get; }

        /// <summary>
        /// 指示每个状态的当前状态
        /// </summary>
        public aStateState State { get; set; }
        public virtual Loop Loop { get { return Loop.Once; } }
        public float durationTime = 0f;

        public CanInterrupt CanInterrupt = CanInterrupt.Yes;

        public aState(StateMachine machine)
        {
            this.machine = machine;
        }


        public abstract void OnStart();

        public abstract void OnUpdate(float deltaTime);

        public abstract void OnExit();

        /// <summary>
        /// 应用资源变更
        /// </summary>
        public void AssetsChange()
        {
            foreach (var item in MustAsset)
            {
                Charator.AvatarAsset[item].Use(this,CanInterrupt == CanInterrupt.No);
            }

            State = aStateState.WaitStart;
        }

        /// <summary>
        /// 资源被抢走
        /// </summary>
        /// <param name="organ"></param>
        public void LostAsset(Organ organ)
        {
            if (MustAsset.Contains(organ.Part))
            {
                if (State == aStateState.WaitStart)
                {
                    State = aStateState.Sleep;
                }

                if (State == aStateState.OnUpdate)
                {
                    State = aStateState.WaitExit;
                }
            }
        }
    }
}
