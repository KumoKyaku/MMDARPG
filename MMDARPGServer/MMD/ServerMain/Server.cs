using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Poi
{
    /// <summary>
    /// 游戏服务器实体
    /// </summary>
    public class Server
    {
        private Server() { }
        /// <summary>
        /// 服务器唯一实例
        /// </summary>
        public static readonly Server Instance = new Server();

        private bool IsRunning = true;
        private ushort port;

        /// <summary>
        /// 循环主体
        /// </summary>
        public void Run()
        {
            Time_base time = new Time_base();
            while (IsRunning)
            {
                var dt = time.Delta().TotalMilliseconds;
                ///服务器的工作代码传入距上次执行的时间间隔毫秒数
                Work(dt);

                Sleep(time);
            }

        }

        private void Work(double dt)
        {
            ClientManager.Update(dt);
        }

        /// <summary>
        /// 睡眠
        /// </summary>
        /// <param name="time"></param>
        void Sleep(Time_base time)
        {
            if (time.DeltaUnMark().TotalMilliseconds > 1)
            {
                Thread.Sleep(0);
            }
            else
            {
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 初始化服务器
        /// </summary>
        /// <param name="args">传入参数</param>
        public void Init(string[] args)
        {
            InitPath();
            InitLogger();
            InitData();

            InitConfig();
            InitDB();

            InitProtocol();
            InitSocket();
        }

        /// <summary>
        /// 初始化Socket监听
        /// </summary>
        private void InitSocket()
        {
            LinkManager.Listen(port, (ushort listen_port) =>
            {
                Client client = new Client();
                client.Listen(listen_port);
            });
        }

        /// <summary>
        /// 初始传输协议的编码
        /// </summary>
        private void InitProtocol()
        {
            Protocol.Api.GenerateId();
        }

        private void InitDB()
        {

        }

        private void InitConfig()
        {

        }

        private void InitData()
        {

        }

        /// <summary>
        /// 初始化Logger
        /// </summary>
        private void InitLogger()
        {
            var logger = new ServerLogger(Path.ApplicationPath +  @"LogForSaeaTest\");
            logger.Init("Server", true, true);

            Log.SetGlobalLogger(logger);
        }

        /// <summary>
        /// 初始化根路径
        /// </summary>
        private void InitPath()
        {
            string curPath = Directory.GetCurrentDirectory().Replace("\\", "/");

            Path.SetApplicationPath(curPath+"/");

            string rootPath = "";
            string[] folders = curPath.Split('/');

            for (int i = 0; i < folders.Length - 1; ++i)
            {
                rootPath += folders[i] + "/";
            }

            Path.SetPath(rootPath);

        }

        /// <summary>
        /// 监控对服务器的指令
        /// </summary>
        public void ProcessInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo != null)
            {
                if (keyInfo.Key == ConsoleKey.F5)
                {
                    Console.WriteLine(" [ Script ] Reloading Success.");
                }

                if (keyInfo.Key == ConsoleKey.F6)
                {
                    Console.WriteLine("当前连接客户端数量:" );
                }

                if (keyInfo.Key == ConsoleKey.F7)
                {
                    Console.WriteLine("上次运行时间:" );
                }

                if (keyInfo.Key == ConsoleKey.F8)
                {
                    Console.WriteLine("DB线程上次执行所用的时间(毫秒)：  " );
                }
            }
        }

        public void Exit()
        {

        }
    }
}
