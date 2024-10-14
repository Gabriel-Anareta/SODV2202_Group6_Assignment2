using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NHLPlayers
{
    public static class ExpressionManager
    {
        // (\w|[+\-/%])+[<>=]{1,2}(\w|[\-\.])+      :   full ex
        // ^(\w|[+\-/%])+                           :   prop
        // [<>=]{1,2}                               :   op
        // (\w|[\-\.])+$                            :   arg

        public static MatchCollection GetExpressions(string input)
        {
            Regex filter = new Regex(@"(\w|[+\-/%])+[<>=]{1,2}(\w|[\-\.])+", RegexOptions.Compiled);

            return filter.Matches(input);
        }
    }
}
