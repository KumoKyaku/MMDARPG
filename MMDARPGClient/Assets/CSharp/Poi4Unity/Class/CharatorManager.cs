using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Linq;
using System.Collections;

namespace Poi
{
    public class CharatorManager:MonoBehaviour
    {
        public static CharatorManager Instance;
        static Dictionary<string, Data4Charator> datadic = new Dictionary<string, Data4Charator>();
        public static bool IsInit;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            StartCoroutine(InitData());
        }

        private IEnumerator InitData()
        {
            ///等待一秒，以免卡死
            yield return new WaitForSeconds(1);

                ///加载本地角色
            TextAsset ch = Loader.Load<TextAsset>(CFG.DataPath + "Charator");
            XElement xe = XElement.Parse(ch.text);

            var collection = xe.Elements("char");

            lock (datadic)
            {
                foreach (var item in collection)
                {
                    Data4Charator da = new Data4Charator(item);
                    datadic[da.Name] = da;
                }

                IsInit = true;
            }            
          
        }

        internal static bool CheckCharatorName(string charatorName)
        {
            return datadic.ContainsKey(charatorName);
        }


        public static Player Player { get; private set; }
        /// <summary>
        /// 初始化玩家
        /// </summary>
        public static Task InstantiatePlayer()
        {
            var charator = GetCharatorTemplate();
            Player = charator.AddComponent<Player>();

            return InitCharatorModel(Player, Data4Player.CharatorName);
        }


        /// <summary>
        /// 初始化角色
        /// </summary>
        public static Task InstantiateCharator(string name)
        {
            var charator = GetCharatorTemplate();
            Charator ch = charator.AddComponent<Charator>();

            return InitCharatorModel(ch,name);
        }


        /// <summary>
        /// 加载模型
        /// </summary>
        /// <param name="charator"></param>
        /// <param name="charatorName"></param>
        private static Task InitCharatorModel(Charator charator, string charatorName)
        {
            Task async = new Task();
            ///第一种方法
            ///开启一个协同分离加载和回调
            Instance.StartCoroutine(TempC(
                () =>
                {
                    Task<GameObject> task = new Task<GameObject>();
                    Instance.StartCoroutine(LoadModel(charatorName, task));
                    return task;
                },

                (model) =>
                {
                    ///将模型挂在model节点下
                    model.transform.SetParent(charator.transform);
                    ///从实例化数据
                    Data4Charator d4c = GetData(charatorName);
                    ///添加数据
                    charator.Set(d4c);
                    async.IsDone = true;
                }
                ));

            ///第二种方法
            

            return async;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">加载函数是延时的，完成是会把GameObject放在Task的Value中</param>
        /// <param name="callback">处理加载道的GameObject</param>
        /// <returns></returns>
        static IEnumerator TempC(Func<Task<GameObject>> a,Action<GameObject> callback)
        {
            var res = a();
            while (!res.IsDone)
            {
                ///阻塞
                yield return null;
            }

            if (callback != null)
            {
                callback(res.Value);
            }
        }

        /// <summary>
        /// 根据角色名字取得配置数据
        /// </summary>
        /// <param name="charatorName"></param>
        /// <returns></returns>
        private static Data4Charator GetData(string charatorName)
        {
            Data4Charator data;
            datadic.TryGetValue(charatorName, out data);
            return data;
        }

        static GameObject GetCharatorTemplate()
        {
            return Loader.Instantiate(CFG.ModelPath + "Charator");
        }

        /// <summary>
        /// DLC模型在这里扩展
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="async"></param>
        /// <returns></returns>
        private static IEnumerator LoadModel(string modelName, Task<GameObject> async)
        {
            bool local = true;
            if (local)
            {
                async.Value = Loader.Instantiate(CFG.ModelPath + modelName);
                yield return null;
            }
            else
            {
                WWW www = new WWW("");
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    var b = www.assetBundle;
                    async.Value = Instantiate(b.LoadAsset(modelName)) as GameObject;
                    yield return null;
                }
            }
        }
    }
}
