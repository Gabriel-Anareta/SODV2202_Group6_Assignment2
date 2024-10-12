using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public Player(
            string _Name,           string _Plus_Minus,         string _GWG,
            string _Team,           string _PIM,                string _OTG,
            string _Pos,            string _P_GP,               string _SOG,
            string _GP,             string _PPG,                string _S_Perc,
            string _G,              string _PPP,                string _TOI_GP,
            string _A,              string _SHG,                string _Shifts_GP,
            string _PTS,            string _SHP,                string _FOW_Perc)
        {
            Name = _Name;           Plus_Minus = _Plus_Minus;   GWG = _GWG;
            Team = _Team;           PIM = _PIM;                 OTG = _OTG;
            Pos = _Pos;             P_GP = _P_GP;               SOG = _SOG;      
            GP = _GP;               PPG = _PPG;                 S_Perc = _S_Perc;   
            G = _G;                 PPP = _PPP;                 TOI_GP = _TOI_GP;   
            A = _A;                 SHG = _SHG;                 Shifts_GP = _Shifts_GP;
            PTS = _PTS;             SHP = _SHP;                 FOW_Perc = _FOW_Perc; 
        }
    }
}
