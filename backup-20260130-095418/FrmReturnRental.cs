using System;
using System.Data;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmReturnRental : Form
    {
        readonly FrmMainMenu parent;
        private decimal totCst;
        private decimal cost;
        public FrmReturnRental()
        {
            InitializeComponent();
        }
        public FrmReturnRental(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            txtCustId.Clear();
            txtEMail.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhoneNumber.Clear();
            grdData.Enabled = true;
            DataSet ds = new();
            grdData.DataSource = Customer.GetCustomerByLastName(ds, txtSearch.Text.ToLower()).Tables["cst"];
            grdData.Columns[4].Visible = false;
            grdData.Columns[3].Visible = false;

        }

        private void GrdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //  grdData.Columns["ACCOUNT_STATUS"].Visible = false;
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

            DataSet ds = new();
            grdDataRentalItems.DataSource = RentalItems.GetRentalItemByCustId(ds, txtCustId.Text).Tables["item"];
            grdDataRentalItems.Columns[2].Visible = false;
            grdDataRentalItems.Columns[5].Visible = false;


        }

        private void GrdDataRentalItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                btnInvoice.Enabled = true;
                DateTime end = (DateTime)grdDataRentalItems.Rows[grdDataRentalItems.CurrentCell.RowIndex].Cells[4].Value;
                DateTime nowDate = DateTime.Now.Date;

                int daydiff = (int)((nowDate - end).TotalDays);
                string strCost = grdDataRentalItems.Rows[grdDataRentalItems.CurrentCell.RowIndex].Cells[5].Value + string.Empty;

                //  MessageBox.Show(strCost);
                cost = Convert.ToDecimal(strCost);
                int i;

                for (i = 0; i < daydiff; i++)
                {
                    cost += 20;
                }
                totCst += cost;
                txtTotalCost.Text = "" + totCst;




                Customer newCust = new(Convert.ToInt32(txtCustId.Text), txtFirstName.Text, txtLastName.Text, txtEMail.Text, txtPhoneNumber.Text, Customer.GetBalance(Convert.ToInt32(txtCustId.Text)), "A");
                //insert customer object
                //  newCust.updateCustomerBalance(cost);
                RentalItems.ReturnRentalItem(Convert.ToInt32(grdDataRentalItems.Rows[grdDataRentalItems.CurrentCell.RowIndex].Cells[0].Value));




                //insert stock object
                Supply.UndoRemoveSupp(Convert.ToInt32(grdDataRentalItems.Rows[grdDataRentalItems.CurrentCell.RowIndex].Cells[2].Value));
                DataSet ds = new();
                grdDataRentalItems.DataSource = RentalItems.GetRentalItemByCustId(ds, txtCustId.Text).Tables["item"];






            }
            catch
            {
                MessageBox.Show("Return failed");
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
        }

        private void BtnInvoice_Click(object sender, EventArgs e)
        {
            //  string rentId = grdDataRentalItems.Rows[grdDataRentalItems.CurrentCell.RowIndex].Cells[0].Value + string.Empty;
            //  int rentalItemsId = Convert.ToInt32(rentId);


            DateTime now = DateTime.Now.Date;
            int transId = Invoice.GetNextTransId();
            try
            {
                string status = "outstanding";
                Invoice newInvoice = new(transId, Convert.ToInt32(txtCustId.Text), now, Convert.ToDecimal(txtTotalCost.Text), status);
                newInvoice.RegInvoice();
                MessageBox.Show("Invoice created");
                btnInvoice.Enabled = false;
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
            totCst = 0;
            txtTotalCost.Text = "";

            // grdData.DataSource = null;

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmReturnRental_Load(object sender, EventArgs e)
        {

        }

        private void GrdDataRentalItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
