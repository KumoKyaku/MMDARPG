using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 操作来源
    /// </summary>
    public enum OperationFrom
    {
        /// <summary>
        /// 服务器
        /// </summary>
        Sever = 0,
        /// <summary>
        /// 客户端
        /// </summary>
        Client =1,
        
        /// <summary>
        /// 电脑
        /// </summary>
        Pc = 2,
        /// <summary>
        /// 移动端
        /// </summary>
        Touch = 3,
    }
}
