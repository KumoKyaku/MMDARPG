using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace Poi
{
    public class Data4Charator : iData
    {
       
        public int ID { get; internal set; }
        public string Name { get; internal set; }
        public float Height { get; internal set; }
        public float Speed { get; internal set; }

        public V Get<K, V>(K key)
        {
            return default(V);
        }

        public Data4Charator(XElement data)
        {
            Util.AutoFullProperties(data, this);
        }
    }
}
