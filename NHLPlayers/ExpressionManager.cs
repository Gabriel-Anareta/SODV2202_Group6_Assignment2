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
        // @"(\w|[+\-/%])+[<>=]{1,2}(\w|[\-\.])+"       :   full ex
        // @"^(\w|[+\-/%])+"                            :   prop
        // @"[<>=]{1,2}"                                :   op
        // @"(\w|[\-\.])+$"                             :   arg

        public static MatchCollection GetMatches(string input, string filter)
        {
            Regex regex = new Regex(filter, RegexOptions.Compiled);

            return regex.Matches(input);
        }

        public static Match GetMatch(string input, string filter)
        {
            Regex regex = new Regex(filter, RegexOptions.Compiled);

            return regex.Match(input);
        }
    }
}
