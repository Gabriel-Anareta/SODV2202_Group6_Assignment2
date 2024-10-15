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
        public static bool RunFilters(MatchCollection rawfilters, Object obj)
        {
            bool state = true;

            foreach (Match filter in rawfilters)
            {
                // get property
                string prop = ExpressionManager.GetMatch(filter.Value, @"^(\w|[+\-/%])+").Value;
                object propObj = PropManager.GetPropValue(obj, prop);
                dynamic propVal = propObj.GetType().GetProperty("value").GetValue(propObj);

                // check property on object
                if (propVal is string)
                {
                    if (propVal == "invalid property")
                        continue;
                }

                // get operation
                string op = ExpressionManager.GetMatch(filter.Value, @"[<>=]{1,2}").Value.Trim();

                // get expression
                dynamic exp = ExpressionManager.GetMatch(filter.Value, @"(\w|\s|[\-\.])+$").Value.Trim();
                Type propType = propVal.GetType();
                exp = Convert.ChangeType(exp, propType);

                // do not allow < > operations of string values
                if (
                    (op.Contains('<') || op.Contains('>')) &&
                    (propVal is string)
                ) continue;

                // dynamic type used to bypass compiler error when comparing two objects
                switch (op)
                {
                    case "<":
                        state = state && (propVal < exp);
                        break;
                    case ">":
                        state = state && (propVal > exp);
                        break;
                    case "<=": case "=<":
                        state = state && (propVal <= exp);
                        break;
                    case ">=": case "=>":
                        state = state && (propVal >= exp);
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
