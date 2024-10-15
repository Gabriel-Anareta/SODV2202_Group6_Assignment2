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
                prop = PropManager.MapPlayerProp(prop);
                object propObj = PropManager.GetPropValue(obj, prop);
                dynamic propValTemp = propObj.GetType().GetProperty("value").GetValue(propObj);
                dynamic propVal;

                // check property on object
                if (propValTemp is string)
                {
                    if (propValTemp == "invalid property")
                        continue;
                }

                if (propValTemp is CustomTime)
                    propVal = (propValTemp as CustomTime).AsSeconds();
                else
                    propVal = propValTemp;

                // get operation
                string op = ExpressionManager.GetMatch(filter.Value, @"[<>=]{1,2}").Value.Trim();

                // get expression
                dynamic expTemp = ExpressionManager.GetMatch(filter.Value, @"(\w|\s|[\-\.])+$").Value.Trim();
                dynamic exp;
                Type propType = propVal.GetType();
                expTemp = Convert.ChangeType(expTemp, propType);

                if (expTemp is CustomTime)
                    exp = (expTemp as CustomTime).AsSeconds();
                else
                    exp = expTemp;

                // do not allow < > operations of string values
                if ((op.Contains('<') || op.Contains('>')) && propVal is string) 
                    continue;

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
