using System;
using System.Windows.Forms;


namespace ToolHireSystem
{
    public partial class FrmRevAnal : Form
    {
        readonly FrmMainMenu parent;
        public FrmRevAnal()
        {
            InitializeComponent();
        }
        public FrmRevAnal(FrmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            parent.Visible = true;
        }
    }
}
