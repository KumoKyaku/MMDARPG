using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 真空类型，Null
    /// </summary>
    [TesT,Obsolete("并没有作用，并不能应用于泛型",true)]
    public sealed class Void
    {
        /// <summary>
        /// Void 和null返回true，Void和Void返回true,其他返回false。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            try
            {
                if (obj.GetType() == typeof(Void))
                {
                    return true;
                }
            }
            catch (NotImplementedException)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 永远返回0x0。
        /// </summary>
        /// <returns>0x0</returns>
        public override int GetHashCode()
        {
            return 0x0;
        }

        public static bool operator ==(Void v, Void obj)
        {
            return true;
        }

        public static bool operator != (Void v, Void obj)
        {
            return false;
        }
    }
}
