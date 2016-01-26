using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 操作
    /// </summary>
    public interface iOperation
    {
        /// <summary>
        /// 操作来源
        /// </summary>
        OperationFrom OperationFrom { get; }

    }
}
