using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Poi
{
    /// <summary>
    /// 异步操作进度
    /// </summary>
    public class Task
    {
        private bool isDone = false;

        public float Progress
        {
            get
            {
                return progress;
            }

            set
            {
                progress = value;
            }
        }

        public bool IsDone
        {
            get
            {
                return isDone;
            }

            set
            {
                isDone = value;
            }
        }

        private float progress = 0f;
    }

    /// <summary>
    /// 异步操作进度,如果需要一个返回值，则放入Value中，isDone自动变为true。
    /// </summary>
    public class Task<T>
    {
        private bool isDone = false;

        public float Progress
        {
            get
            {
                return progress;
            }

            set
            {
                progress = value;
            }
        }

        public bool IsDone
        {
            get
            {
                return isDone;
            }
        }

        private float progress = 0f;
        T value;
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                isDone = true;
            }
        }
    }

    /// <summary>
    /// 增强型？
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TaskTest<T> :MonoBehaviour
    {
        private bool isDone = false;

        public float Progress
        {
            get
            {
                return progress;
            }

            set
            {
                progress = value;
            }
        }

        public bool IsDone
        {
            get
            {
                return isDone;
            }
        }

        private float progress = 0f;
        T value;
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                isDone = true;
            }
        }

        private TaskTest()
        {

        }

        public static TaskTest<Result> Get<K,Result>(Func<K, TaskTest<Result>, IEnumerator> func,K k)
        {
            TaskTest<Result> task = new TaskTest<Result>();
            task.StartCoroutine(func(k, task));
            return task;
        }
    }
}
