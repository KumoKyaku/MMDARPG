using System;
using System.Xml.Linq;

namespace UnityEngine
{
    public class Data4Player
    {
        public static string CharatorName { get; internal set; }
        public static string NiChengName { get; internal set; }

        internal static void Init(XElement xElement)
        {
            Poi.Util.AutoFullProperties<Data4Player>(xElement, null);
        }
    }
}
