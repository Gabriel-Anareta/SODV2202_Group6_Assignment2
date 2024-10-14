using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NHLPlayers
{
    public static class PropManager
    {
        public static object GetPropValue(Object obj, string prop)
        {
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();

            if (type.GetProperty(prop) == null)
                return new { value = "invalid" };

            if (!props.Contains(type.GetProperty(prop)))
                return new { value = "invalid" }; ;

            return new { value = type.GetProperty(prop).GetValue(obj) };
        }
    }
}
