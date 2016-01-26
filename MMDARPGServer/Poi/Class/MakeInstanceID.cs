using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    ///  实例ID，线程安全
    /// </summary>
    public static class MakeInstanceID
    {
        /// <summary>
        /// 取得一个ID
        /// </summary>
        static public int ID
        {
            get
            {
                lock (idlock)
                {
                    return id++;
                }
            }
        }

        static int id = 0;
        static string idlock = "";

        /// <summary>
        /// 归零
        /// </summary>
        static public void Zero()
        {
            lock (idlock)
            {
                id = 0;
            }
        }
    }
}