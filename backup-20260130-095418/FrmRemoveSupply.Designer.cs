namespace ToolHireSystem
{
    partial class FrmRemoveSupp
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
            this.menuRemove = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBack = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveLabel = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.ComboBox();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuRemove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.SuspendLayout();
            // 
            // menuRemove
            // 
            this.menuRemove.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.btnBack});
            this.menuRemove.Location = new System.Drawing.Point(0, 0);
            this.menuRemove.Name = "menuRemove";
            this.menuRemove.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuRemove.Size = new System.Drawing.Size(370, 24);
            this.menuRemove.TabIndex = 0;
            this.menuRemove.Text = "RemoveMenu";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // btnBack
            // 
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(44, 20);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // RemoveLabel
            // 
            this.RemoveLabel.AutoSize = true;
            this.RemoveLabel.Location = new System.Drawing.Point(8, 33);
            this.RemoveLabel.Name = "RemoveLabel";
            this.RemoveLabel.Size = new System.Drawing.Size(140, 13);
            this.RemoveLabel.TabIndex = 1;
            this.RemoveLabel.Text = "Choose a Supply  to remove";
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(60, 249);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(204, 39);
            this.RemoveButton.TabIndex = 3;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtSearch.FormattingEnabled = true;
            this.txtSearch.Location = new System.Drawing.Point(154, 33);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(124, 21);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.SelectedIndexChanged += new System.EventHandler(this.TxtSearch_SelectedIndexChanged);
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Location = new System.Drawing.Point(12, 86);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.Size = new System.Drawing.Size(346, 87);
            this.grdData.TabIndex = 5;
            this.grdData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdData_CellClick);
 
            // 
            // txtStatus
            // 
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new System.Drawing.Point(171, 214);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(27, 20);
            this.txtStatus.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Supply Status";
            // 
            // frmRemoveSupp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 312);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.RemoveLabel);
            this.Controls.Add(this.menuRemove);
            this.MainMenuStrip = this.menuRemove;
            this.Name = "frmRemoveSupp";
            this.Text = "RemoveSupply";
            this.Load += new System.EventHandler(this.FrmRemoveSupp_Load);
            this.menuRemove.ResumeLayout(false);
            this.menuRemove.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnBack;
        private System.Windows.Forms.Label RemoveLabel;
        private System.Windows.Forms.MenuStrip menuRemove;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.ComboBox txtSearch;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label1;
    }
}