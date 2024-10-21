using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLPlayers.PlayerInfo
{
    public class CustomTime
    {
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public CustomTime(string time)
        {
            string[] split = time.Split(':');
            Minutes = int.Parse(split[0]);
            Seconds = int.Parse(split[1]);
        }

        public int AsSeconds()
        {
            return Minutes * 60 + Seconds;
        }

        public override string ToString()
        {
            string secondsAsString = intToString(Seconds);
            string minutesAsString = intToString(Minutes);

            return $"{minutesAsString}:{secondsAsString}";
        }

        private string intToString(int value)
        {
            string valueString = value.ToString();
            if (value < 10)
                valueString = "0" + valueString;

            return valueString;
        }
    }
}
