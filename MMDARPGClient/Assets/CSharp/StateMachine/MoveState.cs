using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Poi
{
    public class MoveState : aState
    {
        AvatarTarget[] asset = new AvatarTarget[] {  AvatarTarget.LeftFoot };
        public MoveState(StateMachine stateMachine) : base(stateMachine) { }
        readonly int hashname = Animator.StringToHash("Grounded");


        public override int HashName
        {
            get
            {
                return hashname;
            }
        }

        public override AvatarTarget[] MustAsset
        {
            get
            {
                return asset;
            }
        }

        public override void OnStart()
        {
            durationTime = Charator.Play(this);


            State = aStateState.OnUpdate;
        }

        float time = 5f;
        public override void OnUpdate(float deltaTime)
        {
            if (time < 0)
            {
                time = 5;
                machine.WakeUpState(Animator.StringToHash("Idle"));
            }
            else
            {
                time -= deltaTime;
            }
        }

        public override void OnExit()
        {
            Debuger.Log("MoveExit");
            State = aStateState.Sleep;
        }
    }
}
