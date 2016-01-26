using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 角色属性
    /// </summary>
    public interface iCharator:iLabel,iNiCheng
    {
        /// <summary>
        /// 实例号码
        /// </summary>
        int InstanceID { get; set; }
        /// <summary>
        /// 角色类型
        /// </summary>
        void Set(CharatorType rigType);
        /// <summary>
        /// 空间计算
        /// </summary>
        void Set(iTransform transform_Poi);
        /// <summary>
        /// 操作
        /// </summary>
        void Set(iOperation operation);
        /// <summary>
        /// 角色数值
        /// </summary>
        void Set(iData data);
    }
}
