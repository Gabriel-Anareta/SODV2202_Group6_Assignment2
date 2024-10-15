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

            foreach (Match filter in rawfilters)
            {
                // get property
                string propString = ExpressionManager.GetMatch(filter.Value, @"^(\w|[+\-/%])+").Value;
                propString = PropManager.MapPlayerProp(propString);
                object propObj = PropManager.GetPropValue(obj, propString);
                dynamic propTemp = propObj.GetType().GetProperty("value").GetValue(propObj);
                dynamic prop;

                // check property on object
                if (propTemp is string)
                    if (propTemp == "invalid property") continue;

                if (propTemp is CustomTime)
                    prop = (propTemp as CustomTime).AsSeconds();  // explicit cast to see method
                else
                    prop = propTemp;

                // get operation
                string op = ExpressionManager.GetMatch(filter.Value, @"[<>=]{1,2}").Value.Trim();

                // get expression
                string expTemp = ExpressionManager.GetMatch(filter.Value, @"(\w|\s|[\-\.])+$").Value.Trim();
                dynamic exp;
                Type propType = prop.GetType();
                
                // check expression on prop value for casting
                if (CanCast(expTemp, prop))
                    exp = CastTo(expTemp, propTemp);
                else
                    continue;

                // do not allow < > operations of string values
                if ((op.Contains('<') || op.Contains('>')) && prop is string) 
                    continue;

                // run expressions based on given operations
                switch (op)
                {
                    case "<":
                        state = state && (prop < exp);
                        break;
                    case ">":
                        state = state && (prop > exp);
                        break;
                    case "<=": case "=<":
                        state = state && (prop <= exp);
                        break;
                    case ">=": case "=>":
                        state = state && (prop >= exp);
                        break;
                    case "==":
                        state = state && (prop == exp);
                        break;
                }
            }

            return state;
        }
        
        private static bool CanCast(string value, dynamic castTo)
        {
            if (castTo is string)
                return true;

            Match timeMatch = ExpressionManager.GetMatch(value, @"[0-9]{1,2}\:[0-9]{2}");
            if (castTo is CustomTime)
                if (timeMatch != null) return true;

            bool intMatch = value.Contains('.');
            if (castTo is int)
                if (timeMatch == null && !intMatch) return true;

            Match doubleMatch = ExpressionManager.GetMatch(value, @"\-*[0-9]{1,}\.{0,1}[0-9]*");
            if (castTo is double)
                if (timeMatch == null && doubleMatch != null) return true;

            return false;
        }

        private static dynamic CastTo(string value, dynamic castTo)
        {
            if (castTo is string)
                return value;

            if (castTo is CustomTime)
                return (new CustomTime(value)).AsSeconds();

            if (castTo is int)
                return int.Parse(value);

            if (castTo is double)
                return double.Parse(value);

            return value;
        }
    }
}
