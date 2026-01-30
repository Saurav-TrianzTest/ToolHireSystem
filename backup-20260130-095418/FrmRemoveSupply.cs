using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;


namespace ToolHireSystem
{

    public partial class FrmRemoveSupp : Form
    {
        readonly FrmMainMenu parent;
        public FrmRemoveSupp()
        {
            InitializeComponent();
        }
        public FrmRemoveSupp(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void FrmRemoveSupp_Load(object sender, EventArgs e)
        {
            RemoveButton.Hide();

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT supply_type from SupplyType";


            txtSearch.Items.Clear();
            SqlCommand command = new(strSQL, databaseConnection);
            SqlDataAdapter da = new(command);
            command.ExecuteNonQuery();

            DataTable dt = new();
            //SqlDataReader dr = command.ExecuteReader();
            da.Fill(dt);

            foreach (DataRow d in dt.Rows)
            {
                txtSearch.Items.Add(d["supply_type"].ToString());
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
            grdData.Columns[0].Visible = false;
            grdData.Columns[4].Visible = false;
        }




        private void RemoveButton_Click(object sender, EventArgs e)
        {
            string type = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[1].Value + string.Empty;
            string description = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[2].Value + string.Empty;
            string price = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[3].Value + string.Empty;


            Supply newSupply = new(Convert.ToInt32(grdData.Rows[grdData.CurrentCell.RowIndex].Cells[0].Value), type, description, Convert.ToDecimal(price), "U");
            //insert stock object
            newSupply.RemoveSupp();
            //disp conf message
            MessageBox.Show("Stock set to unavailable!");


            txtStatus.Clear();
            RemoveButton.Hide();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GrdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RemoveButton.Show();

            string description = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[4].Value + string.Empty;

            txtStatus.Text = description;
        }
    }
}
