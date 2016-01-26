using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Engine.Foundation;

namespace Poi
{
    /// <summary>
    /// 客户端
    /// </summary>
    public partial class Client
    {
        /// <summary>
        /// 存放的数据
        /// </summary>
        Queue<KeyValuePair<uint, MemoryStream>> datagQueue = new Queue<KeyValuePair<uint, MemoryStream>>();
        /// <summary>
        /// 交换的数据
        /// </summary>
        Queue<KeyValuePair<uint, MemoryStream>> dataQueueInven = new Queue<KeyValuePair<uint, MemoryStream>>();
        /// <summary>
        /// 正在处理的数据
        /// </summary>
        Queue<KeyValuePair<uint, MemoryStream>> deal_dataQueue;
        private int OnRead(MemoryStream transferred)
        {
            int offset = 0;
            byte[] buffer = transferred.GetBuffer();

            lock (this)
            {
                while ((transferred.Length - offset) > sizeof(ushort))
                {
                    ushort size = BitConverter.ToUInt16(buffer, offset);

                    if (size > transferred.Length - offset)
                    {
                        break;
                    }

                    uint msg_id = BitConverter.ToUInt32(buffer, offset + sizeof(ushort));
                    MemoryStream msg = new MemoryStream(buffer, offset + SocketHeader.Size, size, true, true);

                    datagQueue.Enqueue(new KeyValuePair<UInt32, MemoryStream>(msg_id, msg));

                    offset += (size + SocketHeader.Size);
                }
            }

            transferred.Seek(offset, SeekOrigin.Begin);
            return 0;
        }

        /// <summary>
        /// 处理服务器传来的消息
        /// </summary>
        public void DealMessage()
        {
            lock (datagQueue)
            {
                ///已经接到的数据交给  处理中的数据
                deal_dataQueue = datagQueue;
                ///备用的库存数据交给存放数据 继续接收
                datagQueue = dataQueueInven;
            }

            ///处理客户端的请求数据
            while (deal_dataQueue.Count > 0)
            {
                var msg = deal_dataQueue.Dequeue();
                try
                {
                    OnResponse(msg.Key, msg.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            ///将出完的数据空队列 交给 备用队列
            dataQueueInven = deal_dataQueue;
        }

        /// <summary>
        /// 服务器向客户端发送数据
        /// </summary>
        /// <param name="msg"></param>
        public void Send(iMessageDate msg)
        {

        }

        internal void Listen(ushort listen_port)
        {
            throw new NotImplementedException();
        }
    }
}
