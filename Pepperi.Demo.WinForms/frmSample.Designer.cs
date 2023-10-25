namespace WindowsFormsApp1
{
    partial class frmSample
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
            this.btnViewItems = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnViewAcounts = new System.Windows.Forms.Button();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.btnSaveSelected = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnUploadUdc = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnViewUdc = new System.Windows.Forms.Button();
            this.ipaasJobsTab = new System.Windows.Forms.TabControl();
            this.Generic = new System.Windows.Forms.TabPage();
            this.udt = new System.Windows.Forms.TabPage();
            this.udt_BulkUploadButton = new System.Windows.Forms.Button();
            this.udt_exportAsync = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DeleteSchemeButton = new System.Windows.Forms.Button();
            this.ExportDataFile = new System.Windows.Forms.Button();
            this.ViewSchemes = new System.Windows.Forms.Button();
            this.Surveys = new System.Windows.Forms.TabPage();
            this.btnViewTemplates = new System.Windows.Forms.Button();
            this.surveysUpsert = new System.Windows.Forms.Button();
            this.getSurveys = new System.Windows.Forms.Button();
            this.ipaasJobsTabPage = new System.Windows.Forms.TabPage();
            this.ipaasRunJobButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.ipaasJobsTab.SuspendLayout();
            this.Generic.SuspendLayout();
            this.udt.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.Surveys.SuspendLayout();
            this.ipaasJobsTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnViewItems
            // 
            this.btnViewItems.Location = new System.Drawing.Point(5, 5);
            this.btnViewItems.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewItems.Name = "btnViewItems";
            this.btnViewItems.Size = new System.Drawing.Size(116, 52);
            this.btnViewItems.TabIndex = 0;
            this.btnViewItems.Text = "View Items";
            this.btnViewItems.UseVisualStyleBackColor = true;
            this.btnViewItems.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 150);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(866, 316);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnViewAcounts
            // 
            this.btnViewAcounts.Location = new System.Drawing.Point(125, 5);
            this.btnViewAcounts.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewAcounts.Name = "btnViewAcounts";
            this.btnViewAcounts.Size = new System.Drawing.Size(154, 52);
            this.btnViewAcounts.TabIndex = 2;
            this.btnViewAcounts.Text = "View Accounts (Customers)";
            this.btnViewAcounts.UseVisualStyleBackColor = true;
            this.btnViewAcounts.Click += new System.EventHandler(this.btnViewAcounts_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Location = new System.Drawing.Point(403, 5);
            this.btnSaveAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(116, 52);
            this.btnSaveAll.TabIndex = 3;
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnSaveSelected
            // 
            this.btnSaveSelected.Location = new System.Drawing.Point(283, 5);
            this.btnSaveSelected.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveSelected.Name = "btnSaveSelected";
            this.btnSaveSelected.Size = new System.Drawing.Size(116, 52);
            this.btnSaveSelected.TabIndex = 4;
            this.btnSaveSelected.Text = "Save Selected";
            this.btnSaveSelected.UseVisualStyleBackColor = true;
            this.btnSaveSelected.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(9, 10);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(164, 37);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect And Use This Token";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(250, 10);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(198, 20);
            this.txtEmail.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(250, 32);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(198, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(506, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Company Token";
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(607, 10);
            this.txtToken.Margin = new System.Windows.Forms.Padding(2);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(269, 20);
            this.txtToken.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(506, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Filter Query";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(607, 32);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(269, 19);
            this.txtFilter.TabIndex = 12;
            this.txtFilter.Text = "Name Like \'%A%\'";
            // 
            // btnUploadUdc
            // 
            this.btnUploadUdc.Location = new System.Drawing.Point(76, 6);
            this.btnUploadUdc.Margin = new System.Windows.Forms.Padding(2);
            this.btnUploadUdc.Name = "btnUploadUdc";
            this.btnUploadUdc.Size = new System.Drawing.Size(91, 51);
            this.btnUploadUdc.TabIndex = 15;
            this.btnUploadUdc.Text = "Upload Data";
            this.btnUploadUdc.UseVisualStyleBackColor = true;
            this.btnUploadUdc.Click += new System.EventHandler(this.btnUploadUdc_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnViewUdc
            // 
            this.btnViewUdc.Location = new System.Drawing.Point(5, 5);
            this.btnViewUdc.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewUdc.Name = "btnViewUdc";
            this.btnViewUdc.Size = new System.Drawing.Size(67, 52);
            this.btnViewUdc.TabIndex = 16;
            this.btnViewUdc.Text = "View Data";
            this.btnViewUdc.UseVisualStyleBackColor = true;
            this.btnViewUdc.Click += new System.EventHandler(this.btnViewUdc_Click);
            // 
            // ipaasJobsTab
            // 
            this.ipaasJobsTab.Controls.Add(this.Generic);
            this.ipaasJobsTab.Controls.Add(this.udt);
            this.ipaasJobsTab.Controls.Add(this.tabPage2);
            this.ipaasJobsTab.Controls.Add(this.Surveys);
            this.ipaasJobsTab.Controls.Add(this.ipaasJobsTabPage);
            this.ipaasJobsTab.Location = new System.Drawing.Point(12, 56);
            this.ipaasJobsTab.Name = "ipaasJobsTab";
            this.ipaasJobsTab.SelectedIndex = 0;
            this.ipaasJobsTab.Size = new System.Drawing.Size(862, 89);
            this.ipaasJobsTab.TabIndex = 18;
            // 
            // Generic
            // 
            this.Generic.BackColor = System.Drawing.SystemColors.Control;
            this.Generic.Controls.Add(this.btnViewItems);
            this.Generic.Controls.Add(this.btnViewAcounts);
            this.Generic.Controls.Add(this.btnSaveSelected);
            this.Generic.Controls.Add(this.btnSaveAll);
            this.Generic.Location = new System.Drawing.Point(4, 22);
            this.Generic.Name = "Generic";
            this.Generic.Padding = new System.Windows.Forms.Padding(3);
            this.Generic.Size = new System.Drawing.Size(854, 63);
            this.Generic.TabIndex = 0;
            this.Generic.Text = "Generic";
            // 
            // udt
            // 
            this.udt.BackColor = System.Drawing.SystemColors.Control;
            this.udt.Controls.Add(this.udt_BulkUploadButton);
            this.udt.Controls.Add(this.udt_exportAsync);
            this.udt.Location = new System.Drawing.Point(4, 22);
            this.udt.Name = "udt";
            this.udt.Size = new System.Drawing.Size(854, 63);
            this.udt.TabIndex = 4;
            this.udt.Text = "User Defined Tables";
            // 
            // udt_BulkUploadButton
            // 
            this.udt_BulkUploadButton.Location = new System.Drawing.Point(766, 5);
            this.udt_BulkUploadButton.Margin = new System.Windows.Forms.Padding(2);
            this.udt_BulkUploadButton.Name = "udt_BulkUploadButton";
            this.udt_BulkUploadButton.Size = new System.Drawing.Size(86, 52);
            this.udt_BulkUploadButton.TabIndex = 19;
            this.udt_BulkUploadButton.Text = "Bulk Upload";
            this.udt_BulkUploadButton.UseVisualStyleBackColor = true;
            this.udt_BulkUploadButton.Click += new System.EventHandler(this.udt_BulkUploadButton_Click);
            // 
            // udt_exportAsync
            // 
            this.udt_exportAsync.Location = new System.Drawing.Point(2, 5);
            this.udt_exportAsync.Margin = new System.Windows.Forms.Padding(2);
            this.udt_exportAsync.Name = "udt_exportAsync";
            this.udt_exportAsync.Size = new System.Drawing.Size(86, 52);
            this.udt_exportAsync.TabIndex = 18;
            this.udt_exportAsync.Text = "Export Async";
            this.udt_exportAsync.UseVisualStyleBackColor = true;
            this.udt_exportAsync.Click += new System.EventHandler(this.udt_exportAsync_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.DeleteSchemeButton);
            this.tabPage2.Controls.Add(this.ExportDataFile);
            this.tabPage2.Controls.Add(this.ViewSchemes);
            this.tabPage2.Controls.Add(this.btnUploadUdc);
            this.tabPage2.Controls.Add(this.btnViewUdc);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(854, 63);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "User Defined Collections";
            // 
            // DeleteSchemeButton
            // 
            this.DeleteSchemeButton.Location = new System.Drawing.Point(757, 5);
            this.DeleteSchemeButton.Name = "DeleteSchemeButton";
            this.DeleteSchemeButton.Size = new System.Drawing.Size(89, 51);
            this.DeleteSchemeButton.TabIndex = 19;
            this.DeleteSchemeButton.Text = "Delete Scheme";
            this.DeleteSchemeButton.UseVisualStyleBackColor = true;
            this.DeleteSchemeButton.Click += new System.EventHandler(this.DeleteSchemeButton_Click);
            // 
            // ExportDataFile
            // 
            this.ExportDataFile.Location = new System.Drawing.Point(172, 6);
            this.ExportDataFile.Name = "ExportDataFile";
            this.ExportDataFile.Size = new System.Drawing.Size(102, 51);
            this.ExportDataFile.TabIndex = 18;
            this.ExportDataFile.Text = "Export Data (File)";
            this.ExportDataFile.UseVisualStyleBackColor = true;
            this.ExportDataFile.Click += new System.EventHandler(this.ExportDataFile_Click);
            // 
            // ViewSchemes
            // 
            this.ViewSchemes.Location = new System.Drawing.Point(662, 5);
            this.ViewSchemes.Name = "ViewSchemes";
            this.ViewSchemes.Size = new System.Drawing.Size(89, 51);
            this.ViewSchemes.TabIndex = 17;
            this.ViewSchemes.Text = "View Schemes";
            this.ViewSchemes.UseVisualStyleBackColor = true;
            this.ViewSchemes.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Surveys
            // 
            this.Surveys.BackColor = System.Drawing.SystemColors.Control;
            this.Surveys.Controls.Add(this.btnViewTemplates);
            this.Surveys.Controls.Add(this.surveysUpsert);
            this.Surveys.Controls.Add(this.getSurveys);
            this.Surveys.Location = new System.Drawing.Point(4, 22);
            this.Surveys.Name = "Surveys";
            this.Surveys.Padding = new System.Windows.Forms.Padding(3);
            this.Surveys.Size = new System.Drawing.Size(854, 63);
            this.Surveys.TabIndex = 2;
            this.Surveys.Text = "Surveys";
            // 
            // btnViewTemplates
            // 
            this.btnViewTemplates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnViewTemplates.Location = new System.Drawing.Point(760, 6);
            this.btnViewTemplates.Name = "btnViewTemplates";
            this.btnViewTemplates.Size = new System.Drawing.Size(88, 51);
            this.btnViewTemplates.TabIndex = 2;
            this.btnViewTemplates.Text = "View Templates";
            this.btnViewTemplates.UseVisualStyleBackColor = true;
            this.btnViewTemplates.Click += new System.EventHandler(this.btnViewTemplates_Click);
            // 
            // surveysUpsert
            // 
            this.surveysUpsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.surveysUpsert.Location = new System.Drawing.Point(100, 6);
            this.surveysUpsert.Name = "surveysUpsert";
            this.surveysUpsert.Size = new System.Drawing.Size(88, 51);
            this.surveysUpsert.TabIndex = 1;
            this.surveysUpsert.Text = "Upsert";
            this.surveysUpsert.UseVisualStyleBackColor = true;
            this.surveysUpsert.Click += new System.EventHandler(this.surveysUpsert_Click);
            // 
            // getSurveys
            // 
            this.getSurveys.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.getSurveys.Location = new System.Drawing.Point(6, 6);
            this.getSurveys.Name = "getSurveys";
            this.getSurveys.Size = new System.Drawing.Size(88, 51);
            this.getSurveys.TabIndex = 0;
            this.getSurveys.Text = "View";
            this.getSurveys.UseVisualStyleBackColor = true;
            this.getSurveys.Click += new System.EventHandler(this.getSurveys_Click);
            // 
            // ipaasJobsTabPage
            // 
            this.ipaasJobsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.ipaasJobsTabPage.Controls.Add(this.ipaasRunJobButton);
            this.ipaasJobsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ipaasJobsTabPage.Name = "ipaasJobsTabPage";
            this.ipaasJobsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ipaasJobsTabPage.Size = new System.Drawing.Size(854, 63);
            this.ipaasJobsTabPage.TabIndex = 3;
            this.ipaasJobsTabPage.Text = "iPaaS Jobs";
            // 
            // ipaasRunJobButton
            // 
            this.ipaasRunJobButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ipaasRunJobButton.Location = new System.Drawing.Point(6, 6);
            this.ipaasRunJobButton.Name = "ipaasRunJobButton";
            this.ipaasRunJobButton.Size = new System.Drawing.Size(88, 51);
            this.ipaasRunJobButton.TabIndex = 1;
            this.ipaasRunJobButton.Text = "Run Job";
            this.ipaasRunJobButton.UseVisualStyleBackColor = true;
            this.ipaasRunJobButton.Click += new System.EventHandler(this.ipaasRunJobButton_Click);
            // 
            // frmSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 477);
            this.Controls.Add(this.ipaasJobsTab);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pepperi API Demo";
            this.Load += new System.EventHandler(this.frmSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ipaasJobsTab.ResumeLayout(false);
            this.Generic.ResumeLayout(false);
            this.udt.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.Surveys.ResumeLayout(false);
            this.ipaasJobsTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnViewItems;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnViewAcounts;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnSaveSelected;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnUploadUdc;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnViewUdc;
        private System.Windows.Forms.TabControl ipaasJobsTab;
        private System.Windows.Forms.TabPage Generic;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button ViewSchemes;
        private System.Windows.Forms.Button ExportDataFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button DeleteSchemeButton;
        private System.Windows.Forms.TabPage Surveys;
        private System.Windows.Forms.Button getSurveys;
        private System.Windows.Forms.Button surveysUpsert;
        private System.Windows.Forms.Button btnViewTemplates;
        private System.Windows.Forms.TabPage ipaasJobsTabPage;
        private System.Windows.Forms.Button ipaasRunJobButton;
        private System.Windows.Forms.TabPage udt;
        private System.Windows.Forms.Button udt_exportAsync;
        private System.Windows.Forms.Button udt_BulkUploadButton;
    }
}

