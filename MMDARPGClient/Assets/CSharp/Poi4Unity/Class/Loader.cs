using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Poi;

namespace UnityEngine
{
    /// <summary>
    /// 加载类，实现Resources和AssetBundle加载
    /// </summary>
    public static class Loader
    {
        static readonly BackgroundThread loadThread = new BackgroundThread();

        static public GameObject Instantiate(string path, Transform parent = null, bool isResetTransform = false)
        {
            Object resource = Resources.Load(path);

            if (resource != null)
            {
                GameObject obj = Object.Instantiate(resource) as GameObject;
                if (parent != null && obj != null)
                {
                    obj.transform.SetParent(parent);

                    if (isResetTransform)
                    {
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.localRotation = Quaternion.identity;
                        obj.transform.localScale = Vector3.one;
                    }
                }

                return obj;
            }

            return null;
        }

        public static Object Load(string path)
        {
            return Resources.Load(path);
        }

        public static Object Load(string path, Type systemTypeInstance)
        {
            return Resources.Load(path, systemTypeInstance);
        }

        public static T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public static void UnloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
        }



        static AssetBundleManifest assetBundleManifest;

        /// <summary>
        /// 预热
        /// <para>建议游戏开始的时候调用一次</para>
        /// </summary>
        /// <returns></returns>
        public static IEnumerator Preheat()
        {
            yield return null;
        }

        /// <summary>
        /// 加载主依赖关系，会常驻在内存中
        /// </summary>
        /// <returns></returns>
        private static IEnumerator LoadAssetBundleManifest(string _assetBundlePath, int _version = 0)
        {
            WWW www = WWW.LoadFromCacheOrDownload(_assetBundlePath, _version);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                AssetBundle mainb = www.assetBundle;
                var manifest = mainb.LoadAsset("AssetBundleManifest") as AssetBundleManifest;

#if UNITY_EDITOR || Development
                if (manifest == null)
                {
                    Debuger.LogError("AssetBundleManifest == null");
                }
#endif
                assetBundleManifest = manifest ?? assetBundleManifest;
                mainb.Unload(false);
            }
            else
            {
#if UNITY_EDITOR || Development
                Debuger.LogError(www.error);
#endif
            }
        }

        /// <summary>
        /// 加载资源包
        /// </summary>
        /// <param name="dirctory"></param>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public static IEnumerator LoadAssetBundle(string dirctory, string assetBundleName)
        {
            ///如果assetBundleManifest为空，结束加载。
            if (assetBundleManifest == null)
            {
                yield break;
            }

            string[] dps = assetBundleManifest.GetAllDependencies(assetBundleName);
            AssetBundle[] temp = new AssetBundle[dps.Length];
            for (int i = 0; i < dps.Length; i++)
            {
                WWW www = WWW.LoadFromCacheOrDownload(dps[i], assetBundleManifest.GetAssetBundleHash(dps[i]));
                yield return www;
                temp[i] = www.assetBundle;
            }

            WWW tar = WWW.LoadFromCacheOrDownload(dirctory + assetBundleName, assetBundleManifest.GetAssetBundleHash(assetBundleName));
            yield return tar;
            AssetBundle t = tar.assetBundle;
        }
        
    }
}
