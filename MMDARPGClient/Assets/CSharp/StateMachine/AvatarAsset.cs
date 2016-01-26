using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Poi
{
    /// <summary>
    /// 骨骼资源
    /// </summary>
    public class AvatarAsset
    {
        Dictionary<AvatarTarget, Organ> partdic = new Dictionary<AvatarTarget, Organ>();

        public AvatarAsset()
        {
            foreach (AvatarTarget item in Enum.GetValues(typeof(AvatarTarget)))
            {
                Organ temp = new Organ() { Part = item };
                partdic[temp.Part] = temp;
            }
        }

        public Organ this[AvatarTarget part]
        {
            get
            {
                return partdic[part];
            }
        }
    }

    /// <summary>
    /// 器官部件
    /// </summary>
    public class Organ
    {
        public AvatarTarget Part { get; set; }

        public bool Locked { get; private set; }

        public iState User { get; private set; }

        public void Use(iState user, bool islock = false)
        {
            Locked = islock;
            if (User != null)
            {
                User.LostAsset(this);
            }
            
            User = user;
        }

        public void UseDone(iState user)
        {
            if (User == user)
            {
                Locked = false;
                User = null;
            }
        }
    }
}
