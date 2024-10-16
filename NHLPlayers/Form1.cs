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

            label1.Text = "GP > 60, FOW% > 40";

            string toShow = "";
            MatchCollection matches = ExpressionManager.GetExpressions(label1.Text);
            IEnumerable<Player> result = players.Where(player => QueryManager.RunFilters(matches, player));

            toShow = $"{result.Count()}";

            /*foreach (Match in matches.Take<>())
            {
                toShow += $"{match.Value}\n";
            }*/

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
