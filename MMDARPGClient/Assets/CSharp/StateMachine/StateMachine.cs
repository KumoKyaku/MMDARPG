using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    public class StateMachine
    {
        public iStateMachineUser Charator { get; private set; }
        AvatarAsset AvatarAsset { get { return Charator.AvatarAsset; } }

        /// <summary>
        ///当前含有的状态
        /// </summary>
        Dictionary<int, aState> statedic = new Dictionary<int, aState>();

        public bool IsRunning { get; private set; }        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="user"></param>
        public StateMachine(iStateMachineUser user)
        {
            Charator = user;
        }

        public void AddState(aState state)
        {
            statedic[state.HashName] = state;
        }

        public void Entry()
        {
            IsRunning = true;
        }

        /// <summary>
        /// 每次状态机更新按照这个顺序执行
        /// </summary>
        /// <param name="deltatime"></param>
        public void Update(float deltatime)
        {
            if (!IsRunning) return;

            OnApplyState();

            OnExit();

            OnStart();

            OnUpdateStates(deltatime);
        }      

        /// <summary>
        /// 检查应用资源
        /// </summary>
        private void OnApplyState()
        {
            foreach (var item in statedic.Values)
            {
                if (item.State == aStateState.WakeUp)
                {
                    item.AssetsChange();
                }
            }
        }

        private void OnExit()
        {
            foreach (var item in statedic.Values)
            {
                if (item.State == aStateState.WaitExit)
                {
                    item.OnExit();
                }
            }
        }

        private void OnStart()
        {
            foreach (var item in statedic.Values)
            {
                if (item.State == aStateState.WaitStart)
                {
                    item.OnStart();
                }
            }
        }


        public void OnUpdateStates(float delta)
        {
            foreach (var item in statedic.Values)
            {
                if (item.State == aStateState.OnUpdate)
                {
                    item.OnUpdate(delta);
                }
            }
        }

        /// <summary>
        /// 计算能否拿到资源
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool CheckAsset(aState state)
        {     
            foreach (var item in state.MustAsset)
            {
                if (AvatarAsset[item].Locked && AvatarAsset[item].User != state)
                {
                    return false;
                }
            }

            return true;
        }

        public bool WakeUpState(int HashName,bool IsForce = false)
        {
            if (!statedic.ContainsKey(HashName)) return false;

            if (!IsForce)
            {
                if (!CheckAsset(statedic[HashName])) return false;
            }

            statedic[HashName].State = aStateState.WakeUp;
            return true;
        }
    }
}
