// Form1.Designer.cs - Modernleştirilmiş Arayüz
using System.Drawing;
using System.Windows.Forms;

namespace DatabaseSimulation
{
    partial class Form1
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelTypeA = new System.Windows.Forms.Label();
            this.numericUpDownTypeA = new System.Windows.Forms.NumericUpDown();
            this.labelTypeB = new System.Windows.Forms.Label();
            this.numericUpDownTypeB = new System.Windows.Forms.NumericUpDown();
            this.labelIsolationLevel = new System.Windows.Forms.Label();
            this.comboBoxIsolationLevel = new System.Windows.Forms.ComboBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeB)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTypeA
            // 
            this.labelTypeA.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelTypeA.Location = new System.Drawing.Point(40, 80);
            this.labelTypeA.Name = "labelTypeA";
            this.labelTypeA.Size = new System.Drawing.Size(100, 23);
            this.labelTypeA.TabIndex = 1;
            this.labelTypeA.Text = "A Tipi Kullanıcı Sayısı:";
            // 
            // numericUpDownTypeA
            // 
            this.numericUpDownTypeA.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numericUpDownTypeA.Location = new System.Drawing.Point(250, 80);
            this.numericUpDownTypeA.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTypeA.Name = "numericUpDownTypeA";
            this.numericUpDownTypeA.Size = new System.Drawing.Size(120, 25);
            this.numericUpDownTypeA.TabIndex = 2;
            // 
            // labelTypeB
            // 
            this.labelTypeB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelTypeB.Location = new System.Drawing.Point(40, 120);
            this.labelTypeB.Name = "labelTypeB";
            this.labelTypeB.Size = new System.Drawing.Size(100, 23);
            this.labelTypeB.TabIndex = 3;
            this.labelTypeB.Text = "B Tipi Kullanıcı Sayısı:";
            // 
            // numericUpDownTypeB
            // 
            this.numericUpDownTypeB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numericUpDownTypeB.Location = new System.Drawing.Point(250, 120);
            this.numericUpDownTypeB.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTypeB.Name = "numericUpDownTypeB";
            this.numericUpDownTypeB.Size = new System.Drawing.Size(120, 25);
            this.numericUpDownTypeB.TabIndex = 4;
            // 
            // labelIsolationLevel
            // 
            this.labelIsolationLevel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelIsolationLevel.Location = new System.Drawing.Point(40, 160);
            this.labelIsolationLevel.Name = "labelIsolationLevel";
            this.labelIsolationLevel.Size = new System.Drawing.Size(100, 23);
            this.labelIsolationLevel.TabIndex = 5;
            this.labelIsolationLevel.Text = "İzolasyon Seviyesi:";
            // 
            // comboBoxIsolationLevel
            // 
            this.comboBoxIsolationLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIsolationLevel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxIsolationLevel.Items.AddRange(new object[] {
            "READ UNCOMMITTED",
            "READ COMMITTED",
            "REPEATABLE READ",
            "SERIALIZABLE"});
            this.comboBoxIsolationLevel.Location = new System.Drawing.Point(250, 160);
            this.comboBoxIsolationLevel.Name = "comboBoxIsolationLevel";
            this.comboBoxIsolationLevel.Size = new System.Drawing.Size(121, 25);
            this.comboBoxIsolationLevel.TabIndex = 6;
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonStart.ForeColor = System.Drawing.Color.White;
            this.buttonStart.Location = new System.Drawing.Point(40, 210);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(350, 40);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Simülasyonu Başlat";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxResults
            // 
            this.textBoxResults.Font = new System.Drawing.Font("Consolas", 11F);
            this.textBoxResults.Location = new System.Drawing.Point(20, 270);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ReadOnly = true;
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResults.Size = new System.Drawing.Size(960, 300);
            this.textBoxResults.TabIndex = 9;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(420, 210);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(540, 40);
            this.progressBar.TabIndex = 8;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 60);
            this.panelTop.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(20, 15);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(316, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "SE 308 - Database Simulation";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.labelTypeA);
            this.Controls.Add(this.numericUpDownTypeA);
            this.Controls.Add(this.labelTypeB);
            this.Controls.Add(this.numericUpDownTypeB);
            this.Controls.Add(this.labelIsolationLevel);
            this.Controls.Add(this.comboBoxIsolationLevel);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.textBoxResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Simulation";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTypeB)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTypeA;
        private System.Windows.Forms.NumericUpDown numericUpDownTypeA;
        private System.Windows.Forms.Label labelTypeB;
        private System.Windows.Forms.NumericUpDown numericUpDownTypeB;
        private System.Windows.Forms.Label labelIsolationLevel;
        private System.Windows.Forms.ComboBox comboBoxIsolationLevel;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxResults;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelTitle;
    }
}