
using System;
using System.Xml.Linq;

namespace Poi
{
    /// <summary>
    /// 工具
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 自动填充属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_cfg">配置xml</param>
        /// <param name="_instance">静态属性为null</param>
        public static void AutoFullProperties<T>(XElement _cfg, T _instance)
        {
            var collection = typeof(T).GetProperties();
            foreach (var item in collection)
            {
                XAttribute _temp = _cfg.Attribute(item.Name);
                
                try
                {
                    if (item.CanWrite)
                    {
                        if (_temp == null)
                        {
                            continue;
                        }
                        else
                        {
                            item.SetValue(_instance, Convert.ChangeType(_temp.Value, item.PropertyType), null);
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }   
        }

        /// <summary>
        /// 把属性转成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_instance"></param>
        /// <param name="name">XElement的名字</param>
        /// <param name="addTypeNameAttribute"></param>
        /// <returns></returns>
        public static XElement SaveProperties<T>(T _instance,string name,bool addTypeNameAttribute = false)
        {
            string _n = name;
            if (string.IsNullOrEmpty(_n))
            {
                _n = typeof(T).ToString().Split(new string[] { "."},StringSplitOptions.RemoveEmptyEntries).Last();
            }

            XElement _root = new XElement(_n);
            if (addTypeNameAttribute)
            {
                _root.Add(new XAttribute("TypeName", typeof(T).ToString()));
            }

            var collection = typeof(T).GetProperties();
            foreach (var item in collection)
            {
                var ob = item.GetValue(_instance, null);
                XAttribute _temp;
                if (ob == null)
                {
                    _temp = new XAttribute(item.Name, "");
                }
                else
                {
                    _temp = new XAttribute(item.Name, ob);
                }
                _root.Add(_temp);
            }

            return _root;
        }
    }
}
