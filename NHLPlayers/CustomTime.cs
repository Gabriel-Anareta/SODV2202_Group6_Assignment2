using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLPlayers
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

        public override string ToString()
        {
            return $"{Minutes}:{Seconds}";
        }
    }
}
