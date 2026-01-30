using System;
using System.Data;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmCloseAcc : Form
    {
        readonly FrmMainMenu parent;
        public FrmCloseAcc()
        {
            InitializeComponent();
        }
        public FrmCloseAcc(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void FrmCloseAcc_Load(object sender, EventArgs e)
        {
            BtnCloseCust.Hide();
        }

        private void MnuBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new();
            grdData.DataSource = Customer.GetCustomerByLastName(ds, txtSearch.Text).Tables["cst"];
            grdData.Columns[0].Visible = false;
            grdData.Columns[5].Visible = false;

        }



        private void MnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void TxtSearch_TextChanged_1(object sender, EventArgs e)
        {
            DataSet ds = new();
            grdData.DataSource = Customer.GetCustomerByLastName(ds, txtSearch.Text).Tables["cst"];
            grdData.Columns[0].Visible = false;
            grdData.Columns[5].Visible = false;
        }

        private void GrdData_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            BtnCloseCust.Show();


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

        private void BtnCloseCust_Click_1(object sender, EventArgs e)
        {
            if (Customer.GetBalance(Convert.ToInt32(txtCustId.Text)) > 0)
            {
                MessageBox.Show("Outstanding debt on account!");
            }
            else
            {
                Customer.CloseCustomer(Convert.ToInt32(txtCustId.Text));
                MessageBox.Show("Account closed successfully!");

            }

            BtnCloseCust.Hide();
        }
    }
}
