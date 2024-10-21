using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NHLPlayers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UpdateDataSource();
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
            dtGV_results.DataSource = PlayerData.AllData
                .Where(player => player.RunFilters(filterMatches))
                .RunOrders(orderMatches)
                .ToList();
        }
    }
}
