using System.Linq;
using System.Linq.Expressions;

namespace NHLPlayers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<Player> list = Test(player => player.FOW_Perc > 40).ToList();

            label1.Text = list[1].ToString();
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
