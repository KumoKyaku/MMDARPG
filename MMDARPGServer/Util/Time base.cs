using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 时间轴
    /// </summary>
    public class Time_base
    {
        /// <summary>
        /// 记录的时间点
        /// </summary>
        private DateTime mark;

        /// <summary>
        /// 构造方法，同事初始化当前时间
        /// </summary>
        public Time_base()
        {
            mark = DateTime.Now;
        }

        /// <summary>
        /// 将当前时间记录为记录点
        /// </summary>
        public void Mark()
        {
            mark = DateTime.Now;
        }

        /// <summary>
        /// 距记录点的时间间隔，并更新记录点
        /// </summary>
        /// <returns></returns>
        public TimeSpan Delta()
        {
            var now = DateTime.Now;
            var delta = now - mark;
            mark = now;

            return delta;
        }

        /// <summary>
        /// 距记录点的时间间隔，不更新记录点
        /// </summary>
        /// <returns></returns>
        public TimeSpan DeltaUnMark()
        {
            return DateTime.Now - mark;
        }

        /// <summary>
        /// 检测是否需要刷新
        /// </summary>
        /// <param name="refresh">刷新的整点</param>
        /// <param name="lastRefresh">上次刷新时间</param>
        /// <returns></returns>
        static public bool CheckRefresh(int[] refresh, DateTime lastRefresh)
        {
            if (lastRefresh.Date < DateTime.Today)
            {
                return true;
            }
            else
            {
                foreach (var item in refresh)
                {
                    if (lastRefresh.Hour < item && DateTime.Now.Hour > item)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
