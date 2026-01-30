namespace ToolHireSystem
{
    partial class FrmRevenueAnalysis
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chtDataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnChart = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnBack = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.grpBoxHighestMonth = new System.Windows.Forms.GroupBox();
            this.grpBoxLowest = new System.Windows.Forms.GroupBox();
            this.lblLowest = new System.Windows.Forms.Label();
            this.lblHighest = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chtDataChart)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.grpBoxHighestMonth.SuspendLayout();
            this.grpBoxLowest.SuspendLayout();
            this.SuspendLayout();
            // 
            // chtDataChart
            // 
            chartArea2.Name = "ChartArea1";
            this.chtDataChart.ChartAreas.Add(chartArea2);
            this.chtDataChart.DataSource = this.chtDataChart.Images;
            legend2.Name = "Legend1";
            this.chtDataChart.Legends.Add(legend2);
            this.chtDataChart.Location = new System.Drawing.Point(12, 188);
            this.chtDataChart.Name = "chtDataChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chtDataChart.Series.Add(series2);
            this.chtDataChart.Size = new System.Drawing.Size(862, 349);
            this.chtDataChart.TabIndex = 0;
            this.chtDataChart.Text = "chart1";
            // 
            // btnChart
            // 
            this.btnChart.Location = new System.Drawing.Point(374, 50);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(144, 32);
            this.btnChart.TabIndex = 1;
            this.btnChart.Text = "Revenue Analysis";
            this.btnChart.UseVisualStyleBackColor = true;
            this.btnChart.Click += new System.EventHandler(this.BtnChart_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBack,
            this.btnExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(893, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnBack
            // 
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(44, 20);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(37, 20);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // grpBoxHighestMonth
            // 
            this.grpBoxHighestMonth.Controls.Add(this.lblHighest);
            this.grpBoxHighestMonth.Location = new System.Drawing.Point(559, 50);
            this.grpBoxHighestMonth.Name = "grpBoxHighestMonth";
            this.grpBoxHighestMonth.Size = new System.Drawing.Size(315, 132);
            this.grpBoxHighestMonth.TabIndex = 3;
            this.grpBoxHighestMonth.TabStop = false;
            this.grpBoxHighestMonth.Text = "Highest Earning Month";
            // 
            // grpBoxLowest
            // 
            this.grpBoxLowest.Controls.Add(this.lblLowest);
            this.grpBoxLowest.Location = new System.Drawing.Point(12, 50);
            this.grpBoxLowest.Name = "grpBoxLowest";
            this.grpBoxLowest.Size = new System.Drawing.Size(309, 132);
            this.grpBoxLowest.TabIndex = 4;
            this.grpBoxLowest.TabStop = false;
            this.grpBoxLowest.Text = "Lowest Earning Month";
            // 
            // lblLowest
            // 
            this.lblLowest.AutoSize = true;
            this.lblLowest.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowest.Location = new System.Drawing.Point(99, 51);
            this.lblLowest.Name = "lblLowest";
            this.lblLowest.Size = new System.Drawing.Size(86, 31);
            this.lblLowest.TabIndex = 0;
            this.lblLowest.Text = "label1";
            // 
            // lblHighest
            // 
            this.lblHighest.AutoSize = true;
            this.lblHighest.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighest.Location = new System.Drawing.Point(118, 51);
            this.lblHighest.Name = "lblHighest";
            this.lblHighest.Size = new System.Drawing.Size(86, 31);
            this.lblHighest.TabIndex = 0;
            this.lblHighest.Text = "label1";
            // 
            // frmRevenueAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 549);
            this.Controls.Add(this.grpBoxLowest);
            this.Controls.Add(this.grpBoxHighestMonth);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnChart);
            this.Controls.Add(this.chtDataChart);
            this.Name = "frmRevenueAnalysis";
            this.Text = "Revenue Analysis";
            this.Load += new System.EventHandler(this.FrmRevenueAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chtDataChart)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpBoxHighestMonth.ResumeLayout(false);
            this.grpBoxHighestMonth.PerformLayout();
            this.grpBoxLowest.ResumeLayout(false);
            this.grpBoxLowest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chtDataChart;
        private System.Windows.Forms.Button btnChart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnBack;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.GroupBox grpBoxHighestMonth;
        private System.Windows.Forms.GroupBox grpBoxLowest;
        private System.Windows.Forms.Label lblLowest;
        private System.Windows.Forms.Label lblHighest;
    }
}