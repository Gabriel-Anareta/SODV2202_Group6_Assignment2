using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NHLPlayers
{
    public static partial class RegexCollection
    {
        // REGEX strings used for reference

        // @"(\w|[+\-/%])+\s*[<>=]{1,2}\s*(\w|\s|[\-\.])+[^,]"              :   full expression
        // @"^(\w|[+\-/%])+"                                                :   property
        // @"[<>=]{1,2}"                                                    :   operation
        // @"(\w|\s|[\-\.])+$"                                              :   argument
        // @"(\w|[+\-/%])+\s+(\basc(ending){0,1}\b|\bdes(cending){0,1}\b)"  :   full order
        // @"(\basc(ending){0,1}\b|\bdes(cending){0,1}\b)$"                 :   asc des
        // @"[A-Z]{3}(\,\s[A-Z]{3})*"                                       :   filter for team
        // @"[0-9]{1,2}\:[0-9]{2}"                                          :   filter for CustomTime
        // @"\-{0,1}[0-9]{1,}\.{0,1}[0-9]*"                                 :   filter for double

        [GeneratedRegex(@"(\w|[+\-/%])+\s*[<>=]{1,2}\s*(\w|\s|[\-\.])+[^,]", RegexOptions.Compiled)]
        public static partial Regex ExpressionRegex();

        [GeneratedRegex(@"^(\w|[+\-/%])+", RegexOptions.Compiled)]
        public static partial Regex PropertyRegex();

        [GeneratedRegex(@"[<>=]{1,2}", RegexOptions.Compiled)]
        public static partial Regex OperationRegex();

        [GeneratedRegex(@"(\w|\s|[\-\.])+$", RegexOptions.Compiled)]
        public static partial Regex ArgumentRegex();

        [GeneratedRegex(@"(\w|[+\-/%])+\s+(\basc(ending){0,1}\b|\bdes(cending){0,1}\b)", RegexOptions.Compiled)]
        public static partial Regex OrderRegex();

        [GeneratedRegex(@"(\basc(ending){0,1}\b|\bdes(cending){0,1}\b)$", RegexOptions.Compiled)]
        public static partial Regex AscDesRegex();

        [GeneratedRegex(@"[A-Z]{3}(\,\s[A-Z]{3})*", RegexOptions.Compiled)]
        public static partial Regex TeamRegex();

        [GeneratedRegex(@"[0-9]{1,2}\:[0-9]{2}", RegexOptions.Compiled)]
        public static partial Regex TimeRegex();

        [GeneratedRegex(@"\-{0,1}[0-9]{1,}\.{0,1}[0-9]*", RegexOptions.Compiled)]
        public static partial Regex DoubleRegex();
    }
}
