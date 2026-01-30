using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmMainMenu : Form
    {
        public FrmMainMenu()
        {
            InitializeComponent();
        }

        private void MnuAdd_Click(object sender, EventArgs e)
        {
            Hide();
            FrmAddSupply nextForm = new(this);
            nextForm.Show();
        }

        private void UpdateSupplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            FrmUpdateSupply nextForm = new(this);
            nextForm.Show();
        }

        private void MnuRemove_Click(object sender, EventArgs e)
        {
            Hide();
            FrmRemoveSupp nextForm = new(this);
            nextForm.Show();
        }

        private void FrmMainMenu_Load(object sender, EventArgs e)
        {

        }

        private void OpenAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            FrmOpenAcc nextForm = new(this);
            nextForm.Show();
        }

        private void FrmUpdateAccount_Click(object sender, EventArgs e)
        {
            Hide();
            FrmUpdateAcc nextForm = new(this);
            nextForm.Show();
        }

        private void FrmCloseAccount_Click(object sender, EventArgs e)
        {
            Hide();
            FrmCloseAcc nextForm = new(this);
            nextForm.Show();
        }

        private void BtnProceRent_Click(object sender, EventArgs e)
        {
            Hide();
            FrmProcessRental nextForm = new(this);
            nextForm.Show();
        }

        private void BtnReturnRental_Click(object sender, EventArgs e)
        {
            Hide();
            FrmReturnRental nextForm = new(this);
            nextForm.Show();
        }



        private void BtnRentalPayment_Click_1(object sender, EventArgs e)
        {
            Hide();
            FrmRentalPayment nextForm = new(this);
            nextForm.Show();
        }

        private void FrmTypeAnal_Click(object sender, EventArgs e)
        {
            Hide();
            FrmRevenueAnalysis nextForm = new(this);
            nextForm.Show();
        }

        private void FrmRevAnal_Click(object sender, EventArgs e)
        {
            Hide();
            FrmTypeAnalysis nextForm = new(this);
            nextForm.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string hUsername = ComputeHash(txtUserName.Text, new SHA256CryptoServiceProvider());


            string hPassword = ComputeHash(txtPassword.Text, new SHA256CryptoServiceProvider());


            //USERNAME AND PASSWORD ARE 'manager' TO ACCES MANAGER ACCOUNT

            if (User.GetUserByUserName(hUsername, hPassword))
            {
                mnuSupplies.Enabled = true;
                mnuCustomers.Enabled = true;
                mnuRentals.Enabled = true;
                mnuAdmin.Enabled = true;
                txtPassword.Visible = false;
                txtUserName.Enabled = false;
                label2.Visible = false;
                btnLogin.Visible = false;
                btnLogout.Visible = true;

            }
            else
            {
                MessageBox.Show("Bad username password combination");
                txtUserName.Clear();
                txtPassword.Clear();
                txtUserName.Focus();
            }

            //next check if the username and password are saved in a table Users.
            //This table should contain two columns - Username and password.
            //The Username and Password would have been collected by hte "Register User" function and put throught the ComputeHash() algorithm before saving.

        }
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            btnLogout.Visible = false;
            btnLogin.Visible = true;
            mnuSupplies.Enabled = false;
            mnuAdmin.Enabled = false;
            txtUserName.Enabled = true;
            txtPassword.Visible = true;
            txtUserName.Clear();
            txtPassword.Clear();
            label2.Visible = true;

        }
    }
}

