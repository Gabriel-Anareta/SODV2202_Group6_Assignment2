using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using NHLPlayers.PlayerInfo;

namespace NHLPlayers.Managers
{
    public static class QueryManager
    {
        public static bool RunFilters(this Player player, MatchCollection filters)
        {
            // Check player on all filters
            for (int i = 0; i < filters.Count; i++)
                if (!RunExpression(filters[i].Value, player))
                    return false;

            return true;
        }

        public static IEnumerable<Player> RunOrders(this IEnumerable<Player> data, MatchCollection orders)
        {
            var result = data;
            bool isMainFilter = true;

            // Run orders
            for (int i = 0; i < orders.Count; i++)
            {
                result = result.OrderData(orders[i].Value, isMainFilter);
                isMainFilter = false;
            }

            return result;
        }


        // Helper functions

        private static bool RunExpression(string filter, Player player)
        {
            // get property Name
            string propName = GetPropName(filter);

            // check property
            if (!ValidProp(propName, typeof(Player)))
                return true;

            // get property value
            dynamic? propTemp = player.GetPropValue(propName);

            // get property type
            Type propType = typeof(Player).GetProperty(propName).PropertyType;

            // get operation
            string op = filter.AsOperation().Value;

            // ignore < > operations of string or null values
            if (IllogicalComparison(propTemp, op))
                return true;

            // get argument as string
            string argString = filter.AsArgument().Value.Trim();

            // check expression on prop value
            if (!argString.CanCast(propType))
                return true;

            // set argument value
            dynamic? arg = argString.CastTo(propType);

            dynamic? prop;
            if (propTemp is CustomTime)
                prop = (propTemp as CustomTime).AsSeconds();  // explicit cast to see method
            else
                prop = propTemp;

            // return the evaluated expression
            return EvaluateExpression(prop, op, arg);
        }

        public static string GetPropName(string filter)
        {
            string propName = filter.AsProperty().Value;
            propName = PropManager.MapPlayerProp(propName);

            return propName;
        }

        public static bool ValidProp(string propName, Type type)
        {
            PropertyInfo? prop = type.GetProperty(propName);

            if (prop == null)
                return false;

            return true;
        }

        public static dynamic GetPropValue(this Player player, string propName)
        {
            dynamic? prop = PropManager.GetPropValue(player, propName);

            if (prop is string)
                if ((prop as string).Contains(','))
                    return AsTeamList(prop);

            return prop;
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
                return new CustomTime(value).AsSeconds();

            if (type == typeof(int))
                return int.Parse(value);

            if (type == typeof(double) || type == typeof(double?))
            {
                if (value == "null")
                    return null as double?;
                else
                    return double.Parse(value);
            }

            return value;
        }

        private static List<string> AsTeamList(string value)
        {
            List<string> teamList = new List<string>();
            foreach (string item in value.Split(','))
                teamList.Add(item.Trim());
            return teamList;
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

        private static IEnumerable<Player> OrderData(this IEnumerable<Player> data, string filter, bool isMainFilter)
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

            if (orderByValue is CustomTime)
                return (orderByValue as CustomTime).AsSeconds();

            return orderByValue;
        }
    }
}
