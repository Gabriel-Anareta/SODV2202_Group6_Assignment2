using System.Reflection;

namespace NHLPlayers
{
    public class Player
    {
        public string Name { get; set; }
        public List<string> Team { get; set; }
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
            PropertyInfo[] props = this.GetType().GetProperties();

            for (int i = 0; i < props.Length; i++)
                SetValue(props[i], data[i]);
        }

        public Player()
        {
            PropertyInfo[] props = this.GetType().GetProperties();
            List<string> data = new List<string>();
            for (int i = 0; i < 21; i++)
                data.Add("0");

            for (int i = 0; i < props.Length; i++)
            {
                SetValue(props[i], data[i]);
            }
        }

        private void SetValue(PropertyInfo prop, string data)
        {
            Type propType = prop.PropertyType;

            if (propType == typeof(int))
            {
                prop.SetValue(this, int.Parse(data));
                return;
            }

            if (propType == typeof(double) || propType == typeof(double?))
            {
                if (data == "--")
                    prop.SetValue(this, null);
                else
                    prop.SetValue(this, double.Parse(data));
                return;
            }

            if (propType == typeof(string))
            {
                prop.SetValue(this, data);
                return;
            }

            if (propType == typeof(List<string>))
            {
                List<string> teams = data.Split(',').ToList();
                for (int i = 0; i < teams.Count; i++)
                    teams[i] = teams[i].Trim();
                teams.Sort();
                prop.SetValue(this, teams);
                return;
            }

            if (propType == typeof(CustomTime))
            {
                prop.SetValue(this, new CustomTime(data));
                return;
            }
        }

        public override string ToString()
        {
            string propsString = "";
            PropertyInfo[] props = this.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                string name = PropManager.MapPlayerProp(prop.Name);
                if (name == "Team")
                {
                    string value = "";
                    var list = prop.GetValue(this) as List<string>;
                    for (int i = 0; i < list.Count; i++)
                    {
                        value += list[i];
                        if (i + 1 != list.Count)
                            value += ", ";
                    }
                    propsString += $"{name}: {value}\n";
                }
                else
                {
                    propsString += $"{name}: {prop.GetValue(this)}\n";
                }
            }

            return propsString;
        }
    }
}
