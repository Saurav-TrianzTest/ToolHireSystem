namespace ToolHireSystem
{
    partial class FrmUpdateSupply
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
            this.UpdateMenu = new System.Windows.Forms.MenuStrip();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TxtDescription = new System.Windows.Forms.TextBox();
            this.TxtSupplyId = new System.Windows.Forms.TextBox();
            this.grpSupply = new System.Windows.Forms.GroupBox();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.TxtSupplyType = new System.Windows.Forms.ComboBox();
            this.TxtPrice = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.ComboBox();
            this.grpSupplies = new System.Windows.Forms.GroupBox();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.UpdateMenu.SuspendLayout();
            this.grpSupply.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPrice)).BeginInit();
            this.grpSupplies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.SuspendLayout();
            // 
            // UpdateMenu
            // 
            this.UpdateMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit,
            this.backToolStripMenuItem});
            this.UpdateMenu.Location = new System.Drawing.Point(0, 0);
            this.UpdateMenu.Name = "UpdateMenu";
            this.UpdateMenu.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.UpdateMenu.Size = new System.Drawing.Size(542, 24);
            this.UpdateMenu.TabIndex = 0;
            this.UpdateMenu.Text = "menuStrip1";
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(37, 20);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.Menu_Click);
            // 
            // backToolStripMenuItem
            // 
            this.backToolStripMenuItem.Name = "backToolStripMenuItem";
            this.backToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.backToolStripMenuItem.Text = "Back";
            this.backToolStripMenuItem.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // TxtDescription
            // 
            this.TxtDescription.Location = new System.Drawing.Point(99, 84);
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(106, 20);
            this.TxtDescription.TabIndex = 6;
            // 
            // TxtSupplyId
            // 
            this.TxtSupplyId.Enabled = false;
            this.TxtSupplyId.Location = new System.Drawing.Point(99, 32);
            this.TxtSupplyId.Name = "TxtSupplyId";
            this.TxtSupplyId.Size = new System.Drawing.Size(106, 20);
            this.TxtSupplyId.TabIndex = 7;
            // 
            // grpSupply
            // 
            this.grpSupply.Controls.Add(this.UpdateButton);
            this.grpSupply.Controls.Add(this.TxtSupplyType);
            this.grpSupply.Controls.Add(this.TxtPrice);
            this.grpSupply.Controls.Add(this.label5);
            this.grpSupply.Controls.Add(this.label4);
            this.grpSupply.Controls.Add(this.label3);
            this.grpSupply.Controls.Add(this.label2);
            this.grpSupply.Controls.Add(this.TxtSupplyId);
            this.grpSupply.Controls.Add(this.TxtDescription);
            this.grpSupply.Location = new System.Drawing.Point(152, 269);
            this.grpSupply.Name = "grpSupply";
            this.grpSupply.Size = new System.Drawing.Size(259, 193);
            this.grpSupply.TabIndex = 10;
            this.grpSupply.TabStop = false;
            this.grpSupply.Text = "Update Supply details";
            this.grpSupply.Visible = false;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(79, 153);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(126, 34);
            this.UpdateButton.TabIndex = 18;
            this.UpdateButton.Text = "Update Supply";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click_1);
            // 
            // TxtSupplyType
            // 
            this.TxtSupplyType.FormattingEnabled = true;
            this.TxtSupplyType.Location = new System.Drawing.Point(101, 59);
            this.TxtSupplyType.Name = "TxtSupplyType";
            this.TxtSupplyType.Size = new System.Drawing.Size(103, 21);
            this.TxtSupplyType.TabIndex = 17;
            // 
            // TxtPrice
            // 
            this.TxtPrice.DecimalPlaces = 2;
            this.TxtPrice.Location = new System.Drawing.Point(99, 111);
            this.TxtPrice.Name = "TxtPrice";
            this.TxtPrice.Size = new System.Drawing.Size(105, 20);
            this.TxtPrice.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Price";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Supply Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Supply ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Search Type";
            // 
            // txtSearch
            // 
            this.txtSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtSearch.FormattingEnabled = true;
            this.txtSearch.Location = new System.Drawing.Point(233, 53);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(137, 21);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.SelectedIndexChanged += new System.EventHandler(this.TxtSearch_SelectedIndexChanged);
            // 
            // grpSupplies
            // 
            this.grpSupplies.Controls.Add(this.grdData);
            this.grpSupplies.Location = new System.Drawing.Point(12, 91);
            this.grpSupplies.Name = "grpSupplies";
            this.grpSupplies.Size = new System.Drawing.Size(514, 155);
            this.grpSupplies.TabIndex = 14;
            this.grpSupplies.TabStop = false;
            this.grpSupplies.Text = "Select Supply to Update";
            this.grpSupplies.Visible = false;
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Location = new System.Drawing.Point(41, 33);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.Size = new System.Drawing.Size(444, 92);
            this.grdData.TabIndex = 5;
            this.grdData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdData_CellClick);
            // 
            // frmUpdateSupply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 486);
            this.Controls.Add(this.grpSupplies);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpSupply);
            this.Controls.Add(this.UpdateMenu);
            this.MainMenuStrip = this.UpdateMenu;
            this.Name = "frmUpdateSupply";
            this.Text = "UpdateSupply";
            this.Load += new System.EventHandler(this.FrmUpdateSupply_Load);
            this.UpdateMenu.ResumeLayout(false);
            this.UpdateMenu.PerformLayout();
            this.grpSupply.ResumeLayout(false);
            this.grpSupply.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPrice)).EndInit();
            this.grpSupplies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip UpdateMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
        private System.Windows.Forms.TextBox TxtDescription;
        private System.Windows.Forms.TextBox TxtSupplyId;
        private System.Windows.Forms.GroupBox grpSupply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown TxtPrice;
        private System.Windows.Forms.ComboBox TxtSupplyType;
        private System.Windows.Forms.ComboBox txtSearch;
        private System.Windows.Forms.GroupBox grpSupplies;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.Button UpdateButton;
    }
}