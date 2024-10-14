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
        public string TOI_GP { get; set; }      // TOI/GP changed to TOI_GP
        public double Shifts_GP { get; set; }   // Shifts/GP changed to Shifts_GP
        public double FOW_Perc { get; set; }    // FOW% changed to FOW_Perc

        public Player(List<string> data)
        {
            Name = data[0];             Plus_Minus = int.Parse(data[7]);    GWG = int.Parse(data[14]);
            Team = data[1];             PIM = int.Parse(data[8]);           OTG = int.Parse(data[15]);
            Pos = data[2];              P_GP = double.Parse(data[9]);       SOG = int.Parse(data[16]);
            GP = int.Parse(data[3]);    PPG = int.Parse(data[10]);          S_Perc = double.Parse(data[17]);
            G = int.Parse(data[4]);     PPP = int.Parse(data[11]);          TOI_GP = data[18];
            A = int.Parse(data[5]);     SHG = int.Parse(data[12]);          Shifts_GP = double.Parse(data[19]);
            PTS = int.Parse(data[6]);   SHP = int.Parse(data[13]);          FOW_Perc = double.Parse(data[20]);
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
                $"FOW_Perc: {FOW_Perc}"
            );
        }
    }
}
