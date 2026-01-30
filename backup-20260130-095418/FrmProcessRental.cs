using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmProcessRental : Form
    {
        readonly FrmMainMenu parent;
        private decimal totalCost;

        public FrmProcessRental()
        {
            InitializeComponent();
        }


        public FrmProcessRental(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;

        }


        private void FrmProcessRental_Load(object sender, EventArgs e)
        {
            daysRented.Enabled = false;
        }


        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
        }


        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new();
            grdData.DataSource = Customer.GetCustomerByLastName(ds, txtSearch.Text.ToLower()).Tables["cst"];
        }


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new();
            grdDataSupply.DataSource = Supply.GetSuppType(ds, txtType.Text.ToUpper()).Tables["stk"];
        }

        private void GrdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[0].Value + string.Empty;
            string firstName = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[1].Value + string.Empty;
            string lastName = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[2].Value + string.Empty;
            string eMail = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[3].Value + string.Empty;
            string phone = grdData.Rows[grdData.CurrentCell.RowIndex].Cells[4].Value + string.Empty;

            txtCustId.Text = id;
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtEMail.Text = eMail;
            txtPhoneNumber.Text = phone;

            grdData.Enabled = false;

            SqlConnection databaseConnection = new(DBConnect.oradb);
            databaseConnection.Open();

            string strSQL = "SELECT supply_type from SupplyType";

            txtType.Items.Clear();
            SqlCommand command = new(strSQL, databaseConnection);
            SqlDataAdapter da = new(command);
            command.ExecuteNonQuery();

            DataTable dt = new();
            da.Fill(dt);

            foreach (DataRow d in dt.Rows)
            {
                txtType.Items.Add(d["supply_type"].ToString());
            }
            databaseConnection.Close();
        }

        private void GrdDataSupply_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            daysRented.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (daysRented.Value <= 0)
            {
                MessageBox.Show("Please enter an amount of days to rent supply!");
            }
            else
            {
                try
                {
                    string id = grdDataSupply.Rows[grdDataSupply.CurrentCell.RowIndex].Cells[0].Value + string.Empty;
                    string type = grdDataSupply.Rows[grdDataSupply.CurrentCell.RowIndex].Cells[1].Value + string.Empty;
                    string description = grdDataSupply.Rows[grdDataSupply.CurrentCell.RowIndex].Cells[2].Value + string.Empty;
                    string price = grdDataSupply.Rows[grdDataSupply.CurrentCell.RowIndex].Cells[3].Value + string.Empty;
                    string period = daysRented.Value + string.Empty;

                    DataSet ds = new();

                    DataTable dt = new("Basket");
                    dt.Columns.Add(new DataColumn("id", typeof(string)));
                    dt.Columns.Add(new DataColumn("type", typeof(string)));
                    dt.Columns.Add(new DataColumn("description", typeof(string)));
                    dt.Columns.Add(new DataColumn("price", typeof(string)));
                    dt.Columns.Add(new DataColumn("date_to", typeof(string)));

                    DataRow dr = dt.NewRow();

                    decimal supplyCost = Convert.ToDecimal(price) * Convert.ToInt32(period);

                    totalCost += supplyCost;
                    txtTotalCost.Text = Convert.ToString(totalCost);

                    dr["id"] = id;
                    dr["Type"] = type;
                    dr["Description"] = description;
                    dr["Price"] = supplyCost;
                    dr["date_to"] = period;
                    dt.Rows.Add(dr);

                    foreach (DataRow d in dt.Rows)
                    {
                        grdDataBasket.Rows.Add(d.ItemArray);
                    }
                    Supply newSupply = new(Convert.ToInt32(id), type, description, Convert.ToDecimal(price), "U");

                    newSupply.RemoveSupp();
                    MessageBox.Show("Stock set to unavailable!");
                    DataSet newds = new();
                    grdDataSupply.DataSource = Supply.GetSuppType(newds, txtType.Text.ToUpper()).Tables["stk"];

                }
                catch (Exception)
                {
                    MessageBox.Show("Error transaction failed");
                }
            }
        }


        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            using SqlConnection connection = new(DBConnect.oradb);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            command.Transaction = transaction;

            try
            {
                decimal updatedBalance = totalCost;

                Customer newCust = new(Convert.ToInt32(txtCustId.Text), txtFirstName.Text, txtLastName.Text, txtEMail.Text, txtPhoneNumber.Text, updatedBalance, "A");
                newCust.UpdateCustomerBalance(updatedBalance);

                int rentId = Rental.GetNextRentalId();

                Rental newRental = new(rentId, Convert.ToInt32(txtCustId.Text));
                newRental.RegRental();

                for (int i = 0; i < grdDataBasket.Rows.Count; i++)
                {
                    string id = grdDataBasket.Rows[i].Cells[0].Value + string.Empty;
                    string type = grdDataBasket.Rows[i].Cells[1].Value + string.Empty;
                    string description = grdDataBasket.Rows[i].Cells[2].Value + string.Empty;
                    string price = grdDataBasket.Rows[i].Cells[3].Value + string.Empty; ;
                    string period = grdDataBasket.Rows[i].Cells[4].Value + string.Empty;

                    RentalItems newRentalItem = new(RentalItems.GetNextRentalItemsId(), rentId, Convert.ToInt32(id), Convert.ToInt32(txtCustId.Text), Convert.ToInt32(period), Convert.ToDecimal(price), "A");
                    newRentalItem.RegRentalItems();
                }
                MessageBox.Show("Rental created successfully");

                grdDataSupply.DataSource = null;
                grdDataBasket.Rows.Clear();
                grdDataBasket.Refresh();
                txtCustId.Clear();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtEMail.Clear();
                txtPhoneNumber.Clear();
                txtSearch.ResetText();
                txtTotalCost.Clear();
                daysRented.Value = 0;
                grdData.DataSource = null;
                grdData.Enabled = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Transaction failed");
            }
        }

        private void GrdDataBasket_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = grdDataBasket.Rows[grdDataBasket.CurrentCell.RowIndex].Cells[0].Value + string.Empty;

            Supply.UndoRemoveSupp(Convert.ToInt32(id));
            MessageBox.Show("Stock set to Available!");

            totalCost = Convert.ToDecimal(grdDataBasket.Rows[grdDataBasket.CurrentCell.RowIndex].Cells[3].Value) - Convert.ToDecimal(grdDataBasket.Rows[grdDataBasket.CurrentCell.RowIndex].Cells[3].Value);
            txtTotalCost.Text = Convert.ToString(totalCost);
            grdDataBasket.Rows.RemoveAt(grdDataBasket.CurrentCell.RowIndex);

            DataSet newds = new();
            grdDataSupply.DataSource = Supply.GetSuppType(newds, txtType.Text.ToUpper()).Tables["stk"];

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
