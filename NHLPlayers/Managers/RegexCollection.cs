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

        [GeneratedRegex(@"(\w|[+\-/%])+\s*[<>=]{1,2}\s*(\w|\s|[\-.:])+[^,]", RegexOptions.Compiled)]
        public static partial Regex ExpressionRegex(); // gets an expression command in the from "property operation argument"

        [GeneratedRegex(@"^(\w|[+\-/%])+", RegexOptions.Compiled)]
        public static partial Regex PropertyRegex(); // gets property value from an expression

        [GeneratedRegex(@"[<>=]{1,2}", RegexOptions.Compiled)]
        public static partial Regex OperationRegex(); // gets operation value from an expression

        [GeneratedRegex(@"(\w|\s|[\-.:])+$", RegexOptions.Compiled)]
        public static partial Regex ArgumentRegex(); // gets argument value from an expression

        [GeneratedRegex(@"(\w|[+\-/%])+\s+(\basc(ending){0,1}\b|\bdes(cending){0,1}\b)", RegexOptions.Compiled)]
        public static partial Regex OrderRegex(); // gets an order command in the form "property-name ascending-descending"

        [GeneratedRegex(@"(\basc(ending){0,1}\b|\bdes(cending){0,1}\b)$", RegexOptions.Compiled)]
        public static partial Regex AscDesRegex(); // gets order direction value from an expression

        [GeneratedRegex(@"[A-Z]{3}(\,\s[A-Z]{3})*", RegexOptions.Compiled)]
        public static partial Regex TeamRegex(); // gets team values in the form "team-acronym, " for every team name in succession

        [GeneratedRegex(@"[0-9]{1,2}\:[0-9]{2}", RegexOptions.Compiled)]
        public static partial Regex TimeRegex(); // gets time values in the form "minutes:seconds"

        [GeneratedRegex(@"\-{0,1}[0-9]{1,}\.{0,1}[0-9]*", RegexOptions.Compiled)]
        public static partial Regex DoubleRegex(); // gets valid double values - can return multiple results if checked on a time value
    }
}
