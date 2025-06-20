namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnShowTop100;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cmbQueries = new System.Windows.Forms.ComboBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.chartPerformance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.btnShowTop100 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbQueries
            // 
            this.cmbQueries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQueries.FormattingEnabled = true;
            this.cmbQueries.Location = new System.Drawing.Point(12, 12);
            this.cmbQueries.Name = "cmbQueries";
            this.cmbQueries.Size = new System.Drawing.Size(200, 24);
            this.cmbQueries.TabIndex = 0;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(218, 12);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(90, 24);
            this.btnAnalyze.TabIndex = 1;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(12, 42);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(400, 172);
            this.txtResults.TabIndex = 2;
            // 
            // chartPerformance
            // 
            chartArea2.Name = "ChartArea1";
            this.chartPerformance.ChartAreas.Add(chartArea2);
            this.chartPerformance.Location = new System.Drawing.Point(447, 47);
            this.chartPerformance.Name = "chartPerformance";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Execution Time (ms)";
            this.chartPerformance.Series.Add(series2);
            this.chartPerformance.Size = new System.Drawing.Size(336, 148);
            this.chartPerformance.TabIndex = 3;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(12, 250);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.RowTemplate.Height = 24;
            this.dgvResults.Size = new System.Drawing.Size(806, 230);
            this.dgvResults.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 221);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(400, 23);
            this.progressBar.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(418, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Row Count:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblRowCount
            // 
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Location = new System.Drawing.Point(504, 198);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(0, 16);
            this.lblRowCount.TabIndex = 7;
            // 
            // btnOptimize
            // 
            this.btnOptimize.Location = new System.Drawing.Point(314, 13);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(101, 24);
            this.btnOptimize.TabIndex = 8;
            this.btnOptimize.Text = "Optimize";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.btnOptimize_Click);
            // 
            // btnShowTop100
            // 
            this.btnShowTop100.Location = new System.Drawing.Point(421, 13);
            this.btnShowTop100.Name = "btnShowTop100";
            this.btnShowTop100.Size = new System.Drawing.Size(184, 24);
            this.btnShowTop100.TabIndex = 9;
            this.btnShowTop100.Text = "First 100 Rows Timer";
            this.btnShowTop100.UseVisualStyleBackColor = true;
            this.btnShowTop100.Click += new System.EventHandler(this.btnShowTop100_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 492);
            this.Controls.Add(this.btnShowTop100);
            this.Controls.Add(this.btnOptimize);
            this.Controls.Add(this.lblRowCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.chartPerformance);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.cmbQueries);
            this.Name = "Form1";
            this.Text = "SQL Query Performance Analyzer";
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbQueries;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformance;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Button btnOptimize;

    }
}