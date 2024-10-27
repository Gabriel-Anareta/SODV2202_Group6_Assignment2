using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NHLPlayers.Managers;
using NHLPlayers.PlayerInfo;

namespace NHLPlayers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dtGV_results.DataSource = PlayerData.AllData;
            lbl_ResultCount.Text = $"Result Count: {PlayerData.AllData.Count}";
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            UpdateDataSource();
        }

        private void tb_OnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateDataSource();
                this.ActiveControl = null;
            }
        }

        private void UpdateDataSource()
        {
            MatchCollection filterMatches = tb_filter.Text.AsExpressions();
            MatchCollection orderMatches = tb_order.Text.AsOrders();
            var res = PlayerData.AllData
                .Where(player => player.RunFilters(filterMatches))
                .RunOrders(orderMatches)
                .ToList();

            dtGV_results.DataSource = res;
            lbl_ResultCount.Text = $"Result Count: {res.Count}";
        }
    }
}
