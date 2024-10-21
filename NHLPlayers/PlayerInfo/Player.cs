using System.Reflection;
using NHLPlayers.Managers;

namespace NHLPlayers.PlayerInfo
{
    public class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
        public string Pos { get; set; }
        public int GP { get; set; }
        public int G { get; set; }
        public int A { get; set; }
        public int PTS { get; set; }            // P changed to PTS
        public int Plus_Minus { get; set; }     // +/- changed to Plus_Minus
        public int PIM { get; set; }
        public double? P_GP { get; set; }       // P/GP changed to P_GP
        public int PPG { get; set; }
        public int PPP { get; set; }
        public int SHG { get; set; }
        public int SHP { get; set; }
        public int GWG { get; set; }
        public int OTG { get; set; }
        public int SOG { get; set; }            // S changed to SOG
        public double S_Perc { get; set; }      // S% changed to S_Perc
        public CustomTime TOI_GP { get; set; }  // TOI/GP changed to TOI_GP
        public double Shifts_GP { get; set; }   // Shifts/GP changed to Shifts_GP
        public double FOW_Perc { get; set; }    // FOW% changed to FOW_Perc

        public Player(List<string> data)
        {
            // setting player properties using reflection
            PropertyInfo[] props = GetType().GetProperties();

            for (int i = 0; i < props.Length; i++)
                props[i].SetValue(this, DataValue(props[i], data[i]));
        }

        private dynamic? DataValue(PropertyInfo prop, string data)
        {
            Type propType = prop.PropertyType;

            if (propType == typeof(int))
                return int.Parse(data);

            if (propType == typeof(double) || propType == typeof(double?))
            {
                if (data == "--")
                    return null as double?;
                else
                    return double.Parse(data);
            }

            if (propType == typeof(string))
            {
                if (prop.Name == "Team")
                    return SortedTeams(data);
                else
                    return data;
            }

            if (propType == typeof(CustomTime))
                return new CustomTime(data);

            return Convert.ChangeType(null, propType);
        }

        private string SortedTeams(string data)
        {
            List<string> teamsList = new List<string>();
            foreach (string item in data.Split(','))
                teamsList.Add(item.Trim());

            teamsList.Sort();

            string teamsString = "";
            for (int i = 0; i < teamsList.Count; i++)
            {
                teamsString += teamsList[i];
                if (i + 1 != teamsList.Count)
                    teamsString += ", ";
            }

            return teamsString;
        }

        public override string ToString()
        {
            string propsString = "";
            PropertyInfo[] props = GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                string name = PropManager.MapPlayerProp(prop.Name);
                propsString += $"{name}: {prop.GetValue(this)}\n";
            }

            return propsString;
        }
    }
}
