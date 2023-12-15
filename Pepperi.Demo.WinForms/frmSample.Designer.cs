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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.otherTab = new System.Windows.Forms.TabPage();
            this.OtherTabControl = new System.Windows.Forms.TabControl();
            this.JourneysTabPage = new System.Windows.Forms.TabPage();
            this.JourneysSearchFiles = new System.Windows.Forms.Button();
            this.NotificationsTabPage = new System.Windows.Forms.TabPage();
            this.Notifications_PostButton = new System.Windows.Forms.Button();
            this.NotificationsShowJsonCheckbox = new System.Windows.Forms.CheckBox();
            this.NotificationsFindButton = new System.Windows.Forms.Button();
            this.RelatedItemsTabPage = new System.Windows.Forms.TabPage();
            this.RelatedItems_UploadFile_Button = new System.Windows.Forms.Button();
            this.RelatedItems_ExportAsyncButton = new System.Windows.Forms.Button();
            this.RelatedItems_UpsertButton = new System.Windows.Forms.Button();
            this.RelatedItems_ShowJsonCheckbox = new System.Windows.Forms.CheckBox();
            this.RelatedItems_FindByKeyButton = new System.Windows.Forms.Button();
            this.RelatedItems_FindButton = new System.Windows.Forms.Button();
            this.ipaasJobsTabPage = new System.Windows.Forms.TabPage();
            this.ipaasRunJobButton = new System.Windows.Forms.Button();
            this.Surveys = new System.Windows.Forms.TabPage();
            this.btnViewTemplates = new System.Windows.Forms.Button();
            this.surveysUpsert = new System.Windows.Forms.Button();
            this.getSurveys = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DeleteSchemeButton = new System.Windows.Forms.Button();
            this.ExportDataFile = new System.Windows.Forms.Button();
            this.ViewSchemes = new System.Windows.Forms.Button();
            this.btnUploadUdc = new System.Windows.Forms.Button();
            this.btnViewUdc = new System.Windows.Forms.Button();
            this.MainResourcesTabPage = new System.Windows.Forms.TabPage();
            this.MainResource_BulkUploadButton = new System.Windows.Forms.Button();
            this.MainResource_ExportAsyncButton = new System.Windows.Forms.Button();
            this.MainResource_ShowJsonCheckBox = new System.Windows.Forms.CheckBox();
            this.MainResource_FindButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.MainResources_ResourceToUse = new System.Windows.Forms.ComboBox();
            this.Generic = new System.Windows.Forms.TabPage();
            this.btnViewItems = new System.Windows.Forms.Button();
            this.btnViewAcounts = new System.Windows.Forms.Button();
            this.btnSaveSelected = new System.Windows.Forms.Button();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.ipaasJobsTab = new System.Windows.Forms.TabControl();
            this.SaveAndUseToken_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.otherTab.SuspendLayout();
            this.OtherTabControl.SuspendLayout();
            this.JourneysTabPage.SuspendLayout();
            this.NotificationsTabPage.SuspendLayout();
            this.RelatedItemsTabPage.SuspendLayout();
            this.ipaasJobsTabPage.SuspendLayout();
            this.Surveys.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.MainResourcesTabPage.SuspendLayout();
            this.Generic.SuspendLayout();
            this.ipaasJobsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 174);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(866, 358);
            this.dataGridView1.TabIndex = 1;
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
            this.label4.Location = new System.Drawing.Point(577, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Filter Query";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(580, 37);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(269, 19);
            this.txtFilter.TabIndex = 12;
            this.txtFilter.Text = "Name Like \'%A%\'";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // otherTab
            // 
            this.otherTab.BackColor = System.Drawing.SystemColors.Control;
            this.otherTab.Controls.Add(this.OtherTabControl);
            this.otherTab.Location = new System.Drawing.Point(4, 22);
            this.otherTab.Name = "otherTab";
            this.otherTab.Size = new System.Drawing.Size(854, 87);
            this.otherTab.TabIndex = 5;
            this.otherTab.Text = "Other";
            // 
            // OtherTabControl
            // 
            this.OtherTabControl.Controls.Add(this.JourneysTabPage);
            this.OtherTabControl.Controls.Add(this.NotificationsTabPage);
            this.OtherTabControl.Controls.Add(this.RelatedItemsTabPage);
            this.OtherTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OtherTabControl.Location = new System.Drawing.Point(0, 0);
            this.OtherTabControl.Name = "OtherTabControl";
            this.OtherTabControl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OtherTabControl.SelectedIndex = 0;
            this.OtherTabControl.Size = new System.Drawing.Size(854, 87);
            this.OtherTabControl.TabIndex = 0;
            // 
            // JourneysTabPage
            // 
            this.JourneysTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.JourneysTabPage.Controls.Add(this.JourneysSearchFiles);
            this.JourneysTabPage.Location = new System.Drawing.Point(4, 22);
            this.JourneysTabPage.Name = "JourneysTabPage";
            this.JourneysTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.JourneysTabPage.Size = new System.Drawing.Size(846, 61);
            this.JourneysTabPage.TabIndex = 0;
            this.JourneysTabPage.Text = "Journeys";
            // 
            // JourneysSearchFiles
            // 
            this.JourneysSearchFiles.Location = new System.Drawing.Point(5, 4);
            this.JourneysSearchFiles.Margin = new System.Windows.Forms.Padding(2);
            this.JourneysSearchFiles.Name = "JourneysSearchFiles";
            this.JourneysSearchFiles.Size = new System.Drawing.Size(94, 52);
            this.JourneysSearchFiles.TabIndex = 17;
            this.JourneysSearchFiles.Text = "Search Files";
            this.JourneysSearchFiles.UseVisualStyleBackColor = true;
            this.JourneysSearchFiles.Click += new System.EventHandler(this.JourneysSearchFiles_Click);
            // 
            // NotificationsTabPage
            // 
            this.NotificationsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.NotificationsTabPage.Controls.Add(this.Notifications_PostButton);
            this.NotificationsTabPage.Controls.Add(this.NotificationsShowJsonCheckbox);
            this.NotificationsTabPage.Controls.Add(this.NotificationsFindButton);
            this.NotificationsTabPage.Location = new System.Drawing.Point(4, 22);
            this.NotificationsTabPage.Name = "NotificationsTabPage";
            this.NotificationsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.NotificationsTabPage.Size = new System.Drawing.Size(846, 61);
            this.NotificationsTabPage.TabIndex = 1;
            this.NotificationsTabPage.Text = "Notifications";
            // 
            // Notifications_PostButton
            // 
            this.Notifications_PostButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Notifications_PostButton.Location = new System.Drawing.Point(747, 6);
            this.Notifications_PostButton.Margin = new System.Windows.Forms.Padding(2);
            this.Notifications_PostButton.Name = "Notifications_PostButton";
            this.Notifications_PostButton.Size = new System.Drawing.Size(94, 50);
            this.Notifications_PostButton.TabIndex = 20;
            this.Notifications_PostButton.Text = "POST";
            this.Notifications_PostButton.UseVisualStyleBackColor = true;
            this.Notifications_PostButton.Click += new System.EventHandler(this.Notifications_PostButton_Click);
            // 
            // NotificationsShowJsonCheckbox
            // 
            this.NotificationsShowJsonCheckbox.AutoSize = true;
            this.NotificationsShowJsonCheckbox.Location = new System.Drawing.Point(6, 6);
            this.NotificationsShowJsonCheckbox.Name = "NotificationsShowJsonCheckbox";
            this.NotificationsShowJsonCheckbox.Size = new System.Drawing.Size(84, 17);
            this.NotificationsShowJsonCheckbox.TabIndex = 19;
            this.NotificationsShowJsonCheckbox.Text = "Show JSON";
            this.NotificationsShowJsonCheckbox.UseVisualStyleBackColor = true;
            // 
            // NotificationsFindButton
            // 
            this.NotificationsFindButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotificationsFindButton.Location = new System.Drawing.Point(5, 26);
            this.NotificationsFindButton.Margin = new System.Windows.Forms.Padding(2);
            this.NotificationsFindButton.Name = "NotificationsFindButton";
            this.NotificationsFindButton.Size = new System.Drawing.Size(94, 30);
            this.NotificationsFindButton.TabIndex = 18;
            this.NotificationsFindButton.Text = "Find";
            this.NotificationsFindButton.UseVisualStyleBackColor = true;
            this.NotificationsFindButton.Click += new System.EventHandler(this.NotificationsFindButton_Click);
            // 
            // RelatedItemsTabPage
            // 
            this.RelatedItemsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.RelatedItemsTabPage.Controls.Add(this.RelatedItems_UploadFile_Button);
            this.RelatedItemsTabPage.Controls.Add(this.RelatedItems_ExportAsyncButton);
            this.RelatedItemsTabPage.Controls.Add(this.RelatedItems_UpsertButton);
            this.RelatedItemsTabPage.Controls.Add(this.RelatedItems_ShowJsonCheckbox);
            this.RelatedItemsTabPage.Controls.Add(this.RelatedItems_FindByKeyButton);
            this.RelatedItemsTabPage.Controls.Add(this.RelatedItems_FindButton);
            this.RelatedItemsTabPage.Location = new System.Drawing.Point(4, 22);
            this.RelatedItemsTabPage.Name = "RelatedItemsTabPage";
            this.RelatedItemsTabPage.Size = new System.Drawing.Size(846, 61);
            this.RelatedItemsTabPage.TabIndex = 2;
            this.RelatedItemsTabPage.Text = "Related Items";
            // 
            // RelatedItems_UploadFile_Button
            // 
            this.RelatedItems_UploadFile_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelatedItems_UploadFile_Button.Location = new System.Drawing.Point(709, 2);
            this.RelatedItems_UploadFile_Button.Margin = new System.Windows.Forms.Padding(2);
            this.RelatedItems_UploadFile_Button.Name = "RelatedItems_UploadFile_Button";
            this.RelatedItems_UploadFile_Button.Size = new System.Drawing.Size(135, 57);
            this.RelatedItems_UploadFile_Button.TabIndex = 24;
            this.RelatedItems_UploadFile_Button.Text = "Upload Data (File)";
            this.RelatedItems_UploadFile_Button.UseVisualStyleBackColor = true;
            this.RelatedItems_UploadFile_Button.Click += new System.EventHandler(this.RelatedItems_UploadFile_Button_Click);
            // 
            // RelatedItems_ExportAsyncButton
            // 
            this.RelatedItems_ExportAsyncButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelatedItems_ExportAsyncButton.Location = new System.Drawing.Point(422, 2);
            this.RelatedItems_ExportAsyncButton.Margin = new System.Windows.Forms.Padding(2);
            this.RelatedItems_ExportAsyncButton.Name = "RelatedItems_ExportAsyncButton";
            this.RelatedItems_ExportAsyncButton.Size = new System.Drawing.Size(124, 57);
            this.RelatedItems_ExportAsyncButton.TabIndex = 23;
            this.RelatedItems_ExportAsyncButton.Text = "Export Data (File)";
            this.RelatedItems_ExportAsyncButton.UseVisualStyleBackColor = true;
            this.RelatedItems_ExportAsyncButton.Click += new System.EventHandler(this.RelatedItems_ExportAsyncButton_Click);
            // 
            // RelatedItems_UpsertButton
            // 
            this.RelatedItems_UpsertButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelatedItems_UpsertButton.Location = new System.Drawing.Point(7, 31);
            this.RelatedItems_UpsertButton.Margin = new System.Windows.Forms.Padding(2);
            this.RelatedItems_UpsertButton.Name = "RelatedItems_UpsertButton";
            this.RelatedItems_UpsertButton.Size = new System.Drawing.Size(101, 28);
            this.RelatedItems_UpsertButton.TabIndex = 22;
            this.RelatedItems_UpsertButton.Text = "Upsert";
            this.RelatedItems_UpsertButton.UseVisualStyleBackColor = true;
            this.RelatedItems_UpsertButton.Click += new System.EventHandler(this.RelatedItems_UpsertButton_Click);
            // 
            // RelatedItems_ShowJsonCheckbox
            // 
            this.RelatedItems_ShowJsonCheckbox.AutoSize = true;
            this.RelatedItems_ShowJsonCheckbox.Location = new System.Drawing.Point(14, 9);
            this.RelatedItems_ShowJsonCheckbox.Name = "RelatedItems_ShowJsonCheckbox";
            this.RelatedItems_ShowJsonCheckbox.Size = new System.Drawing.Size(84, 17);
            this.RelatedItems_ShowJsonCheckbox.TabIndex = 21;
            this.RelatedItems_ShowJsonCheckbox.Text = "Show JSON";
            this.RelatedItems_ShowJsonCheckbox.UseVisualStyleBackColor = true;
            // 
            // RelatedItems_FindByKeyButton
            // 
            this.RelatedItems_FindByKeyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelatedItems_FindByKeyButton.Location = new System.Drawing.Point(112, 31);
            this.RelatedItems_FindByKeyButton.Margin = new System.Windows.Forms.Padding(2);
            this.RelatedItems_FindByKeyButton.Name = "RelatedItems_FindByKeyButton";
            this.RelatedItems_FindByKeyButton.Size = new System.Drawing.Size(101, 28);
            this.RelatedItems_FindByKeyButton.TabIndex = 20;
            this.RelatedItems_FindByKeyButton.Text = "Find By Key";
            this.RelatedItems_FindByKeyButton.UseVisualStyleBackColor = true;
            this.RelatedItems_FindByKeyButton.Click += new System.EventHandler(this.RelatedItems_FindByKeyButton_Click);
            // 
            // RelatedItems_FindButton
            // 
            this.RelatedItems_FindButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelatedItems_FindButton.Location = new System.Drawing.Point(112, 2);
            this.RelatedItems_FindButton.Margin = new System.Windows.Forms.Padding(2);
            this.RelatedItems_FindButton.Name = "RelatedItems_FindButton";
            this.RelatedItems_FindButton.Size = new System.Drawing.Size(101, 28);
            this.RelatedItems_FindButton.TabIndex = 19;
            this.RelatedItems_FindButton.Text = "Find";
            this.RelatedItems_FindButton.UseVisualStyleBackColor = true;
            this.RelatedItems_FindButton.Click += new System.EventHandler(this.RelatedItems_FindButton_Click);
            // 
            // ipaasJobsTabPage
            // 
            this.ipaasJobsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.ipaasJobsTabPage.Controls.Add(this.ipaasRunJobButton);
            this.ipaasJobsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ipaasJobsTabPage.Name = "ipaasJobsTabPage";
            this.ipaasJobsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ipaasJobsTabPage.Size = new System.Drawing.Size(854, 87);
            this.ipaasJobsTabPage.TabIndex = 3;
            this.ipaasJobsTabPage.Text = "iPaaS Jobs";
            // 
            // ipaasRunJobButton
            // 
            this.ipaasRunJobButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ipaasRunJobButton.Location = new System.Drawing.Point(6, 19);
            this.ipaasRunJobButton.Name = "ipaasRunJobButton";
            this.ipaasRunJobButton.Size = new System.Drawing.Size(88, 51);
            this.ipaasRunJobButton.TabIndex = 1;
            this.ipaasRunJobButton.Text = "Run Job";
            this.ipaasRunJobButton.UseVisualStyleBackColor = true;
            this.ipaasRunJobButton.Click += new System.EventHandler(this.ipaasRunJobButton_Click);
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
            this.Surveys.Size = new System.Drawing.Size(854, 87);
            this.Surveys.TabIndex = 2;
            this.Surveys.Text = "Surveys";
            // 
            // btnViewTemplates
            // 
            this.btnViewTemplates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnViewTemplates.Location = new System.Drawing.Point(760, 20);
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
            this.surveysUpsert.Location = new System.Drawing.Point(100, 20);
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
            this.getSurveys.Location = new System.Drawing.Point(6, 20);
            this.getSurveys.Name = "getSurveys";
            this.getSurveys.Size = new System.Drawing.Size(88, 51);
            this.getSurveys.TabIndex = 0;
            this.getSurveys.Text = "View";
            this.getSurveys.UseVisualStyleBackColor = true;
            this.getSurveys.Click += new System.EventHandler(this.getSurveys_Click);
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
            this.tabPage2.Size = new System.Drawing.Size(854, 87);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "User Defined Collections";
            // 
            // DeleteSchemeButton
            // 
            this.DeleteSchemeButton.Location = new System.Drawing.Point(755, 20);
            this.DeleteSchemeButton.Name = "DeleteSchemeButton";
            this.DeleteSchemeButton.Size = new System.Drawing.Size(89, 51);
            this.DeleteSchemeButton.TabIndex = 19;
            this.DeleteSchemeButton.Text = "Delete Scheme";
            this.DeleteSchemeButton.UseVisualStyleBackColor = true;
            this.DeleteSchemeButton.Click += new System.EventHandler(this.DeleteSchemeButton_Click);
            // 
            // ExportDataFile
            // 
            this.ExportDataFile.Location = new System.Drawing.Point(170, 21);
            this.ExportDataFile.Name = "ExportDataFile";
            this.ExportDataFile.Size = new System.Drawing.Size(102, 51);
            this.ExportDataFile.TabIndex = 18;
            this.ExportDataFile.Text = "Export Data (File)";
            this.ExportDataFile.UseVisualStyleBackColor = true;
            this.ExportDataFile.Click += new System.EventHandler(this.ExportDataFile_Click);
            // 
            // ViewSchemes
            // 
            this.ViewSchemes.Location = new System.Drawing.Point(660, 20);
            this.ViewSchemes.Name = "ViewSchemes";
            this.ViewSchemes.Size = new System.Drawing.Size(89, 51);
            this.ViewSchemes.TabIndex = 17;
            this.ViewSchemes.Text = "View Schemes";
            this.ViewSchemes.UseVisualStyleBackColor = true;
            this.ViewSchemes.Click += new System.EventHandler(this.Udc_ViewSchemes_Button_Click);
            // 
            // btnUploadUdc
            // 
            this.btnUploadUdc.Location = new System.Drawing.Point(74, 21);
            this.btnUploadUdc.Margin = new System.Windows.Forms.Padding(2);
            this.btnUploadUdc.Name = "btnUploadUdc";
            this.btnUploadUdc.Size = new System.Drawing.Size(91, 51);
            this.btnUploadUdc.TabIndex = 15;
            this.btnUploadUdc.Text = "Upload Data";
            this.btnUploadUdc.UseVisualStyleBackColor = true;
            this.btnUploadUdc.Click += new System.EventHandler(this.btnUploadUdc_Click);
            // 
            // btnViewUdc
            // 
            this.btnViewUdc.Location = new System.Drawing.Point(3, 20);
            this.btnViewUdc.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewUdc.Name = "btnViewUdc";
            this.btnViewUdc.Size = new System.Drawing.Size(67, 52);
            this.btnViewUdc.TabIndex = 16;
            this.btnViewUdc.Text = "View Data";
            this.btnViewUdc.UseVisualStyleBackColor = true;
            this.btnViewUdc.Click += new System.EventHandler(this.btnViewUdc_Click);
            // 
            // MainResourcesTabPage
            // 
            this.MainResourcesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.MainResourcesTabPage.Controls.Add(this.MainResource_BulkUploadButton);
            this.MainResourcesTabPage.Controls.Add(this.MainResource_ExportAsyncButton);
            this.MainResourcesTabPage.Controls.Add(this.MainResource_ShowJsonCheckBox);
            this.MainResourcesTabPage.Controls.Add(this.MainResource_FindButton);
            this.MainResourcesTabPage.Controls.Add(this.label5);
            this.MainResourcesTabPage.Controls.Add(this.MainResources_ResourceToUse);
            this.MainResourcesTabPage.Location = new System.Drawing.Point(4, 22);
            this.MainResourcesTabPage.Margin = new System.Windows.Forms.Padding(2);
            this.MainResourcesTabPage.Name = "MainResourcesTabPage";
            this.MainResourcesTabPage.Size = new System.Drawing.Size(854, 87);
            this.MainResourcesTabPage.TabIndex = 6;
            this.MainResourcesTabPage.Text = "Main Resources";
            // 
            // MainResource_BulkUploadButton
            // 
            this.MainResource_BulkUploadButton.Location = new System.Drawing.Point(762, 17);
            this.MainResource_BulkUploadButton.Margin = new System.Windows.Forms.Padding(2);
            this.MainResource_BulkUploadButton.Name = "MainResource_BulkUploadButton";
            this.MainResource_BulkUploadButton.Size = new System.Drawing.Size(90, 57);
            this.MainResource_BulkUploadButton.TabIndex = 20;
            this.MainResource_BulkUploadButton.Text = "Bulk Upload";
            this.MainResource_BulkUploadButton.UseVisualStyleBackColor = true;
            this.MainResource_BulkUploadButton.Click += new System.EventHandler(this.MainResource_BulkUploadButton_Click);
            // 
            // MainResource_ExportAsyncButton
            // 
            this.MainResource_ExportAsyncButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainResource_ExportAsyncButton.Location = new System.Drawing.Point(328, 17);
            this.MainResource_ExportAsyncButton.Margin = new System.Windows.Forms.Padding(2);
            this.MainResource_ExportAsyncButton.Name = "MainResource_ExportAsyncButton";
            this.MainResource_ExportAsyncButton.Size = new System.Drawing.Size(90, 57);
            this.MainResource_ExportAsyncButton.TabIndex = 19;
            this.MainResource_ExportAsyncButton.Text = "Export Async";
            this.MainResource_ExportAsyncButton.UseVisualStyleBackColor = true;
            this.MainResource_ExportAsyncButton.Click += new System.EventHandler(this.MainResource_ExportAsyncButton_Click);
            // 
            // MainResource_ShowJsonCheckBox
            // 
            this.MainResource_ShowJsonCheckBox.AutoSize = true;
            this.MainResource_ShowJsonCheckBox.Location = new System.Drawing.Point(5, 57);
            this.MainResource_ShowJsonCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.MainResource_ShowJsonCheckBox.Name = "MainResource_ShowJsonCheckBox";
            this.MainResource_ShowJsonCheckBox.Size = new System.Drawing.Size(84, 17);
            this.MainResource_ShowJsonCheckBox.TabIndex = 3;
            this.MainResource_ShowJsonCheckBox.Text = "Show JSON";
            this.MainResource_ShowJsonCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainResource_FindButton
            // 
            this.MainResource_FindButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainResource_FindButton.Location = new System.Drawing.Point(234, 17);
            this.MainResource_FindButton.Margin = new System.Windows.Forms.Padding(2);
            this.MainResource_FindButton.Name = "MainResource_FindButton";
            this.MainResource_FindButton.Size = new System.Drawing.Size(90, 57);
            this.MainResource_FindButton.TabIndex = 2;
            this.MainResource_FindButton.Text = "Find";
            this.MainResource_FindButton.UseVisualStyleBackColor = true;
            this.MainResource_FindButton.Click += new System.EventHandler(this.MainResource_FindButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Resource to Use";
            // 
            // MainResources_ResourceToUse
            // 
            this.MainResources_ResourceToUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainResources_ResourceToUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainResources_ResourceToUse.FormattingEnabled = true;
            this.MainResources_ResourceToUse.Items.AddRange(new object[] {
            "Account Users"});
            this.MainResources_ResourceToUse.Location = new System.Drawing.Point(5, 20);
            this.MainResources_ResourceToUse.Margin = new System.Windows.Forms.Padding(2);
            this.MainResources_ResourceToUse.Name = "MainResources_ResourceToUse";
            this.MainResources_ResourceToUse.Size = new System.Drawing.Size(184, 25);
            this.MainResources_ResourceToUse.TabIndex = 0;
            // 
            // Generic
            // 
            this.Generic.BackColor = System.Drawing.SystemColors.Control;
            this.Generic.Controls.Add(this.btnViewItems);
            this.Generic.Controls.Add(this.label4);
            this.Generic.Controls.Add(this.btnViewAcounts);
            this.Generic.Controls.Add(this.txtFilter);
            this.Generic.Controls.Add(this.btnSaveSelected);
            this.Generic.Controls.Add(this.btnSaveAll);
            this.Generic.Location = new System.Drawing.Point(4, 22);
            this.Generic.Name = "Generic";
            this.Generic.Padding = new System.Windows.Forms.Padding(3);
            this.Generic.Size = new System.Drawing.Size(854, 87);
            this.Generic.TabIndex = 0;
            this.Generic.Text = "Generic";
            // 
            // btnViewItems
            // 
            this.btnViewItems.Location = new System.Drawing.Point(5, 20);
            this.btnViewItems.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewItems.Name = "btnViewItems";
            this.btnViewItems.Size = new System.Drawing.Size(116, 52);
            this.btnViewItems.TabIndex = 0;
            this.btnViewItems.Text = "View Items";
            this.btnViewItems.UseVisualStyleBackColor = true;
            this.btnViewItems.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnViewAcounts
            // 
            this.btnViewAcounts.Location = new System.Drawing.Point(125, 20);
            this.btnViewAcounts.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewAcounts.Name = "btnViewAcounts";
            this.btnViewAcounts.Size = new System.Drawing.Size(154, 52);
            this.btnViewAcounts.TabIndex = 2;
            this.btnViewAcounts.Text = "View Accounts (Customers)";
            this.btnViewAcounts.UseVisualStyleBackColor = true;
            this.btnViewAcounts.Click += new System.EventHandler(this.btnViewAcounts_Click);
            // 
            // btnSaveSelected
            // 
            this.btnSaveSelected.Location = new System.Drawing.Point(283, 20);
            this.btnSaveSelected.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveSelected.Name = "btnSaveSelected";
            this.btnSaveSelected.Size = new System.Drawing.Size(116, 52);
            this.btnSaveSelected.TabIndex = 4;
            this.btnSaveSelected.Text = "Save Selected";
            this.btnSaveSelected.UseVisualStyleBackColor = true;
            this.btnSaveSelected.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Location = new System.Drawing.Point(403, 20);
            this.btnSaveAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(116, 52);
            this.btnSaveAll.TabIndex = 3;
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.button3_Click);
            // 
            // ipaasJobsTab
            // 
            this.ipaasJobsTab.Controls.Add(this.Generic);
            this.ipaasJobsTab.Controls.Add(this.MainResourcesTabPage);
            this.ipaasJobsTab.Controls.Add(this.tabPage2);
            this.ipaasJobsTab.Controls.Add(this.Surveys);
            this.ipaasJobsTab.Controls.Add(this.ipaasJobsTabPage);
            this.ipaasJobsTab.Controls.Add(this.otherTab);
            this.ipaasJobsTab.Location = new System.Drawing.Point(12, 56);
            this.ipaasJobsTab.Name = "ipaasJobsTab";
            this.ipaasJobsTab.SelectedIndex = 0;
            this.ipaasJobsTab.Size = new System.Drawing.Size(862, 113);
            this.ipaasJobsTab.TabIndex = 18;
            // 
            // SaveAndUseToken_Button
            // 
            this.SaveAndUseToken_Button.Location = new System.Drawing.Point(738, 32);
            this.SaveAndUseToken_Button.Margin = new System.Windows.Forms.Padding(2);
            this.SaveAndUseToken_Button.Name = "SaveAndUseToken_Button";
            this.SaveAndUseToken_Button.Size = new System.Drawing.Size(138, 37);
            this.SaveAndUseToken_Button.TabIndex = 19;
            this.SaveAndUseToken_Button.Text = "Save And Use Token";
            this.SaveAndUseToken_Button.UseVisualStyleBackColor = true;
            this.SaveAndUseToken_Button.Click += new System.EventHandler(this.SaveAndUseToken_Button_Click);
            // 
            // frmSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 543);
            this.Controls.Add(this.SaveAndUseToken_Button);
            this.Controls.Add(this.ipaasJobsTab);
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
            this.otherTab.ResumeLayout(false);
            this.OtherTabControl.ResumeLayout(false);
            this.JourneysTabPage.ResumeLayout(false);
            this.NotificationsTabPage.ResumeLayout(false);
            this.NotificationsTabPage.PerformLayout();
            this.RelatedItemsTabPage.ResumeLayout(false);
            this.RelatedItemsTabPage.PerformLayout();
            this.ipaasJobsTabPage.ResumeLayout(false);
            this.Surveys.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.MainResourcesTabPage.ResumeLayout(false);
            this.MainResourcesTabPage.PerformLayout();
            this.Generic.ResumeLayout(false);
            this.Generic.PerformLayout();
            this.ipaasJobsTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabPage otherTab;
        private System.Windows.Forms.TabControl OtherTabControl;
        private System.Windows.Forms.TabPage JourneysTabPage;
        private System.Windows.Forms.Button JourneysSearchFiles;
        private System.Windows.Forms.TabPage NotificationsTabPage;
        private System.Windows.Forms.CheckBox NotificationsShowJsonCheckbox;
        private System.Windows.Forms.Button NotificationsFindButton;
        private System.Windows.Forms.TabPage ipaasJobsTabPage;
        private System.Windows.Forms.Button ipaasRunJobButton;
        private System.Windows.Forms.TabPage Surveys;
        private System.Windows.Forms.Button btnViewTemplates;
        private System.Windows.Forms.Button surveysUpsert;
        private System.Windows.Forms.Button getSurveys;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button DeleteSchemeButton;
        private System.Windows.Forms.Button ExportDataFile;
        private System.Windows.Forms.Button ViewSchemes;
        private System.Windows.Forms.Button btnUploadUdc;
        private System.Windows.Forms.Button btnViewUdc;
        private System.Windows.Forms.TabPage MainResourcesTabPage;
        private System.Windows.Forms.Button MainResource_BulkUploadButton;
        private System.Windows.Forms.Button MainResource_ExportAsyncButton;
        private System.Windows.Forms.CheckBox MainResource_ShowJsonCheckBox;
        private System.Windows.Forms.Button MainResource_FindButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox MainResources_ResourceToUse;
        private System.Windows.Forms.TabPage Generic;
        private System.Windows.Forms.Button btnViewItems;
        private System.Windows.Forms.Button btnViewAcounts;
        private System.Windows.Forms.Button btnSaveSelected;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.TabControl ipaasJobsTab;
        private System.Windows.Forms.Button Notifications_PostButton;
        private System.Windows.Forms.TabPage RelatedItemsTabPage;
        private System.Windows.Forms.Button RelatedItems_FindByKeyButton;
        private System.Windows.Forms.Button RelatedItems_FindButton;
        private System.Windows.Forms.CheckBox RelatedItems_ShowJsonCheckbox;
        private System.Windows.Forms.Button RelatedItems_UpsertButton;
        private System.Windows.Forms.Button RelatedItems_ExportAsyncButton;
        private System.Windows.Forms.Button RelatedItems_UploadFile_Button;
        private System.Windows.Forms.Button SaveAndUseToken_Button;
    }
}

