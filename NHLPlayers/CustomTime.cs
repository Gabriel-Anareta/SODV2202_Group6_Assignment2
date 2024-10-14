using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLPlayers
{
    internal class CustomTime
    {
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public CustomTime(int minutes, int seconds)
        {
            Minutes = minutes;
            Seconds = seconds;
        }

        public override string ToString()
        {
            return $"{Minutes}:{Seconds}";
        }
    }
}
