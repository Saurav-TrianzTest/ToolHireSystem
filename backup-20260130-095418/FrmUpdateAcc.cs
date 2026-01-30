using System;
using System.Data;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmUpdateAcc : Form
    {
        readonly FrmMainMenu parent;
        public FrmUpdateAcc()
        {
            InitializeComponent();
        }
        public FrmUpdateAcc(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
        }

        private void FrmUpdateAcc_Load(object sender, EventArgs e)
        {
            btnUpdateCustomer.Hide();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new();
            grdData.DataSource = Customer.GetCustomerByLastName(ds, txtSearch.Text).Tables["cst"];
            grdData.Columns[0].Visible = false;
            grdData.Columns[5].Visible = false;
        }



        private void BtnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (Validator.Valname(txtFirstName.Text) && Validator.ValLastName(txtLastName.Text) && Validator.ValUpdateEmail(txtCustId.Text, txtEMail.Text) && Validator.ValUpdatePhone(txtCustId.Text, txtPhoneNumber.Text))
            {
                Customer newCust = new(Convert.ToInt32(txtCustId.Text), txtFirstName.Text, txtLastName.Text, txtEMail.Text, txtPhoneNumber.Text, 0, "A");
                //insert customer object
                newCust.UpdateCustomer();
                //disp conf message
                MessageBox.Show("Customer ALTERED Success!");
                txtCustId.Text = Customer.GetNextCustId().ToString("00000");

                txtFirstName.Clear();
                txtLastName.Clear();
                txtEMail.Clear();
                txtPhoneNumber.Clear();
                txtSearch.Clear();

                btnUpdateCustomer.Hide();
            }
        }



        private void GrdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdateCustomer.Show();


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

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
