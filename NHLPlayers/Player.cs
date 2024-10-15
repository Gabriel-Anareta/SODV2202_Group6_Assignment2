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
        public double? P_GP { get; set; }        // P/GP changed to P_GP
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
            PropertyInfo[] props = this.GetType().GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                SetValue(props[i], data[i], i);
            }
        }

        private void SetValue(PropertyInfo prop, string data, int i)
        {
            switch (i)
            {
                case 0: case 2:
                    prop.SetValue(this, data);
                    break;
                case 1:
                    List<string> teams = data.Split(',').ToList();

                    for (int j = 0; j < teams.Count; j++)
                        teams[j] = teams[j].Trim();

                    prop.SetValue(this, teams);
                    break;
                case 9:
                    if (data == "--")
                        prop.SetValue(this, null);
                    else
                        prop.SetValue(this, double.Parse(data));
                        break;
                case 17: case 19: case 20:
                    prop.SetValue(this, double.Parse(data));
                    break;
                case 18:
                    prop.SetValue(this, new CustomTime(data));
                    break;
                default:
                    prop.SetValue(this, int.Parse(data));
                    break;
            }
        }

        public override string ToString()
        {
            string propsString = "";
            PropertyInfo[] props = this.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == "Team")
                {
                    string value = "";
                    var list = prop.GetValue(this) as List<string>;
                    for (int i = 0; i < list.Count; i++)
                    {
                        value += list[i];
                        if (i + 1 != list.Count)
                            value += ", ";
                    }
                    propsString += $"{prop.Name}: {value}\n";
                }
                else
                {
                    propsString += $"{prop.Name}: {prop.GetValue(this)}\n";
                }
            }

            return propsString;
        }
    }
}
