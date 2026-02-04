using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmAddSupply : Form
    {
        readonly FrmMainMenu parent;
        public FrmAddSupply()
        {
            InitializeComponent();
        }
        public FrmAddSupply(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void AddSuppBtn_Click(object sender, EventArgs e)
        {

            if (TxtDescription.Text == "")
            {
                MessageBox.Show("Fields must not be empty!");
            }
            else
            {
                //Validate data
                if (Validator.ValPrice(TxtPrice.Value))
                {

                    //Instantiate stock object
                    Supply newSupply = new(Convert.ToInt32(TxtSupplyId.Text), TxtSupplyType.Text, TxtDescription.Text, TxtPrice.Value, "A");
                    //insert stock object
                    newSupply.RegSupply();
                    //disp conf message
                    MessageBox.Show("Stock Added Success!");
                    TxtSupplyId.Text = Supply.GetNextStockNo().ToString("00000");

                    TxtDescription.Clear();
                    TxtPrice.Value = 0;

                }

            }

        }

        private void ToolHireSystem_Load(object sender, EventArgs e)
        {
            TxtSupplyId.Text = Supply.GetNextStockNo().ToString("00000");

            using SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT supply_type FROM SupplyType";

            TxtSupplyType.Items.Clear();
            using SqlCommand command = new(strSQL, databaseConnection);
            using SqlDataAdapter da = new(command);

            DataTable dt = new();
            da.Fill(dt);

            foreach (DataRow d in dt.Rows)
            {
                TxtSupplyType.Items.Add(d["supply_type"].ToString());
            }
        }

        private void
            BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;

        }


    }
}
