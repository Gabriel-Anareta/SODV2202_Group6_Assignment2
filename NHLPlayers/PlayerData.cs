using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace NHLPlayers
{
    static public class PlayerData
    {
        static public List<Player> AllData = GetData(@"NHL Player Stats 2017-18.csv");

        static public List<Player> GetData(string path)
        {
            List<Player> data = new List<Player>();
            string csvPath = Path.Combine(Environment.CurrentDirectory, path);

            if (File.Exists(csvPath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(csvPath))
                    {
                        int count = 0;
                        while (!reader.EndOfStream)
                        {
                            string? line = reader.ReadLine();

                            if (count == 0)
                            {
                                count++;
                                continue;
                            }

                            if (line == null)
                                continue;

                            Match match = ExpressionManager.GetMatch(line, @"[A-Z]{3}(\,\s[A-Z]{3})*"); // make special case for multiple teams
                            List<string> cells = CustomSplit(line, match.Value);
                            
                            data.Add(new Player(cells));
                        }
                    }
                }
                catch (Exception e)
                {
                    // Catch error in reading file -- Place code here to display an error message
                }
            }
            else
            {
                // Catch exception in invalid file path -- Place code here to display an error message
            }

            return data;
        }

        private static List<string> CustomSplit(string line, string teams)
        {
            List<string> split = new List<string>();
            int index = -1;
            int teamIndex = line.IndexOf(teams);

            while (true)
            {
                int start = index + 1;
                index = line.IndexOf(',', index + 1);

                if (index > teamIndex && index <= teamIndex + teams.Length)
                {
                    split.Add(teams);
                    index = teamIndex + teams.Length;
                    continue;
                }

                if (index < 0)
                {
                    split.Add(line.Substring(start, line.Length - start));
                    break;
                }

                split.Add(line.Substring(start, index - start));
            }

            return split;
        }
    }
}
