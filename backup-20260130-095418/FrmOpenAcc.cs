using System;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class FrmOpenAcc : Form
    {
        readonly FrmMainMenu parent;
        public FrmOpenAcc()
        {
            InitializeComponent();
        }
        public FrmOpenAcc(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void FrmOpenAcc_Load(object sender, EventArgs e)
        {
            txtCustId.Text = Customer.GetNextCustId().ToString("00000");
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
        }

        private void BtnRegCust_Click(object sender, EventArgs e)
        {
            if (!Validator.Valname(txtFirstName.Text) || !Validator.ValLastName(txtLastName.Text) || !Validator.ValPhone(txtPhoneNumber.Text))
            {
                //  MessageBox.Show("Name must be Letters only!");
            }
            else
            {


                if (!Validator.ValEmail(txtEMail.Text))
                {
                    // MessageBox.Show("Invalid Email!");
                }

                else
                {


                    Customer newCustomer = new(Convert.ToInt32(txtCustId.Text), txtFirstName.Text, txtLastName.Text, txtEMail.Text, txtPhoneNumber.Text, Convert.ToDecimal(0), "A");
                    //insert stock object
                    newCustomer.RegCustomer();
                    //disp conf message
                    MessageBox.Show("Customer Account created successfully!");
                    txtCustId.Text = Customer.GetNextCustId().ToString("00000");
                    txtFirstName.Clear();
                    txtLastName.Clear();
                    txtEMail.Clear();
                    txtPhoneNumber.Clear();
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
