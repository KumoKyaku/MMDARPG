using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine
{
    /// <summary>
    /// 上下文
    /// </summary>
    public abstract class aContext 
    {
        public virtual object GetParameter(int _index)
        {
            if (parameters.Count > _index)
            {
                return parameters[_index];
            }
            return null;
        }

        protected IList<object> parameters = new List<object>();

        public static implicit operator bool(aContext acon)
        {
            return acon != null;
        }
    }


    public abstract class Context<T>
    {
        
    }
}
