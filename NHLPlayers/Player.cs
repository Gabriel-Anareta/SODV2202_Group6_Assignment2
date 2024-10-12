using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NHLPlayers
{
    public class Player
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
        public string FOW_Perc { get; set; }   // FOW% changed to FOW_Perc 

        public Player(List<string> data)
        {
            Name = data[0];     Plus_Minus = data[7];   GWG = data[14];
            Team = data[1];     PIM = data[8];          OTG = data[15];
            Pos = data[2];      P_GP = data[9];         SOG = data[16];      
            GP = data[3];       PPG = data[10];         S_Perc = data[17];   
            G = data[4];        PPP = data[11];         TOI_GP = data[18];   
            A = data[5];        SHG = data[12];         Shifts_GP = data[19];
            PTS = data[6];      SHP = data[13];         FOW_Perc = data[20]; 
        }

        public override string ToString()
        {
            return (
                $"Name: {Name}" +
                $"Team: {Team}\n" +
                $"Pos: {Pos}\n" +
                $"GP: {GP}\n" +
                $"G: {G}\n" +
                $"A: {A}\n" +
                $"PTS: {PTS}\n" +
                $"Plus_Minus: {Plus_Minus}\n" +
                $"PIM: {PIM}\n" +
                $"P_GP: {P_GP}\n" +
                $"PPG: {PPG}\n" +
                $"PPP: {PPP}\n" +
                $"SHG: {SHG}\n" +
                $"SHP: {SHP}\n" +
                $"GWG: {GWG}\n" +
                $"OTG: {OTG}\n" +
                $"SOG: {SOG}\n" +
                $"S_Perc: {S_Perc}\n" +
                $"TOI_GP: {TOI_GP}\n" +
                $"Shifts_GP: {Shifts_GP}\n" +
                $"FOW_Perc: { FOW_Perc }"
            );
        }
    }
}
