using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poi
{
    public class ServerLogger : AbstractLogger
    {
        private bool doConsolePrint = false;
        private bool doFilePrint = false;

        private string logFilePath = "C:";
        private string logFileName = "";
        private int writeLength = 0;
        private readonly int fileSize = 8388608; // 每个log文件大小为8M
        private string prefix;
        StreamWriter tw;

        public void Init(string prefix, bool consolePrint, bool filePrint)
        {
            doConsolePrint = consolePrint;
            doFilePrint = filePrint;
            this.prefix = prefix;
            //We create a new log file every time we run the app.
            logFileName = GetSaveFileName(prefix, logFilePath);

            // create a writer and open the file
            tw = new StreamWriter(logFileName);
            tw.AutoFlush = true; // TODO 验证性能是否可以接受
        }

        public ServerLogger(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        private string GetSaveFileName(string prefix, string logFilePath)
        {
            try
            {
                if (Directory.Exists(logFilePath) == false)
                {
                    Directory.CreateDirectory(logFilePath);
                }
            }
            catch
            {
                Console.WriteLine("Could not create save directory for log. See Logger.cs.");
            }

            string assemblyFullName = Assembly.GetExecutingAssembly().FullName;
            Int32 index = assemblyFullName.IndexOf(',');
            string dt = "" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour
                + DateTime.Now.Minute + DateTime.Now.Second;

            //Save directory is created in ConfigFileHandler
            return logFilePath + prefix + "_" + dt + ".txt"; ;
        }

        private void CheckFileSize(int length)
        {
            writeLength += length;
            if (writeLength > fileSize)
            {
                tw.Close();
                logFileName = GetSaveFileName(prefix, logFilePath);

                // 创建新的log file
                tw = new StreamWriter(logFileName);
                tw.AutoFlush = true; // TODO 验证性能是否可以接受
                writeLength = 0;
            }
        }

        public override void Write(object obj)
        {
            string info = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") + " [INFO] " + obj;
            tw.WriteLine(info);
            CheckFileSize(info.Length);
            if (doConsolePrint == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(info);
            }
        }

        public override void WriteLine(object obj)
        {
            Write(obj);
        }

        public override void Warn(object obj)
        {
            string info = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") + " [WARN] " + obj;
            tw.WriteLine(info);
            CheckFileSize(info.Length);
            if (doConsolePrint == true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(info);
            }
        }

        public override void WarnLine(object obj)
        {
            Warn(obj);
        }

        public override void Error(object obj)
        {
            string info = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") + " [ERROR] " + obj;
            tw.WriteLine(info);
            CheckFileSize(info.Length);
            if (doConsolePrint == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(info);
            }
        }
        public override void ErrorLine(object obj)
        {
            Error(obj);
        }

        public override void Close()
        {
            WriteLine("This session was logged to " + logFileName);
            tw.Close();
            tw = null;
        }
    }
}
