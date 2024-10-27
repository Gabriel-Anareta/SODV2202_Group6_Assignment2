using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NHLPlayers.Managers
{
    public static class ExpressionManager
    {
        public static MatchCollection AsExpressions(this string input) => RegexCollection.ExpressionRegex().Matches(input);

        public static Match AsProperty(this string input) => RegexCollection.PropertyRegex().Match(input);

        public static Match AsOperation(this string input) => RegexCollection.OperationRegex().Match(input);

        public static Match AsArgument(this string input) => RegexCollection.ArgumentRegex().Match(input);

        public static MatchCollection AsOrders(this string input) => RegexCollection.OrderRegex().Matches(input);

        public static Match AsAscDes(this string input) => RegexCollection.AscDesRegex().Match(input);

        public static Match AsTeam(this string input) => RegexCollection.TeamRegex().Match(input);

        public static Match AsTime(this string input) => RegexCollection.TimeRegex().Match(input);

        public static Match AsDouble(this string input) => RegexCollection.DoubleRegex().Match(input);
    }
}
