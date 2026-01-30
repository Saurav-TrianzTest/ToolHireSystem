namespace ToolHireSystem
{
    partial class FrmAddSupply
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBack = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AddSuppBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtSupplyId = new System.Windows.Forms.TextBox();
            this.TxtPrice = new System.Windows.Forms.NumericUpDown();
            this.TxtSupplyType = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu,
            this.btnBack});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(291, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "mnu";
            // 
            // Menu
            // 
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(37, 20);
            this.Menu.Text = "Exit";
            this.Menu.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // btnBack
            // 
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(44, 20);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description";
            // 
            // TxtDescription
            // 
            this.TxtDescription.Location = new System.Drawing.Point(81, 84);
            this.TxtDescription.MaxLength = 50;
            this.TxtDescription.Name = "TxtDescription";
            this.TxtDescription.Size = new System.Drawing.Size(146, 20);
            this.TxtDescription.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Price";
            // 
            // AddSuppBtn
            // 
            this.AddSuppBtn.Location = new System.Drawing.Point(44, 150);
            this.AddSuppBtn.Name = "AddSuppBtn";
            this.AddSuppBtn.Size = new System.Drawing.Size(203, 60);
            this.AddSuppBtn.TabIndex = 9;
            this.AddSuppBtn.Text = "Add Supply";
            this.AddSuppBtn.UseVisualStyleBackColor = true;
            this.AddSuppBtn.Click += new System.EventHandler(this.AddSuppBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Supply ID";
            // 
            // TxtSupplyId
            // 
            this.TxtSupplyId.Enabled = false;
            this.TxtSupplyId.Location = new System.Drawing.Point(81, 32);
            this.TxtSupplyId.Name = "TxtSupplyId";
            this.TxtSupplyId.Size = new System.Drawing.Size(146, 20);
            this.TxtSupplyId.TabIndex = 11;
            // 
            // TxtPrice
            // 
            this.TxtPrice.DecimalPlaces = 2;
            this.TxtPrice.Location = new System.Drawing.Point(81, 110);
            this.TxtPrice.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
            this.TxtPrice.Name = "TxtPrice";
            this.TxtPrice.Size = new System.Drawing.Size(146, 20);
            this.TxtPrice.TabIndex = 12;
            // 
            // TxtSupplyType
            // 
            this.TxtSupplyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TxtSupplyType.FormattingEnabled = true;
            this.TxtSupplyType.Location = new System.Drawing.Point(81, 58);
            this.TxtSupplyType.Name = "TxtSupplyType";
            this.TxtSupplyType.Size = new System.Drawing.Size(146, 21);
            this.TxtSupplyType.TabIndex = 14;
            // 
            // frmAddSupply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 237);
            this.Controls.Add(this.TxtSupplyType);
            this.Controls.Add(this.TxtPrice);
            this.Controls.Add(this.TxtSupplyId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AddSuppBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmAddSupply";
            this.Text = "Add Supply";
            this.Load += new System.EventHandler(this.ToolHireSystem_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private new System.Windows.Forms.ToolStripMenuItem Menu;
        private System.Windows.Forms.ToolStripMenuItem btnBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button AddSuppBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtSupplyId;
        private System.Windows.Forms.NumericUpDown TxtPrice;
        private System.Windows.Forms.ComboBox TxtSupplyType;
    }
}

