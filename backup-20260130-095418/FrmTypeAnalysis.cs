using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmTypeAnalysis : Form
    {
        readonly FrmMainMenu parent;
        public FrmTypeAnalysis()
        {
            InitializeComponent();
        }
        public FrmTypeAnalysis(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
        }

        private void BtnChart_Click(object sender, EventArgs e)
        {
            string strSQL = "select description, count(*) as count FROM(SELECT supply.supply_id,supplyType.description FROM ((rentalItems inner JOIN supply ON RentalItems.supply_id = Supply.supply_id) inner join supplytype On supply.supply_type = supplytype.supply_type)) as s group by s.description order by count";

            DataTable dt = new();

            SqlConnection databaseConnection = new(DBConnect.oradb);
            SqlCommand command = new(strSQL, databaseConnection);
            SqlDataAdapter da = new(command);

            da.Fill(dt);
            databaseConnection.Close();

            int[] N = new int[dt.Rows.Count];
            string[] M = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                N[i] = Convert.ToInt32(dt.Rows[i][1]);
                M[i] = dt.Rows[i][0].ToString();
            }

            chtDataChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chtDataChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chtDataChart.Series[0].LegendText = "Rentals by type";
            chtDataChart.Series[0].Points.DataBindXY(M, N);
            chtDataChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "C";

            chtDataChart.Series[0].Label = "#VALY";

            chtDataChart.Titles.Add("Type Analysis");

            chtDataChart.Visible = true;
            btnChart.Enabled = false;

            int j = 0;
            int sub = 0;
            int bigSub = 0;
            decimal smallest = N[j];
            decimal biggest = N[j];
            for (j = 0; j < dt.Rows.Count; j++)
            {
                if (N[j] < smallest)
                {
                    smallest = N[j];
                    sub = j;

                }
                if (N[j] > biggest)
                {
                    biggest = N[j];
                    bigSub = j;
                }
            }
            lblLowest.Text = M[sub];
            lblHighest.Text = M[bigSub];
            grpBoxLowest.Visible = true;
            grpBoxHighest.Visible = true;
        }

        private void FrmTypeAnalysis_Load(object sender, EventArgs e)
        {
            chtDataChart.Visible = false;
            grpBoxLowest.Visible = false;
            grpBoxHighest.Visible = false;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChtDataChart_Click(object sender, EventArgs e)
        {

        }
    }
}
