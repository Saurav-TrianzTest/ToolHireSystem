using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmUpdateSupply : Form
    {
        readonly FrmMainMenu parent;
        public FrmUpdateSupply()
        {
            InitializeComponent();
        }
        public FrmUpdateSupply(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmUpdateSupply_Load(object sender, EventArgs e)
        {
            UpdateButton.Hide();


            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT supply_type from SupplyType";


            TxtSupplyType.Items.Clear();
            SqlCommand command = new(strSQL, databaseConnection);
            SqlDataAdapter da = new(command);
            command.ExecuteNonQuery();

            DataTable dt = new();
            da.Fill(dt);

            foreach (DataRow d in dt.Rows)
            {
                txtSearch.Items.Add(d["supply_type"].ToString());
                TxtSupplyType.Items.Add(d["supply_type"].ToString());
            }
            databaseConnection.Close();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
        }

        private void TxtSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new();
            grdData.DataSource = Supply.GetSuppType(ds, txtSearch.Text.ToUpper()).Tables["stk"];
            grdData.Columns[4].Visible = false;
            grpSupplies.Visible = true;
        }

        private void GrdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            string id = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[0].Value + string.Empty;
            string type = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[1].Value + string.Empty;
            string description = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[2].Value + string.Empty;
            string price = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[3].Value + string.Empty;


            TxtSupplyId.Text = id;
            TxtSupplyType.Text = type;
            TxtDescription.Text = description;
            TxtPrice.Value = Convert.ToDecimal(price);
            UpdateButton.Visible = true;
            grpSupply.Visible = true;


        }

        private void UpdateButton_Click_1(object sender, EventArgs e)
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

                    Supply newSupply = new(Convert.ToInt32(TxtSupplyId.Text), TxtSupplyType.Text, TxtDescription.Text, TxtPrice.Value, "A");
                    //insert stock object
                    newSupply.UpdateSupply();
                    //disp conf message
                    MessageBox.Show("Stock ALTERED Success!");
                    TxtSupplyId.Text = Supply.GetNextStockNo().ToString("00000");

                    TxtDescription.Clear();
                    TxtPrice.Value = 0;

                    UpdateButton.Hide();
                }
            }
        }
    }
}
