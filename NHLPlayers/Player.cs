using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLPlayers
{
    internal class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
        public string Pos { get; set; }
        public string GP { get; set; }
        public string G { get; set; }
        public string A { get; set; }
        public string PTS { get; set; }         // P changed to PTS
        public string Plus_Minus { get; set; }  // +/- changed to Plus_Minus
        public string PIM { get; set; }
        public string P_GP { get; set; }        // P/GP changed to P_GP
        public string PPG { get; set; }
        public string PPP { get; set; }
        public string SHG { get; set; }
        public string SHP { get; set; }
        public string GWG { get; set; }
        public string OTG { get; set; }
        public string SOG { get; set; }         // S changed to SOG
        public string S_Perc { get; set; }      // S% changed to S_Perc
        public string TOI_GP { get; set; }      // TOI/GP changed to TOI_GP
        public string Shifts_GP { get; set; }   // Shifts/GP changed to Shifts_GP
        public string FOW_Perc { get; set;  }   // FOW% changed to FOW_Perc 
    }
}
