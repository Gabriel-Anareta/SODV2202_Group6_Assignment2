using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace NHLPlayers
{
    public static class QueryManager
    {
        public static bool RunFilters(this Player player, MatchCollection rawfilters)
        {
            for (int i = 0; i < rawfilters.Count; i++)
            {
                // set current filter
                string filter = rawfilters[i].Value;

                // run expression
                bool expressionResult = RunExpression(filter, player);

                // check state
                if (!expressionResult)
                    return false;
            }

            return true;
        }

        public static IEnumerable<Player> RunOrders(this IEnumerable<Player> data, MatchCollection orders)
        {
            if (orders.Count == 0)
                return data;

            var result = data;

            // Run main order
            result = result.OrderData(orders[0].Value);

            // Run succeding orders
            for (int i = 1; i < orders.Count; i++)
                result = result.OrderData(orders[i].Value, false);

            return result;
        }


        // Helper functions

        private static bool RunExpression(string filter, Player player)
        {
            // get property
            string propName = GetPropName(filter);

            // check property
            if (!ValidProp(propName, typeof(Player)))
                return true;

            var prop = PropManager.GetPropValue(player, propName);

            // get operation
            string op = filter.AsOperation().Value;

            // ignore < > operations of string or null values
            if (IllogicalComparison(prop, op))
                return true;

            // get argument as string
            string argString = filter.AsArgument().Value.Trim();

            Type propType = prop.GetType();

            // check expression on prop value
            if (!argString.CanCast(propType))
                return true;

            // set argument value
            var arg = argString.CastTo(propType);

            if (prop is CustomTime)
                prop = (prop as CustomTime).AsSeconds();  // explicit cast to see method

            return EvaluateExpression(arg, op, arg);
        }

        private static string GetPropName(string filter)
        {
            string propName = filter.AsProperty().Value;
            propName = PropManager.MapPlayerProp(propName);

            return propName;
        }

        private static bool ValidProp(string propName, Type type)
        {
            PropertyInfo? prop = type.GetProperty(propName);

            if (prop == null)
                return false;

            return true;
        }

        private static bool IllogicalComparison(dynamic prop, string op)
        {
            if (prop is string || prop is List<string> || prop == null)
                if (op.Contains('<') || op.Contains('>'))
                    return true;

            return false;
        }

        private static bool CanCast(this string value, Type type)
        {
            if (type == typeof(string) || type == typeof(List<string>))
                return true;

            Match timeMatch = value.AsTime();
            if (type == typeof(CustomTime))
                if (timeMatch.Value != "")
                    return true;

            if (type == typeof(int))
                if (timeMatch.Value == "" && !value.Contains('.'))
                    return true;

            if (type == typeof(double) || type == typeof(double?))
            {
                if (value == "null")
                    return true;

                Match doubleMatch = value.AsDouble();
                if (timeMatch.Value == "" && doubleMatch.Value != "")
                    return true;
            }

            return false;
        }

        private static dynamic? CastTo(this string value, Type type)
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

        private static bool EvaluateExpression(dynamic prop, string op, dynamic arg)
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
            if (!ValidProp(propName, typeof(Player)))
                return data;

            // get arg
            string arg = filter.AsAscDes().Value;

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
