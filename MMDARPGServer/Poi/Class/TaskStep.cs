using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 任务进度指示，一个任务总是从Null开始的，this[string name]== TaskState.Null之后，一个任务的标记就会开始存在
    /// 直到TaskState.UseDone。
    /// </summary>
    [Obsolete("过时:我通过6个小时理清的我想要的模型。然后我发现它就是异步编程模型。" +
        ".NET Task完全可以实现我想要的，Unity中使用协同和AsyncOperation组合也能满足需求" +
        "是的，我重复造轮子了，而且还不圆。2016年1月6日18:12:44", true)]
    public class TaskStep
    {
        Dictionary<string, TaskState> stringdic = new Dictionary<string, TaskState>();
        Dictionary<int, TaskState> intdic = new Dictionary<int, TaskState>();

        /// <summary>
        /// 通过一个字符串来表示一个任务
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TaskState this[string name]
        {
            get
            {
                if (!stringdic.ContainsKey(name))
                {
                    stringdic[name] = TaskState.Null;
                }
                return stringdic[name];
            }
            set
            {
                if (value == TaskState.UseDone)
                {
                    stringdic.Remove(name);
                    return;
                }
                stringdic[name] = value;
            }
        }

        /// <summary>
        /// 通过一个数字表示一个任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TaskState this[int id]
        {
            get
            {
                if (!intdic.ContainsKey(id))
                {
                    intdic[id] = TaskState.Null;
                }
                return intdic[id];
            }
            set
            {
                if (value == TaskState.UseDone)
                {
                    intdic.Remove(id);
                    return;
                }
                intdic[id] = value;
            }
        }
    }
    
}
