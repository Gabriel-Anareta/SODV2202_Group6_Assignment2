using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NHLPlayers
{
    public partial class Form1 : Form
    {
        private List<Player> QueryResults;
        /*label1.Text = "GP > 60, Team == ANA";

            string toShow = "";
            MatchCollection matches = label1.Text.AsExpressions();
            IEnumerable<Player> result = players.Where(player => player.RunFilters(matches));

            toShow = $"Total count: {result.Count()}\n";

            foreach (Player player in result.Take<Player>(20))
            {
                toShow += $"{player.Name}\n";
            }

            label2.Text = toShow;*/

        public Form1()
        {
            InitializeComponent();

            dtGV_results.DataSource = PlayerData.AllData;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            QueryResults = PlayerData.AllData.Where(player => player.GP > 60).ToList();
            dtGV_results.DataSource = QueryResults;
        }
    }
}
