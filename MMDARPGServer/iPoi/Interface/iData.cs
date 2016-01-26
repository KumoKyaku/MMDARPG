using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 能力数值
    /// </summary>
    public interface iData
    {
        /// <summary>
        /// 根据参数取值
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        V Get<K, V>(K key);
    }
}
