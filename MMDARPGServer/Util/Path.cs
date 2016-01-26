using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poi
{
    /// <summary>
    /// 路径
    /// </summary>
    public class Path
    {
        static private string applicationPath = "";
        static private string rootPath = "";
        static private string dataPath = "";
        static private string clientDataPath = "";
        static private string sharedDataPath = "";
        static private string serverDataPath = "";

        public static string ApplicationPath
        {
            get
            {
                return applicationPath;
            }
        }

        static public void SetApplicationPath(string app)
        {
            applicationPath = app;
        }

        static public void SetPath(string rootPath)
        {
            Path.rootPath = rootPath;
            dataPath = rootPath + "Data/";
            clientDataPath = rootPath + "Client/";
            sharedDataPath = dataPath + "Shared/";
            serverDataPath = dataPath;
        }

        static public string FullPathFromRoot(string subDir)
        {
            return rootPath + subDir;
        }
        static public string FullPathFromClientData(string subDir)
        {
            return rootPath + subDir;
        }
        static public string FullPathFromSharedData(string subDir)
        {
            return rootPath + subDir;
        }
        static public string FullPathFromServerData(string subDir)
        {
            return rootPath + subDir;
        }
    }
}
