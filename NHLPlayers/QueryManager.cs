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
        private static List<Func<bool>> CreateFilter(MatchCollection rawfilters, Object obj)
        {
            List<Func<bool>> filters = new List<Func<bool>>();

            foreach (Match filter in rawfilters)
            {
                // get property
                string prop = ExpressionManager.GetMatch(filter.Value, @"^(\w|[+\-/%])+").Value;
                object propObj = PropManager.GetPropValue(obj, prop);
                dynamic propVal = propObj.GetType().GetProperty("value").GetValue(propObj);

                // check property on object
                if (propVal == "invalid porperty")
                    continue;

                // get operation
                string op = ExpressionManager.GetMatch(filter.Value, @"[<>=]{1,2}").Value.Trim();

                // get expression from
                dynamic exp = ExpressionManager.GetMatch(filter.Value, @"(\w|\s|[\-\.])+$").Value.Trim();

                // do not allow < > operations of string values
                if (
                    (op.Contains('<') || op.Contains('>')) &&
                    (propVal is string || exp is string)
                ) continue;


                // dynamic type used to bypass compiler error when comparing two objects
                switch(op)
                {
                    case "<":
                        filters.Add(() => propVal < exp);
                        break;
                    case ">":
                        filters.Add(() => propVal > exp);
                        break;
                    case "<=": case "=<":
                        filters.Add(() => propVal <= exp);
                        break;
                    case ">=": case "=>":
                        filters.Add(() => propVal >= exp);
                        break;
                    case "==":
                        filters.Add(() => propVal == exp);
                        break;
                }  
            }

            return filters;
        }
    }
}
