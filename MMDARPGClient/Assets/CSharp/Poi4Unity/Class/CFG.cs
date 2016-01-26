namespace Poi
{
    /// <summary>
    /// 配置
    /// </summary>
    public class CFG
    {
        /// <summary>
        /// xml路径
        /// </summary>
        public static string PATH { get; set; }
        /// <summary>
        /// 游戏FPS
        /// </summary>
        public static int FPS { get; internal set; }
        /// <summary>
        /// UI预制体路径
        /// </summary>
        public static string UIPath { get; internal set; }
        /// <summary>
        /// 精灵路径
        /// </summary>
        public static string SpritePath { get; internal set; }
        /// <summary>
        /// 音频路径
        /// </summary>
        public static string AudioClipPath { get; internal set; }
        /// <summary>
        /// 预制体路径
        /// </summary>
        public static string PreparePath { get; internal set; }
        public static string WritePath { get; internal set; }
        public static Language Language { get; internal set; }
        /// <summary>
        /// 模型存放路径
        /// </summary>
        public static string ModelPath { get; internal set; }
        public static string DataPath { get; internal set; }
        public static string AnimatiorControllerPath { get; internal set; }


        #region WWWprefix
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        /// <summary>
        /// WWW加载资源的前缀
        /// </summary>
        public static readonly string WWWstreamingAssetsPathPrefix = "file:///";
#elif UNITY_ANDROID
        public static readonly string WWWstreamingAssetsPathPrefix ="";// "jar:file:///";
#endif
        #endregion

        public static readonly string WWWpersistentDataPathPrefix = "file:///";
    }
}
