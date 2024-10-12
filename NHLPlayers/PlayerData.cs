namespace NHLPlayers
{
    static public class PlayerData
    {
        static public List<string> AllData = GetData(@"NHL Player Stats 2017-18.csv");

        static public List<string> GetData(string path)
        {
            List<string> data = new List<string>();
            string csvPath = Path.Combine(Environment.CurrentDirectory, path);

            if (File.Exists(csvPath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(csvPath))
                    {
                        while (!reader.EndOfStream)
                        {
                            data.Add(reader.ReadLine());
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
