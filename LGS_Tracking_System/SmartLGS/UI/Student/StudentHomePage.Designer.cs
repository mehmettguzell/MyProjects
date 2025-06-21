using System.Windows.Forms;

namespace SmartLGS.student
{
    partial class StudentHomePage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.chartPerformance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblRecentExams = new System.Windows.Forms.Label();
            this.dgvRecentExams = new System.Windows.Forms.DataGridView();
            this.tabAddExam = new System.Windows.Forms.TabPage();
            this.btnSubmitExam = new System.Windows.Forms.Button();
            this.txtExamName = new System.Windows.Forms.TextBox();
            this.lblExamName = new System.Windows.Forms.Label();
            this.dtpExamDate = new System.Windows.Forms.DateTimePicker();
            this.lblExamDate = new System.Windows.Forms.Label();
            this.gbExamDetails = new System.Windows.Forms.GroupBox();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.btnImportReport = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentExams)).BeginInit();
            this.tabAddExam.SuspendLayout();
            this.tabReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SmartLGS Öğrenci Arayüzü";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblWelcome.Location = new System.Drawing.Point(20, 90);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(860, 30);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Hoşgeldin, [Student Name]";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDashboard);
            this.tabControl.Controls.Add(this.tabAddExam);
            this.tabControl.Controls.Add(this.tabReports);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tabControl.Location = new System.Drawing.Point(20, 130);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(860, 500);
            this.tabControl.TabIndex = 2;
            // 
            // tabDashboard
            // 
            this.tabDashboard.Controls.Add(this.chartPerformance);
            this.tabDashboard.Controls.Add(this.lblRecentExams);
            this.tabDashboard.Controls.Add(this.dgvRecentExams);
            this.tabDashboard.Location = new System.Drawing.Point(4, 25);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabDashboard.Size = new System.Drawing.Size(852, 471);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "Ana Sayfa";
            this.tabDashboard.UseVisualStyleBackColor = true;
            // 
            // chartPerformance
            // 
            this.chartPerformance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.chartPerformance.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.chartPerformance.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartPerformance.BorderlineWidth = 0;
            this.chartPerformance.Location = new System.Drawing.Point(20, 20);
            this.chartPerformance.Name = "chartPerformance";
            this.chartPerformance.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chartPerformance.Size = new System.Drawing.Size(812, 202);
            this.chartPerformance.TabIndex = 2;
            this.chartPerformance.Text = "Performance Chart";
            // 
            // lblRecentExams
            // 
            this.lblRecentExams.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblRecentExams.Location = new System.Drawing.Point(20, 240);
            this.lblRecentExams.Name = "lblRecentExams";
            this.lblRecentExams.Size = new System.Drawing.Size(812, 30);
            this.lblRecentExams.TabIndex = 1;
            this.lblRecentExams.Text = "Sınavlar ve Sonuçları:";
            this.lblRecentExams.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvRecentExams
            // 
            this.dgvRecentExams.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dgvRecentExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentExams.Location = new System.Drawing.Point(20, 280);
            this.dgvRecentExams.Name = "dgvRecentExams";
            this.dgvRecentExams.ReadOnly = true;
            this.dgvRecentExams.Size = new System.Drawing.Size(812, 180);
            this.dgvRecentExams.TabIndex = 0;
            // 
            // tabAddExam
            // 
            this.tabAddExam.Controls.Add(this.btnSubmitExam);
            this.tabAddExam.Controls.Add(this.txtExamName);
            this.tabAddExam.Controls.Add(this.lblExamName);
            this.tabAddExam.Controls.Add(this.dtpExamDate);
            this.tabAddExam.Controls.Add(this.lblExamDate);
            this.tabAddExam.Controls.Add(this.gbExamDetails);
            this.tabAddExam.Location = new System.Drawing.Point(4, 25);
            this.tabAddExam.Name = "tabAddExam";
            this.tabAddExam.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddExam.Size = new System.Drawing.Size(852, 471);
            this.tabAddExam.TabIndex = 1;
            this.tabAddExam.Text = "Sınav Ekle (Manual)";
            this.tabAddExam.UseVisualStyleBackColor = true;
            // 
            // btnSubmitExam
            // 
            this.btnSubmitExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSubmitExam.FlatAppearance.BorderSize = 0;
            this.btnSubmitExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnSubmitExam.ForeColor = System.Drawing.Color.White;
            this.btnSubmitExam.Location = new System.Drawing.Point(300, 420);
            this.btnSubmitExam.Name = "btnSubmitExam";
            this.btnSubmitExam.Size = new System.Drawing.Size(200, 40);
            this.btnSubmitExam.TabIndex = 4;
            this.btnSubmitExam.Text = "Kaydet";
            this.btnSubmitExam.UseVisualStyleBackColor = false;
            this.btnSubmitExam.Click += new System.EventHandler(this.btnSubmitExam_Click);
            // 
            // txtExamName
            // 
            this.txtExamName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtExamName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExamName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtExamName.Location = new System.Drawing.Point(300, 70);
            this.txtExamName.Name = "txtExamName";
            this.txtExamName.Size = new System.Drawing.Size(400, 23);
            this.txtExamName.TabIndex = 1;
            // 
            // lblExamName
            // 
            this.lblExamName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblExamName.Location = new System.Drawing.Point(150, 70);
            this.lblExamName.Name = "lblExamName";
            this.lblExamName.Size = new System.Drawing.Size(120, 25);
            this.lblExamName.TabIndex = 0;
            this.lblExamName.Text = "Sınav Adı:";
            this.lblExamName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpExamDate
            // 
            this.dtpExamDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpExamDate.Location = new System.Drawing.Point(300, 120);
            this.dtpExamDate.Name = "dtpExamDate";
            this.dtpExamDate.Size = new System.Drawing.Size(200, 23);
            this.dtpExamDate.TabIndex = 6;
            // 
            // lblExamDate
            // 
            this.lblExamDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblExamDate.Location = new System.Drawing.Point(150, 120);
            this.lblExamDate.Name = "lblExamDate";
            this.lblExamDate.Size = new System.Drawing.Size(120, 25);
            this.lblExamDate.TabIndex = 5;
            this.lblExamDate.Text = "Sınav Tarihi:";
            this.lblExamDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbExamDetails
            // 
            this.gbExamDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.gbExamDetails.Location = new System.Drawing.Point(100, 162);
            this.gbExamDetails.Name = "gbExamDetails";
            this.gbExamDetails.Size = new System.Drawing.Size(650, 248);
            this.gbExamDetails.TabIndex = 7;
            this.gbExamDetails.TabStop = false;
            this.gbExamDetails.Text = "Sınav Sonuçları               DOĞRU  YANLIŞ  BOŞ";
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.btnImportReport);
            this.tabReports.Location = new System.Drawing.Point(4, 25);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabReports.Size = new System.Drawing.Size(852, 471);
            this.tabReports.TabIndex = 2;
            this.tabReports.Text = "Sınav Ekle (pdf)";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // btnImportReport
            // 
            this.btnImportReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnImportReport.FlatAppearance.BorderSize = 0;
            this.btnImportReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnImportReport.ForeColor = System.Drawing.Color.White;
            this.btnImportReport.Location = new System.Drawing.Point(301, 23);
            this.btnImportReport.Name = "btnImportReport";
            this.btnImportReport.Size = new System.Drawing.Size(200, 40);
            this.btnImportReport.TabIndex = 1;
            this.btnImportReport.Text = "Pdf ekle";
            this.btnImportReport.UseVisualStyleBackColor = false;
            this.btnImportReport.Click += new System.EventHandler(this.btnImportReport_Click);
            // 
            // StudentHomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StudentHomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartLGS - Student Portal";
            this.Load += new System.EventHandler(this.StudentHomePage_Load);
            this.pnlHeader.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentExams)).EndInit();
            this.tabAddExam.ResumeLayout(false);
            this.tabAddExam.PerformLayout();
            this.tabReports.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.TabPage tabAddExam;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformance;
        private System.Windows.Forms.Label lblRecentExams;
        private System.Windows.Forms.DataGridView dgvRecentExams;
        private System.Windows.Forms.Button btnSubmitExam;
        private System.Windows.Forms.TextBox txtExamName;
        private System.Windows.Forms.Label lblExamName;
        private System.Windows.Forms.Button btnImportReport;
        private System.Windows.Forms.GroupBox gbExamDetails;
        private System.Windows.Forms.TextBox[] txtCorrect;
        private System.Windows.Forms.TextBox[] txtWrong;
        private System.Windows.Forms.TextBox[] txtEmpty;
        private System.Windows.Forms.Label lblExamDate;
        private System.Windows.Forms.DateTimePicker dtpExamDate;

    }
}