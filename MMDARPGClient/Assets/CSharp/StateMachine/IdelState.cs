using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Poi
{
    public class IdelState : aState
    {
        AvatarTarget[] asset = new AvatarTarget[] { AvatarTarget.LeftFoot };
        readonly int hashname = Animator.StringToHash("Idle");

        public IdelState(StateMachine stateMachine) : base(stateMachine) { }

        public override Loop Loop
        {
            get
            {
                return Loop.Loop;
            }
        }

        public override AvatarTarget[] MustAsset
        {
            get
            {
                return asset;
            }
        }

        public override int HashName
        {
            get
            {
                return hashname;
            }
        }

        public override void OnStart()
        {
            durationTime = Charator.Play(this);


            State = aStateState.OnUpdate;
        }

        public override void OnUpdate(float deltaTime)
        {
            durationTime -= deltaTime;
            if (durationTime <= 0)
            {
                if (Loop == Loop.Loop)
                {
                    durationTime = 0;
                }
                else
                {
                    ///此处没有下个状态
                }
            }


            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (h != 0 || v != 0)
            {
                machine.WakeUpState(Animator.StringToHash("Grounded"));
            }
        }

        public override void OnExit()
        {
            State = aStateState.Sleep;
        }
    }
}
