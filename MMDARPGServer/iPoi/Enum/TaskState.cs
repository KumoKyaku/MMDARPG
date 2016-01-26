using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 一个任务的进度
    /// </summary>
    [Obsolete("过时:我通过6个小时理清的我想要的模型。然后我发现它就是异步编程模型。"+
        ".NET Task完全可以实现我想要的，Unity中使用协同和AsyncOperation组合也能满足需求。"+
        "是的，我重复造轮子了，而且还不圆。2016年1月6日18:12:44", true)]
    public enum TaskState
    {
        /// <summary>
        /// 表示任务不存在（你不能让一个成员变量消失，null或者TaskStep.Null）
        /// </summary>
        Null,
        /// <summary>
        /// 开始
        /// </summary>
        Start,
        /// <summary>
        /// 进行中
        /// </summary>
        Doing,
        /// <summary>
        /// 失败结果
        /// </summary>
        ErrorEnd,
        /// <summary>
        /// 完成
        /// </summary>
        Finish,
        /// <summary>
        /// 访问的时间可能不是时候
        /// </summary>
        WrongTime,
        /// <summary>
        /// 使用完毕
        /// </summary>
        UseDone,
    }
}
