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
                            
                            List<string> cells = line == null ? 
                                new List<string>() : 
                                line.Split(",").ToList();

                            if (cells.Count > 0)
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
    }
}
