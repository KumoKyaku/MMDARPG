using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    public interface iStateMachineUser
    {
        /// <summary>
        /// 当前角色所拥有的资源
        /// </summary>
        AvatarAsset AvatarAsset { get; }

        float Play(aState aState, float? time = null);
    }
}
