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

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT supply_type from SupplyType";


            TxtSupplyType.Items.Clear();
            SqlCommand command = new(strSQL, databaseConnection);
            SqlDataAdapter da = new(command);
            command.ExecuteNonQuery();

            DataTable dt = new();
            //SqlDataReader dr = command.ExecuteReader();
            da.Fill(dt);

            foreach (DataRow d in dt.Rows)
            {
                TxtSupplyType.Items.Add(d["supply_type"].ToString());
            }
            databaseConnection.Close();
        }

        private void
            BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;

        }


    }
}
