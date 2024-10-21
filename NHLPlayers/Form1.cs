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
            /*dtGV_results.AutoGenerateColumns = false;*/
            
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

            /*PropertyInfo[] props = typeof(Player).GetProperties();

            for (int i = 0; i < props.Length; i++)
                dtGV_results.Columns.Insert(i, CreateColumn(props[i].Name));*/
        }

        /*
         * .Select(player =>
                {
                    string value = "";
                    var list = player.Team;
                    for (int i = 0; i < list.Count; i++)
                    {
                        value += list[i];
                        if (i + 1 != list.Count)
                            value += ", ";
                    }
                    return new
                    {
                        Name = player.Name,
                        Team = value,
                        Pos = player.Pos,
                        GP = player.GP,
                        G = player.G,
                        A = player.A,
                        PTS = player.PTS,
                        Plus_Minus = player.Plus_Minus,
                        PIM = player.PIM,
                        P_GP = player.P_GP,
                        PPG = player.PPG,
                        PPP = player.PPP,
                        SHG = player.SHG,
                        SHP = player.SHP,
                        GWG = player.GWG,
                        OTG = player.OTG,
                        SOG = player.SOG,
                        S_Perc = player.S_Perc,
                        TOI_GP = player.TOI_GP.ToString(),
                        Shifts_GP = player.Shifts_GP,
                        FOW_Perc = player.FOW_Perc
                    };
                })
                .ToList();
         */

        private DataGridViewColumn CreateColumn(string colName)
        {
            /*if (!QueryManager.ValidProp(propName, typeof(Player)))
            {
                // handle in valid prop
            }

            dynamic? propTemp = PropManager.GetPropValue(player, propName);
            dynamic? prop;
            if (propTemp is List<string>)
                prop = HandleList(propTemp);
            else
                prop = propTemp;*/

            DataGridViewColumn column = new DataGridViewColumn();
            column.Name = colName;
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            /*cell.Value = prop;*/
            column.CellTemplate = cell;

            return column;
        }

        private string HandleList(List<string> list)
        {
            string value = "";
            for (int i = 0; i < list.Count; i++)
            {
                value += list[i];
                if (i + 1 != list.Count)
                    value += ", ";
            }
            return value;
        }
    }
}
