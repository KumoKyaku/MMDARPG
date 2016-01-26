using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Poi
{
    /// <summary>
    /// 后台线程
    /// </summary>
    public class BackgroundThread
    {
        Thread thread;
        public int sleepWithNoWork = 100;

        public BackgroundThread() : this(100) { }
        public BackgroundThread(int sleepWithNoWork)
        {
            if (sleepWithNoWork >= 0)
            {
                this.sleepWithNoWork = sleepWithNoWork;
            }

            thread = new Thread(Run);
            thread.Start();
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    DealLoad();
                }
            }
            catch (Exception e)
            {
#if UNITY_EDITOR || Debug
                Debuger.Log(e);
#endif
                Run();
            }
        }

        public void Start()
        {
            if (thread.ThreadState != ThreadState.Running)
            {
                thread.Start();
            }
        }

        public void Stop()
        {
            if (thread.ThreadState != ThreadState.Stopped)
            {
                Thread.Sleep(0);
            }
        }

        Queue<iDo> qtask = new Queue<iDo>();
        void DealLoad()
        {
            if (qtask.Count > 0)
            {
                qtask.Dequeue().Do();
            }
            else
            {
                Thread.Sleep(sleepWithNoWork);
            }
        }

        /// <summary>
        /// 添加一个任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        public void Add<T>(TaskBackground<T> task)
        {
            qtask.Enqueue(task);
            thread.Interrupt();
        }

        /// <summary>
        /// 添加一个任务
        /// </summary>
        /// <param name="task"></param>
        public void Add(TaskBackground task)
        {
            qtask.Enqueue(task);
            thread.Interrupt();
        }
    }

    /// <summary>
    /// 后台线程任务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TaskBackground<T> : iDo
    {
        public void Do()
        {
            if (Task != null)
            {
                if (Callback != null)
                {
                    Callback(Task());
                }
                else
                {
                    Task();
                }
            }
        }

        public Func<T> Task { get; set; }
        public Action<T> Callback { get; set; }
    }

    /// <summary>
    /// 后台线程任务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TaskBackground: iDo
    {
        public void Do()
        {
            if (Task != null)
            {
                if (Callback != null)
                {
                    Task();
                    Callback();
                }
                else
                {
                    Task();
                }
            }
        }

        public Action Task { get; set; }
        public Action Callback { get; set; }
    }
}
