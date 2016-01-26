using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 连接管理
    /// </summary>
    public class LinkManager
    {
        public static bool IsAvailable { get;set; }

        static public bool Listen(ushort port, Action<ushort> heartbeat)
        {
            return Listen(port, 128, heartbeat);
        }

        static public bool Listen(ushort port, ushort backlog, Action<ushort> heartbeat)
        {
            if (IsAvailable == false) return false;

            //heartbeats.Add(port, heartbeat);

            for (int i = 0; i < backlog; ++i)
            {
                heartbeat(port);
            }
            return true;

        }
    }
}
