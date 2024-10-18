using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace NHLPlayers
{
    public static class QueryManager
    {
        public static bool RunFilters(MatchCollection rawfilters, Object obj)
        {
            bool state = true;

            for (int i = 0; i < rawfilters.Count; i++)
            {
                string filter = rawfilters[i].Value;

                // get property
                string propName = GetPropName(filter);
                dynamic prop;
                if (CheckProp(propName, typeof(Player)))
                    prop = GetPropValue(propName, obj);
                else
                    continue;

                // get operation
                string op = ExpressionManager.GetOperation(filter).Value;

                // ignore < > operations of string or null values
                if (prop is string || prop is List<string> || prop == null)
                    if (op.Contains('<') || op.Contains('>'))
                        continue;

                // get argument
                string argString = ExpressionManager.GetArgument(filter).Value.Trim();
                dynamic arg;

                Type propType = prop.GetType();

                // check expression on prop value for casting
                if (argString.CanCast(propType))
                    arg = argString.CastTo(propType);
                else
                    continue;

                if (prop is CustomTime)
                    prop = (prop as CustomTime).AsSeconds();  // explicit cast to see method

                // run expression and update current state
                state = state && RunExpression(op, prop, arg);

                if (!state)
                    return state;
            }

            return state;
        }

        public static IEnumerable<Player> RunOrders(this IEnumerable<Player> data, MatchCollection orders)
        {
            if (orders.Count == 0)
                return data;

            var result = data;
            result = result.OrderData(orders[0].Value);

            for (int i = 1; i < orders.Count; i++)
                result = result.OrderData(orders[i].Value, false);

            return result;
        }


        // Helper functions

        private static string GetPropName(string filter)
        {
            string propName = ExpressionManager.GetProperty(filter).Value;
            propName = PropManager.MapPlayerProp(propName);

            return propName;
        }

        private static bool CheckProp(string propName, Type type)
        {
            PropertyInfo prop = type.GetProperty(propName);

            if (prop == null)
                return false;

            return true;
        }

        private static dynamic GetPropValue(string propName, Object obj)
        {
            Object propObj = PropManager.GetPropValue(obj, propName);
            dynamic prop = propObj.GetType().GetProperty("value").GetValue(propObj);

            return prop;
        }

        private static bool CanCast(this string value, Type type)
        {
            if (type == typeof(string) || type == typeof(List<string>))
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
                if (value == "null")
                    return true;

                Match doubleMatch = ExpressionManager.GetDouble(value);
                if (timeMatch.Value == "" && doubleMatch.Value != "")
                    return true;
            }

            return false;
        }

        private static dynamic CastTo(this string value, Type type)
        {
            if (type == typeof(CustomTime))
                return (new CustomTime(value)).AsSeconds();

            if (type == typeof(int))
                return int.Parse(value);

            if (type == typeof(double) || type == typeof(double?))
            {
                if (value == "null")
                    return (null as double?);
                else
                    return double.Parse(value);
            }

            return value;
        }

        private static bool RunExpression(string op, dynamic prop, dynamic arg)
        {
            switch (op)
            {
                case "<":
                    return prop < arg;
                case ">":
                    return prop > arg;
                case "<=":
                case "=<":
                    return prop <= arg;
                case ">=":
                case "=>":
                    return prop >= arg;
                case "==":
                    if (prop is List<string>)
                        return (prop as List<string>).Any(value => value == arg); // explicit cast to see method
                    else
                        return prop == arg;
            }

            return true;
        }

        private static IEnumerable<Player> OrderData(this IEnumerable<Player> data, string filter, bool isMainFilter = true)
        {
            // check property
            string propName = GetPropName(filter);
            if (!CheckProp(propName, typeof(Player)))
                return data;

            // get arg
            string arg = ExpressionManager.GetAscDes(filter).Value;

            if (arg == "")
                return data;

            if (arg == "asc" || arg == "ascending")
            {
                if (isMainFilter)
                    data = data.OrderBy(player => player.SelectOrder(propName));
                else
                    data = (data as IOrderedEnumerable<Player>).ThenBy(player => player.SelectOrder(propName));
            }
            else
            {
                if (isMainFilter)
                    data = data.OrderByDescending(player => player.SelectOrder(propName));
                else
                    data = (data as IOrderedEnumerable<Player>).ThenByDescending(player => player.SelectOrder(propName));
            }

            return data;
        }

        private static dynamic SelectOrder(this Player player, string propName)
        {
            var orderByValue = player.GetType().GetProperty(propName).GetValue(player);

            if (orderByValue is List<string>)
                return (orderByValue as List<string>).First();

            if (orderByValue is CustomTime)
                return (orderByValue as CustomTime).AsSeconds();

            return orderByValue;
        }
    }
}
