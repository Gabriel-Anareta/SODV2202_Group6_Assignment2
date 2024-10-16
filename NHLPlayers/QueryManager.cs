using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NHLPlayers
{
    public static class QueryManager
    {
        // method uses dynamic types - make sure checking is done extensively to avoid type errors
        // dynamic type used to keep code relatively simple without bloating it with multipe layers of type checks
        // note to self - dynamic types and reflections seem to love throwing warnings no matter what :(
        public static bool RunFilters(MatchCollection rawfilters, Object obj)
        {
            bool state = true;

            for (int i = 0; i < rawfilters.Count; i++)
            {
                Match filter = rawfilters[i];

                // get property
                string propString = ExpressionManager.GetProperty(filter.Value).Value;
                propString = PropManager.MapPlayerProp(propString);
                object propObj = PropManager.GetPropValue(obj, propString);
                dynamic propTemp = propObj.GetType().GetProperty("value").GetValue(propObj);
                dynamic prop;

                // check property on object
                if (propTemp is string)
                    if (propTemp == "invalid property")
                        continue;

                if (propTemp is CustomTime)
                    prop = (propTemp as CustomTime).AsSeconds();  // explicit cast to see method
                else
                    prop = propTemp;

                // get operation
                string op = ExpressionManager.GetOperation(filter.Value).Value.Trim();

                // do not allow < > operations of string values
                if (prop is string)
                    if (op.Contains('<') || op.Contains('>'))
                        continue;

                // get argument
                string argTemp = ExpressionManager.GetArgument(filter.Value).Value.Trim();
                dynamic arg;
                Type propType = obj.GetType().GetProperty(propString).PropertyType;

                // check expression on prop value for casting
                if (CanCast(argTemp, propType))
                    arg = CastTo(argTemp, propType);
                else
                    continue;

                // run expressions based on given operations
                switch (op)
                {
                    case "<":
                        state = state && (prop < arg);
                        break;
                    case ">":
                        state = state && (prop > arg);
                        break;
                    case "<=":
                    case "=<":
                        state = state && (prop <= arg);
                        break;
                    case ">=":
                    case "=>":
                        state = state && (prop >= arg);
                        break;
                    case "==":
                        state = state && (prop == arg);
                        break;
                }

                if (!state)
                    return state;
            }

            return state;
        }

        private static bool CanCast(string value, Type type)
        {
            if (type == typeof(string))
                return true;

            Match timeMatch = ExpressionManager.GetTime(value);
            if (type == typeof(CustomTime))
            {
                if (timeMatch.Value != "")
                    return true;
            }

            if (type == typeof(int))
            {
                bool intMatch = value.Contains('.');
                if (timeMatch.Value == "" && !intMatch)
                    return true;
            }

            if (type == typeof(double) || type == typeof(double?))
            {
                Match doubleMatch = ExpressionManager.GetDouble(value);
                if (timeMatch.Value == "" && doubleMatch.Value != "")
                    return true;
            }

            return false;
        }

        private static dynamic CastTo(string value, Type type)
        {
            if (type == typeof(string))
                return value;

            if (type == typeof(CustomTime))
                return (new CustomTime(value)).AsSeconds();

            if (type == typeof(int))
                return int.Parse(value);

            if (type == typeof(double))
                return double.Parse(value);

            return value;
        }
    }
}
