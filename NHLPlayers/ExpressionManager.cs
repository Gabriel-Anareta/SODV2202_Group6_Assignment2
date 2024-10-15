using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NHLPlayers
{
    public static class ExpressionManager
    {
        // REGEX strings used in code for reference

        // @"(\w|[+\-/%])+\s*[<>=]{1,2}\s*(\w|\s|[\-\.])+[^,]"      :   full expression
        // @"^(\w|[+\-/%])+"                                        :   property
        // @"[<>=]{1,2}"                                            :   operation
        // @"(\w|\s|[\-\.])+$"                                      :   argument

        // @"[A-Z]{3}(\,\s[A-Z]{3})*"                               :   filter for team 

        // @"[0-9]{1,2}\:[0-9]{2}"                                  :   filter for CustomTime
        // @"\-*[0-9]{1,}\.{0,1}[0-9]*"                             :   filter for double

        public static MatchCollection GetMatches(string input, string filter)
        {
            return Regex.Matches(input, filter, RegexOptions.Compiled);
        }

        public static Match GetMatch(string input, string filter)
        {
            return Regex.Match(input, filter, RegexOptions.Compiled);
        }
    }
}
