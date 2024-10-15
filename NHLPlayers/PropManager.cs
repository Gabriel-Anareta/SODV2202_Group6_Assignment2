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
            
            if (type.GetProperty(prop) == null)
                return new { value = "invalid property" };

            return new { value = type.GetProperty(prop).GetValue(obj) };
        }

        public static string MapPlayerProp(string prop)
        {
            switch (prop)
            {
                case "P":
                    return "PTS";
                case "+/-":
                    return "Plus_Minus";
                case "P/GP":
                    return "P_GP";
                case "S":
                    return "SOG";
                case "S%":
                    return "S_Perc";
                case "TOI/GP":
                    return "TOI_GP";
                case "Shifts/GP":
                    return "Shifts_GP";
                case "FOW%":
                    return "FOW_Perc";
            }

            return prop;
        }
    }
}
