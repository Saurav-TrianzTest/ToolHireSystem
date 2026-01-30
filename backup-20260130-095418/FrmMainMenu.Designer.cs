namespace ToolHireSystem
{
    partial class FrmMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainMenu));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuSupplies = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.frmOpenAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.frmUpdateAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.frmCloseAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRentals = new System.Windows.Forms.ToolStripMenuItem();
            this.btnProceRent = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReturnRental = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRentalPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.frmTypeAnal = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTypeAnal = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.grpBoxLogin = new System.Windows.Forms.GroupBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.mnuMain.SuspendLayout();
            this.grpBoxLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSupplies,
            this.mnuCustomers,
            this.mnuRentals,
            this.mnuAdmin,
            this.btnExit});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(758, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuSupplies
            // 
            this.mnuSupplies.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAdd,
            this.mnuUpdate,
            this.mnuRemove});
            this.mnuSupplies.Enabled = false;
            this.mnuSupplies.Name = "mnuSupplies";
            this.mnuSupplies.Size = new System.Drawing.Size(63, 20);
            this.mnuSupplies.Text = "Supplies";
            // 
            // mnuAdd
            // 
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.Size = new System.Drawing.Size(156, 22);
            this.mnuAdd.Text = "Add Supply";
            this.mnuAdd.Click += new System.EventHandler(this.MnuAdd_Click);
            // 
            // mnuUpdate
            // 
            this.mnuUpdate.Name = "mnuUpdate";
            this.mnuUpdate.Size = new System.Drawing.Size(156, 22);
            this.mnuUpdate.Text = "Update Supply";
            this.mnuUpdate.Click += new System.EventHandler(this.UpdateSupplyToolStripMenuItem_Click);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Size = new System.Drawing.Size(156, 22);
            this.mnuRemove.Text = "Remove Supply";
            this.mnuRemove.Click += new System.EventHandler(this.MnuRemove_Click);
            // 
            // mnuCustomers
            // 
            this.mnuCustomers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frmOpenAccount,
            this.frmUpdateAccount,
            this.frmCloseAccount});
            this.mnuCustomers.Name = "mnuCustomers";
            this.mnuCustomers.Size = new System.Drawing.Size(76, 20);
            this.mnuCustomers.Text = "Customers";
            // 
            // frmOpenAccount
            // 
            this.frmOpenAccount.Name = "frmOpenAccount";
            this.frmOpenAccount.Size = new System.Drawing.Size(198, 22);
            this.frmOpenAccount.Text = "Open Account";
            this.frmOpenAccount.Click += new System.EventHandler(this.OpenAccountToolStripMenuItem_Click);
            // 
            // frmUpdateAccount
            // 
            this.frmUpdateAccount.Name = "frmUpdateAccount";
            this.frmUpdateAccount.Size = new System.Drawing.Size(198, 22);
            this.frmUpdateAccount.Text = "Update Account Details";
            this.frmUpdateAccount.Click += new System.EventHandler(this.FrmUpdateAccount_Click);
            // 
            // frmCloseAccount
            // 
            this.frmCloseAccount.Name = "frmCloseAccount";
            this.frmCloseAccount.Size = new System.Drawing.Size(198, 22);
            this.frmCloseAccount.Text = "Close Account";
            this.frmCloseAccount.Click += new System.EventHandler(this.FrmCloseAccount_Click);
            // 
            // mnuRentals
            // 
            this.mnuRentals.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnProceRent,
            this.btnReturnRental,
            this.btnRentalPayment});
            this.mnuRentals.Name = "mnuRentals";
            this.mnuRentals.Size = new System.Drawing.Size(57, 20);
            this.mnuRentals.Text = "Rentals";
            // 
            // btnProceRent
            // 
            this.btnProceRent.Name = "btnProceRent";
            this.btnProceRent.Size = new System.Drawing.Size(157, 22);
            this.btnProceRent.Text = "Process Rental";
            this.btnProceRent.Click += new System.EventHandler(this.BtnProceRent_Click);
            // 
            // btnReturnRental
            // 
            this.btnReturnRental.Name = "btnReturnRental";
            this.btnReturnRental.Size = new System.Drawing.Size(157, 22);
            this.btnReturnRental.Text = "Return Rental";
            this.btnReturnRental.Click += new System.EventHandler(this.BtnReturnRental_Click);
            // 
            // btnRentalPayment
            // 
            this.btnRentalPayment.Name = "btnRentalPayment";
            this.btnRentalPayment.Size = new System.Drawing.Size(157, 22);
            this.btnRentalPayment.Text = "Rental Payment";
            this.btnRentalPayment.Click += new System.EventHandler(this.BtnRentalPayment_Click_1);
            // 
            // mnuAdmin
            // 
            this.mnuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frmTypeAnal,
            this.btnTypeAnal});
            this.mnuAdmin.Enabled = false;
            this.mnuAdmin.Name = "mnuAdmin";
            this.mnuAdmin.Size = new System.Drawing.Size(55, 20);
            this.mnuAdmin.Text = "Admin";
            // 
            // frmTypeAnal
            // 
            this.frmTypeAnal.Name = "frmTypeAnal";
            this.frmTypeAnal.Size = new System.Drawing.Size(165, 22);
            this.frmTypeAnal.Text = "Revenue Analysis";
            this.frmTypeAnal.Click += new System.EventHandler(this.FrmTypeAnal_Click);
            // 
            // btnTypeAnal
            // 
            this.btnTypeAnal.Name = "btnTypeAnal";
            this.btnTypeAnal.Size = new System.Drawing.Size(165, 22);
            this.btnTypeAnal.Text = "Type Analysis";
            this.btnTypeAnal.Click += new System.EventHandler(this.FrmRevAnal_Click);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(37, 20);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // grpBoxLogin
            // 
            this.grpBoxLogin.Controls.Add(this.btnLogout);
            this.grpBoxLogin.Controls.Add(this.btnLogin);
            this.grpBoxLogin.Controls.Add(this.label2);
            this.grpBoxLogin.Controls.Add(this.label1);
            this.grpBoxLogin.Controls.Add(this.txtPassword);
            this.grpBoxLogin.Controls.Add(this.txtUserName);
            this.grpBoxLogin.Location = new System.Drawing.Point(205, 104);
            this.grpBoxLogin.Name = "grpBoxLogin";
            this.grpBoxLogin.Size = new System.Drawing.Size(327, 182);
            this.grpBoxLogin.TabIndex = 1;
            this.grpBoxLogin.TabStop = false;
            this.grpBoxLogin.Text = "Login for Admin authorization";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(140, 123);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(94, 26);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(113, 81);
            this.txtPassword.MaxLength = 7;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(153, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(113, 47);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(153, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(140, 123);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(94, 26);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Visible = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(758, 368);
            this.Controls.Add(this.grpBoxLogin);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMainMenu";
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.FrmMainMenu_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.grpBoxLogin.ResumeLayout(false);
            this.grpBoxLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuSupplies;
        private System.Windows.Forms.ToolStripMenuItem mnuAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdate;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomers;
        private System.Windows.Forms.ToolStripMenuItem mnuRentals;
        private System.Windows.Forms.ToolStripMenuItem mnuAdmin;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.ToolStripMenuItem mnuRemove;
        private System.Windows.Forms.ToolStripMenuItem frmOpenAccount;
        private System.Windows.Forms.ToolStripMenuItem frmUpdateAccount;
        private System.Windows.Forms.ToolStripMenuItem frmCloseAccount;
        private System.Windows.Forms.ToolStripMenuItem btnProceRent;
        private System.Windows.Forms.ToolStripMenuItem btnReturnRental;
        private System.Windows.Forms.ToolStripMenuItem btnRentalPayment;
        private System.Windows.Forms.ToolStripMenuItem frmTypeAnal;
        private System.Windows.Forms.ToolStripMenuItem btnTypeAnal;
        private System.Windows.Forms.GroupBox grpBoxLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnLogout;
    }
}