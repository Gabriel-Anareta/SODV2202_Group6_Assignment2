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
        public static MatchCollection GetExpressions(string input) => RegexCollection.ExpressionRegex().Matches(input);

        public static Match GetProperty(string input) => RegexCollection.PropertyRegex().Match(input);

        public static Match GetOperation(string input) => RegexCollection.OperationRegex().Match(input);

        public static Match GetArgument(string input) => RegexCollection.ArgumentRegex().Match(input);

        public static MatchCollection GetOrders(string input) => RegexCollection.OrderRegex().Matches(input);

        public static Match GetAscDes(string input) => RegexCollection.AscDesRegex().Match(input);

        public static Match GetTeam(string input) => RegexCollection.TeamRegex().Match(input);

        public static Match GetTime(string input) => RegexCollection.TimeRegex().Match(input);

        public static Match GetDouble(string input) => RegexCollection.DoubleRegex().Match(input);
    }
}
