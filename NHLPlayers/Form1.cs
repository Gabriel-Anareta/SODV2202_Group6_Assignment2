using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NHLPlayers
{
    public partial class Form1 : Form
    {
        public List<Player> players = PlayerData.AllData;

        public Form1()
        {
            InitializeComponent();

            List<Player> list = Test(player => player.FOW_Perc > 40).ToList();

            label1.Text = "GP <= 10, P > 10, +/-=> 10, S_%>= 50, Name == Anderson - Jay, n , <<, GP <<, ";

            string toShow = "";
            MatchCollection matches = ExpressionManager.GetMatches(label1.Text, @"(\w|[+\-/%])+\s*[<>=]{1,2}\s*(\w|\s|[\-\.])+[^,]");

            foreach (Match match in matches)
            {
                toShow += $"{match.Value}\n";
            }

            label2.Text = toShow;
        }

        /*public delegate bool Callback(Player player);*/

        public IEnumerable<Player> Test(Func<Player, bool> callback)
        {
            IEnumerable<Player> test = players.Where(player => callback(player));
            /*from player in players
            where testCallback(player)
            select player;*/

            return test;
        }
    }
}
