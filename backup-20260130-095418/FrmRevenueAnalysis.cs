using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmRevenueAnalysis : Form
    {
        readonly FrmMainMenu parent;
        public FrmRevenueAnalysis()
        {
            InitializeComponent();
        }

        public FrmRevenueAnalysis(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void FrmRevenueAnalysis_Load(object sender, EventArgs e)
        {
            chtDataChart.Visible = false;
            grpBoxLowest.Visible = false;
            grpBoxHighestMonth.Visible = false;
        }

        private void BtnChart_Click(object sender, EventArgs e)
        {


            string strSQL = "SELECT SUM(amount), MONTH(paydate) FROM payments WHERE (SELECT DATEPART(year, paydate)paydate) LIKE (SELECT DATEPART(year, CURRENT_TIMESTAMP)) GROUP BY MONTH(paydate)";

            DataTable dt = new();

            SqlConnection databaseConnection = new(DBConnect.oradb);
            SqlCommand command = new(strSQL, databaseConnection);
            SqlDataAdapter da = new(command);


            da.Fill(dt);
            databaseConnection.Close();

            string[] N = new string[dt.Rows.Count];
            decimal[] M = new decimal[dt.Rows.Count];


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                N[i] = GetMonth(Convert.ToInt32(dt.Rows[i][1]));
                M[i] = Convert.ToDecimal(dt.Rows[i][0]);
            }

            chtDataChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chtDataChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chtDataChart.Series[0].LegendText = "Revenue in 2019";
            chtDataChart.Series[0].Points.DataBindXY(N, M);
            chtDataChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "C";

            chtDataChart.Series[0].Label = "#VALY";

            chtDataChart.Titles.Add("Monthly Revenue");

            chtDataChart.Visible = true;
            btnChart.Enabled = false;

            //insert highest earning month name and lowest earning month name into their labels
            int j = 0;
            int sub = 0;
            int bigSub = 0;
            decimal smallest = M[j];
            decimal biggest = M[j];
            for (j = 0; j < dt.Rows.Count; j++)
            {
                if (M[j] < smallest)
                {
                    smallest = M[j];
                    sub = j;

                }
                if (M[j] > biggest)
                {
                    biggest = M[j];
                    bigSub = j;
                }
            }
            lblLowest.Text = N[sub];
            lblHighest.Text = N[bigSub];
            grpBoxLowest.Visible = true;
            grpBoxHighestMonth.Visible = true;
        }

        public static string GetMonth(int month)
        {
            switch (month)
            {
                case 1:
                    {
                        return "JAN";
                    }
                case 2:
                    {
                        return "FEB";
                    }
                case 3:
                    {
                        return "MAR";
                    }
                case 4:
                    {
                        return "APR";
                    }
                case 5:
                    {
                        return "MAY";
                    }
                case 6:
                    {
                        return "JUN";
                    }
                case 7:
                    {
                        return "JUL";
                    }
                case 8:
                    {
                        return "AUG";
                    }
                case 9:
                    {
                        return "SEP";
                    }
                case 10:
                    {
                        return "OCT";
                    }
                case 11:
                    {
                        return "NOV";
                    }
                case 12:
                    {
                        return "DEC";
                    }
                default: return "OTH";
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
