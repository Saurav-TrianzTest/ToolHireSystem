namespace ToolHireSystem
{
    partial class FrmTypeAnalysis
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnBack = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.chtDataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnChart = new System.Windows.Forms.Button();
            this.grpBoxHighest = new System.Windows.Forms.GroupBox();
            this.lblHighest = new System.Windows.Forms.Label();
            this.grpBoxLowest = new System.Windows.Forms.GroupBox();
            this.lblLowest = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtDataChart)).BeginInit();
            this.grpBoxHighest.SuspendLayout();
            this.grpBoxLowest.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBack,
            this.btnExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 3;
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
            // chtDataChart
            // 
            chartArea1.Name = "ChartArea1";
            this.chtDataChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chtDataChart.Legends.Add(legend1);
            this.chtDataChart.Location = new System.Drawing.Point(32, 142);
            this.chtDataChart.Name = "chtDataChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chtDataChart.Series.Add(series1);
            this.chtDataChart.Size = new System.Drawing.Size(740, 318);
            this.chtDataChart.TabIndex = 4;
            this.chtDataChart.Text = "chart1";
            this.chtDataChart.Click += new System.EventHandler(this.ChtDataChart_Click);
            // 
            // btnChart
            // 
            this.btnChart.Location = new System.Drawing.Point(339, 27);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(104, 32);
            this.btnChart.TabIndex = 5;
            this.btnChart.Text = "Type Analysis";
            this.btnChart.UseVisualStyleBackColor = true;
            this.btnChart.Click += new System.EventHandler(this.BtnChart_Click);
            // 
            // grpBoxHighest
            // 
            this.grpBoxHighest.Controls.Add(this.lblHighest);
            this.grpBoxHighest.Location = new System.Drawing.Point(32, 27);
            this.grpBoxHighest.Name = "grpBoxHighest";
            this.grpBoxHighest.Size = new System.Drawing.Size(214, 100);
            this.grpBoxHighest.TabIndex = 6;
            this.grpBoxHighest.TabStop = false;
            this.grpBoxHighest.Text = "Most in demand";
            // 
            // lblHighest
            // 
            this.lblHighest.AutoSize = true;
            this.lblHighest.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighest.Location = new System.Drawing.Point(62, 34);
            this.lblHighest.Name = "lblHighest";
            this.lblHighest.Size = new System.Drawing.Size(86, 31);
            this.lblHighest.TabIndex = 0;
            this.lblHighest.Text = "label1";
            // 
            // grpBoxLowest
            // 
            this.grpBoxLowest.Controls.Add(this.lblLowest);
            this.grpBoxLowest.Location = new System.Drawing.Point(535, 27);
            this.grpBoxLowest.Name = "grpBoxLowest";
            this.grpBoxLowest.Size = new System.Drawing.Size(237, 100);
            this.grpBoxLowest.TabIndex = 7;
            this.grpBoxLowest.TabStop = false;
            this.grpBoxLowest.Text = "Least in Demand";
            // 
            // lblLowest
            // 
            this.lblLowest.AutoSize = true;
            this.lblLowest.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowest.Location = new System.Drawing.Point(70, 34);
            this.lblLowest.Name = "lblLowest";
            this.lblLowest.Size = new System.Drawing.Size(86, 31);
            this.lblLowest.TabIndex = 0;
            this.lblLowest.Text = "label2";
            // 
            // frmTypeAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 491);
            this.Controls.Add(this.grpBoxLowest);
            this.Controls.Add(this.grpBoxHighest);
            this.Controls.Add(this.btnChart);
            this.Controls.Add(this.chtDataChart);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmTypeAnalysis";
            this.Text = "Type Analysis";
            this.Load += new System.EventHandler(this.FrmTypeAnalysis_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtDataChart)).EndInit();
            this.grpBoxHighest.ResumeLayout(false);
            this.grpBoxHighest.PerformLayout();
            this.grpBoxLowest.ResumeLayout(false);
            this.grpBoxLowest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnBack;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtDataChart;
        private System.Windows.Forms.Button btnChart;
        private System.Windows.Forms.GroupBox grpBoxHighest;
        private System.Windows.Forms.Label lblHighest;
        private System.Windows.Forms.GroupBox grpBoxLowest;
        private System.Windows.Forms.Label lblLowest;
    }
}