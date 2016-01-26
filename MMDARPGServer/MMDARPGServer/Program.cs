using System;
using System.Threading;
using Poi;

namespace MMDARPGServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = Server.Instance;
            api.Init(args);

            var thread = new Thread(api.Run);
            thread.Start();

            Console.WriteLine("Zone Server OnReady..");

            while (thread.IsAlive)
            {
                api.ProcessInput();

                Thread.Sleep(1000);
            }

            api.Exit();
        }
    }
}
