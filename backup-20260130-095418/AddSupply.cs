using System;
using System.Windows.Forms;

namespace ToolHireSystem
{
    public partial class WeldSys : Form
    {
        public WeldSys()
        {
            InitializeComponent();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void AddSuppBtn_Click(object sender, EventArgs e)
        {
            if (SupplyType.Text == "")
            {
                MessageBox.Show("Fields must not be empty!");
            }
            else
            {
                MessageBox.Show("Stock Added Success!");
                SupplyType.Clear();
                Description.Clear();
                Price.Clear();
                Quantity.Clear();
                SupplyType.Focus();
            }

        }
    }
}
