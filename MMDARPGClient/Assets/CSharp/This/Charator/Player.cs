using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Poi
{
    public partial class  Player:Charator
    {
        public FollowThirdCamera MainCamera;


        public override bool IsPlayer
        {
            get
            {
                return true;
            }
        }

        protected override void InitData4Player()
        {
            NiCheng = Data4Player.NiChengName;
        }

        public override void InitCarema()
        {
            if (GM.MainCamera)
            {
                MainCamera = GM.MainCamera;
            }
            else
            {
                MainCamera = GM.InitCamera();
            }

            MainCamera.focusfollow = modelCenter;
        }

        public override void InitOperation()
        {
            if (GM.Operation)
            {
                Operation = GM.Operation;
            }
            else
            {
                Operation = GM.InitOperation();
            }

            Operation.SetTarget(this);
        }
    }
}
