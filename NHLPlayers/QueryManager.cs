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
                dynamic expTemp = ExpressionManager.GetMatch(filter.Value, @"(\w|\s|[\-\.])+$").Value.Trim();
                dynamic exp;
                Type propType = prop.GetType();
                

                if (prop is CustomTime)
                    exp = (new CustomTime(expTemp)).AsSeconds();  // explicit cast to see method
                else
                    exp = Convert.ChangeType(expTemp, propType);

                // do not allow < > operations of string values
                if ((op.Contains('<') || op.Contains('>')) && prop is string) 
                    continue;

                // dynamic type used to bypass compiler error when comparing two objects
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
                        state = state && (propVal == exp);
                        break;
                }
            }

            return state;
        }
    }
}
