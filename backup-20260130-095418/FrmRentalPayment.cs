using System;
using System.Data;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmRentalPayment : Form
    {
        readonly FrmMainMenu parent;
        public FrmRentalPayment()
        {
            InitializeComponent();
        }
        public FrmRentalPayment(FrmMainMenu Parent)
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
            grdData.Columns[0].Visible = false;
            grdData.Columns[4].Visible = false;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
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

            DataSet ds = new();
            grdDataInvoice.DataSource = Invoice.GetInvoiceByCustId(ds, txtCustId.Text).Tables["invoice"];
        }

        private void GrdDataInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnPay.Enabled = true;
            totalText.Text = grdDataInvoice.Rows[grdDataInvoice.CurrentCell.RowIndex].Cells[3].Value + string.Empty;
        }

        private void FrmRentalPayment_Load(object sender, EventArgs e)
        {
            btnPay.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string id = grdDataInvoice.Rows[grdDataInvoice.CurrentCell.RowIndex].Cells[0].Value + string.Empty;
            string custId = grdDataInvoice.Rows[grdDataInvoice.CurrentCell.RowIndex].Cells[1].Value + string.Empty;
            string transDate = grdDataInvoice.Rows[grdDataInvoice.CurrentCell.RowIndex].Cells[2].Value + string.Empty;
            DateTime date = Convert.ToDateTime(transDate);
            string amount = grdDataInvoice.Rows[grdDataInvoice.CurrentCell.RowIndex].Cells[3].Value + string.Empty;
            //string status = grdDataInvoice.Rows[grdDataInvoice.CurrentCell.RowIndex].Cells[4].Value + string.Empty;

            Invoice newInvoice = new(Convert.ToInt32(id), Convert.ToInt32(custId), date, Convert.ToDecimal(amount), "paid");
            newInvoice.PayInvoice(Convert.ToInt32(txtCustId.Text), Convert.ToDecimal(amount));
            MessageBox.Show("Invoice Paid");

            Payment newPayment = new(Payment.GetNextPaymentId(), Convert.ToInt32(id), date, Convert.ToDecimal(amount));
            newPayment.RegPayment();
            MessageBox.Show("Payment created in database");



            DataSet ds = new();
            grdDataInvoice.DataSource = Invoice.GetInvoiceByCustId(ds, txtCustId.Text).Tables["invoice"];
            btnPay.Enabled = false;
            totalText.ResetText();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
