using System;
using System.Collections;
using System.Xml.Linq;
using UnityEngine;
using System.IO;
using System.Text;
using Poi;

namespace UnityEngine
{
    /// <summary>
    /// 游戏管理
    /// </summary>
    public partial class GM : MonoBehaviour
    {
        public static bool LockOperationCharator = false;

        public static readonly BackgroundThread BackgroundThread = new BackgroundThread();
        // Use this for initialization
        void Awake()
        {
            Instance = this;
            GameState = GameState.Loading;
        }

        public static GameState GameState { get; private set; }
        public static bool SplashDone { get; internal set; }
        public static GM Instance { get; private set; }

        void Start()
        {
            StartCoroutine(Load());
        }

        void FixedUpdate()
        {
            switch (GameState)
            {
                case GameState.UnDefine:
                    break;
                case GameState.Prepared:
                    break;
                case GameState.Changing:
                    break;
                case GameState.Loading:
                    break;
                case GameState.Running:
                    break;
                case GameState.Background:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Saving:
                    break;
                case GameState.Exit:
                    break;
                case GameState.LoadPlayer:
                    Update_initPlayer();
                    break;
                default:
                    break;
            }
        }



        /// <summary>
        /// 游戏推出
        /// </summary>
        /// <param name="v"></param>
        public static void Exit(int v)
        {
            switch (v)
            {
                case 0:
                    Application.Quit();
                    break;
                case 1:
                    SaveData();
                    Exit(0);
                    break;
                default:
                    Exit(1);
                    break;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public static void SaveData()
        {
            using (StreamWriter wr = new StreamWriter(
                Application.persistentDataPath + "/Config.xml", false, new UTF8Encoding(false)))
            {
                Util.SaveProperties<CFG>(null,"Config").Save(wr);
            }

            using (StreamWriter wr = new StreamWriter(
                Application.persistentDataPath + "/Data.xml", false, new UTF8Encoding(false)))
            {
                Util.SaveProperties<Data4Player>(null, "Data4Charator").Save(wr);
            }
        }

        #region 加载游戏

        private IEnumerator Load()
        {
            yield return StartCoroutine(LoadConfig());
            yield return StartCoroutine(LoadPlayerConfig());
            yield return StartCoroutine(LoadText(CFG.WritePath + "TranslatorText.xml", CFG.Language));
            yield return StartCoroutine(LoadPlayerData());

            UsingCFG();

            StartCoroutine(WaitSplashImage());
        }

        /// <summary>
        /// 加载游戏初始配置
        /// </summary>
        /// <returns></returns>
        static IEnumerator LoadConfig()
        {
            WWW www = new WWW(CFG.WWWstreamingAssetsPathPrefix + Application.streamingAssetsPath + "/Config.xml");
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                Util.AutoFullProperties<CFG>(XElement.Parse(www.text), null);
            }
            else
            {
                Debuger.Log(www.error);
            }

            //using (StreamWriter wr = new StreamWriter(
            //    Application.streamingAssetsPath + "/Config.xml", false, new UTF8Encoding(false)))
            //{
            //    Util.SaveProperties<CFG>(null, "Config").Save(wr);
            //}
        }

        /// <summary>
        /// 加载用户配置，对游戏内配置覆盖
        /// </summary>
        /// <returns></returns>
        static IEnumerator LoadPlayerConfig()
        {
            WWW www = new WWW(CFG.WWWpersistentDataPathPrefix + Application.persistentDataPath + "/Config.xml");
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                Util.AutoFullProperties<CFG>(XElement.Parse(www.text), null);
            }
            else
            {
                Debuger.Log(www.error);
            }
        }

        /// <summary>
        /// 加载文本
        /// </summary>
        /// <returns></returns>
        public static IEnumerator LoadText(string textname, Language tar = Language.Chinese)
        {
            WWW www = new WWW(CFG.WWWstreamingAssetsPathPrefix + Application.streamingAssetsPath + "/TranslatorText.xml");
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                Writing.Init(XElement.Parse(www.text), tar);
                //Text.AddItem(XElement.Parse(www.text), 100101, 100130).Save(Application.streamingAssetsPath + "/TranslatorText.xml");
                Writing.CurrentLanguage = tar;
            }
            else
            {
                Debuger.Log(www.error);
            }
        }

        /// <summary>
        /// 加载用户游戏数据
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadPlayerData()
        {
            WWW www = new WWW(CFG.WWWpersistentDataPathPrefix + Application.persistentDataPath + "/Data.xml");
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                Data4Player.Init(XElement.Parse(www.text));
            }
            else
            {
                Debuger.Log(www.error);
            }
        }

        static void UsingCFG()
        {
            Application.targetFrameRate = CFG.FPS;

            ///准备阶段完成
            GameState = GameState.Prepared;
        }

        /// <summary>
        /// 等待闪屏结束，开始游戏
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitSplashImage()
        {
            while (GameState == GameState.Loading)
            {
                yield return new WaitForSeconds(0.2f);
            }

            GameStart();
        }

        #endregion

        static void GameStart()
        {
            LoadLevel("Start", () =>
             {
                 Debuger.Log("准备完成，开始游戏");
                 InitOperation();
                 InitCamera();
                 GameState = GameState.LoadPlayer;
             });
        }

        /// <summary>
        /// 协同异步加载场景
        /// </summary>
        /// <param name="v"></param>
        /// <param name="loadDone"></param>
        public static void LoadLevel(string v, Action loadDone = null)
        {
            Instance.StartCoroutine(CoroutineLoadLevel(v, loadDone));
        }

        private static IEnumerator CoroutineLoadLevel(string v, Action loadDone = null)
        {
            GameState = GameState.Changing;
            var result = Application.LoadLevelAsync(v);
            Process.ShowProcess(result);
            
            while (!(result.allowSceneActivation || result.isDone))
            {
                yield return null;
            }
            ///等待加载完成执行回调
            Instance.Wait(new WaitForSeconds(0.1f),()=> 
            {
                GameState = GameState.Running;
                if (loadDone != null)
                {
                    loadDone();
                }
            });   
        }
    }
}
