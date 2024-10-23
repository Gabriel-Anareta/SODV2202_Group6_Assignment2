using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;
using NHLPlayers.Managers;

namespace NHLPlayers.PlayerInfo
{
    static public class PlayerData
    {
        static public readonly List<Player> AllData = GetData(@"Data/NHL Player Stats 2017-18.csv");

        static public List<Player> GetData(string path)
        {
            List<Player> data;
            string csvPath = Path.Combine(Environment.CurrentDirectory, path);

            try
            {
                data = ReadFile(csvPath);
            }
            catch (Exception e)
            {
                data = new List<Player>();
            }

            return data;
        }

        private static List<string> GetFeilds(string path)
        {
            List<string> lineValues = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                string? line = reader.ReadLine();

                if (line == null)
                    return lineValues;

                lineValues = line.Split(',').ToList();
            }

            return lineValues;
        }

        private static List<Player> ReadFile(string path)
        {
            List<Player> data = new List<Player>();

            using (StreamReader reader = new StreamReader(path))
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

                    Match teams = line.AsTeam();
                    List<string> cells = line.CustomSplit(teams.Value);
                    Player set = new Player(cells);

                    data.Add(new Player(cells));
                }
            }

            return data;
        }

        private static List<string> CustomSplit(this string line, string teams)
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
