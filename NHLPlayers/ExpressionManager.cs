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
        // REGEX strings used for reference

        // @"(\w|[+\-/%])+\s*[<>=]{1,2}\s*(\w|\s|[\-\.])+[^,]"      :   full expression
        // @"^(\w|[+\-/%])+"                                        :   property
        // @"[<>=]{1,2}"                                            :   operation
        // @"(\w|\s|[\-\.])+$"                                      :   argument
        // @"[A-Z]{3}(\,\s[A-Z]{3})*"                               :   filter for team
        // @"[0-9]{1,2}\:[0-9]{2}"                                  :   filter for CustomTime
        // @"\-{0,1}[0-9]{1,}\.{0,1}[0-9]*"                         :   filter for double

        private static readonly Regex ExpressionMatch = new Regex(@"(\w|[+\-/%])+\s*[<>=]{1,2}\s*(\w|\s|[\-\.])+[^,]", RegexOptions.Compiled);
        private static readonly Regex PropertyMatch = new Regex(@"^(\w|[+\-/%])+", RegexOptions.Compiled);
        private static readonly Regex OperationMatch = new Regex(@"[<>=]{1,2}", RegexOptions.Compiled);
        private static readonly Regex ArgumentMatch = new Regex(@"(\w|\s|[\-\.])+$", RegexOptions.Compiled);
        private static readonly Regex TeamMatch = new Regex(@"[A-Z]{3}(\,\s[A-Z]{3})*", RegexOptions.Compiled);
        private static readonly Regex TimeMatch = new Regex(@"[0-9]{1,2}\:[0-9]{2}", RegexOptions.Compiled);
        private static readonly Regex DoubleMatch = new Regex(@"\-{0,1}[0-9]{1,}\.{0,1}[0-9]*", RegexOptions.Compiled);

        public static MatchCollection GetExpressions(string input)
        {
            return ExpressionMatch.Matches(input);
        }

        public static Match GetProperty(string input)
        {
            return PropertyMatch.Match(input);
        }

        public static Match GetOperation(string input)
        {
            return OperationMatch.Match(input);
        }

        public static Match GetArgument(string input)
        {
            return ArgumentMatch.Match(input);
        }

        public static Match GetTeam(string input)
        {
            return TeamMatch.Match(input);
        }

        public static Match GetTime(string input)
        {
            return TimeMatch.Match(input);
        }

        public static Match GetDouble(string input)
        {
            return DoubleMatch.Match(input);
        }
    }
}
