using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NHLPlayers
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
        public double P_GP { get; set; }        // P/GP changed to P_GP
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
                SetValue(props, data, i);
            }
        }

        private void SetValue(PropertyInfo[] props, List<string> data, int i)
        {
            if (i <= 2)
            {
                props[i].SetValue(this, data[i]);
                return;
            }

            if (i == 9 || i == 17 || i == 19 || i == 20)
            {
                props[i].SetValue(this, double.Parse(data[i]));
                return;
            }

            if (i == 18)
            {
                props[i].SetValue(this, new CustomTime(data[18]));
                return;
            }

            props[i].SetValue(this, int.Parse(data[i]));
        }
        
        public override string ToString()
        {
            string propsString = "";
            PropertyInfo[] props = this.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                propsString += $"{prop.Name}: {prop.GetValue(this)}\n";
            }

            return propsString;
        }
    }
}
