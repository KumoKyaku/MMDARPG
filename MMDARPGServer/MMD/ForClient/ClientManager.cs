using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 客户端管理者
    /// </summary>
    public static class ClientManager
    {
        static List<Client> clientList = new List<Client>();
        static List<Client> removeclientList = new List<Client>();

        /// <summary>
        /// 刷新方法
        /// </summary>
        /// <param name="dt"></param>
        static public void Update(double dt)
        {
            ///移除客户端
            lock (removeclientList)
            {
                foreach (var item in removeclientList)
                {
                    clientList.Remove(item);
                    item.Dispose();
                }
                removeclientList.Clear();
            }

            ///更新现存的客户端
            lock (clientList)
            {
                foreach (var item in clientList)
                {
                    if (item.IsUsed)
                    {
                        item.DealMessage();
                    }
                }
            }
        }

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="cl"></param>
        /// <returns></returns>
        static public bool AddClient(Client cl)
        {
            if (clientList.Contains(cl))
            {
                return false;
            }
            else
            {
                clientList.Add(cl);
                return true;
            }
        }

        /// <summary>
        /// 将一个客户端添加到移除列表
        /// </summary>
        /// <param name="cl"></param>
        static public void RemoveClient(Client cl)
        {
            removeclientList.Add(cl);
        }
    }
}
