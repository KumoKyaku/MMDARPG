using Poi;

namespace UnityEngine
{
    /// <summary>
    /// 将文本编号重新翻译成文字
    /// </summary>
    [RequireComponent(typeof(UI.Text))]
    public class ReText : MonoBehaviour
    {
        private UI.Text text;
        [SerializeField]
        private int textNum;
        private Language language;
        public int TextNum
        {
            get
            {
                return textNum;
            }
            set
            {
                textNum = value;
            }
        }

        public Language Language
        {
            get
            {
                return language;
            }
        }

        // Use this for initialization
        void Start()
        { 
            text = GetComponent<UI.Text>();
            ReSetText();
        }

        public void ReSetText()
        {
            text.text = Writing.Get(TextNum);
            language = Writing.CurrentLanguage;
        }
    }
}