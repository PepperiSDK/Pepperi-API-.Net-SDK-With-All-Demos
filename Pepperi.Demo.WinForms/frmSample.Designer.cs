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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Generic = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DeleteSchemeButton = new System.Windows.Forms.Button();
            this.ExportDataFile = new System.Windows.Forms.Button();
            this.ViewSchemes = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.Generic.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnViewItems
            // 
            this.btnViewItems.Location = new System.Drawing.Point(7, 6);
            this.btnViewItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewItems.Name = "btnViewItems";
            this.btnViewItems.Size = new System.Drawing.Size(155, 64);
            this.btnViewItems.TabIndex = 0;
            this.btnViewItems.Text = "View Items";
            this.btnViewItems.UseVisualStyleBackColor = true;
            this.btnViewItems.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 185);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1155, 389);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnViewAcounts
            // 
            this.btnViewAcounts.Location = new System.Drawing.Point(167, 6);
            this.btnViewAcounts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewAcounts.Name = "btnViewAcounts";
            this.btnViewAcounts.Size = new System.Drawing.Size(205, 64);
            this.btnViewAcounts.TabIndex = 2;
            this.btnViewAcounts.Text = "View Accounts (Customers)";
            this.btnViewAcounts.UseVisualStyleBackColor = true;
            this.btnViewAcounts.Click += new System.EventHandler(this.btnViewAcounts_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Location = new System.Drawing.Point(537, 6);
            this.btnSaveAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(155, 64);
            this.btnSaveAll.TabIndex = 3;
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnSaveSelected
            // 
            this.btnSaveSelected.Location = new System.Drawing.Point(377, 6);
            this.btnSaveSelected.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveSelected.Name = "btnSaveSelected";
            this.btnSaveSelected.Size = new System.Drawing.Size(155, 64);
            this.btnSaveSelected.TabIndex = 4;
            this.btnSaveSelected.Text = "Save Selected";
            this.btnSaveSelected.UseVisualStyleBackColor = true;
            this.btnSaveSelected.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(219, 46);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect And Use This Token";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(333, 12);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(263, 22);
            this.txtEmail.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(333, 39);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(263, 22);
            this.txtPassword.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(675, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Company Token";
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(809, 12);
            this.txtToken.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(357, 22);
            this.txtToken.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(675, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Filter Query";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(809, 39);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(357, 22);
            this.txtFilter.TabIndex = 12;
            this.txtFilter.Text = "Name Like \'%A%\'";
            // 
            // btnUploadUdc
            // 
            this.btnUploadUdc.Location = new System.Drawing.Point(101, 7);
            this.btnUploadUdc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUploadUdc.Name = "btnUploadUdc";
            this.btnUploadUdc.Size = new System.Drawing.Size(121, 63);
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
            this.btnViewUdc.Location = new System.Drawing.Point(7, 6);
            this.btnViewUdc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewUdc.Name = "btnViewUdc";
            this.btnViewUdc.Size = new System.Drawing.Size(89, 64);
            this.btnViewUdc.TabIndex = 16;
            this.btnViewUdc.Text = "View Data";
            this.btnViewUdc.UseVisualStyleBackColor = true;
            this.btnViewUdc.Click += new System.EventHandler(this.btnViewUdc_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Generic);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(16, 69);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1149, 110);
            this.tabControl1.TabIndex = 18;
            // 
            // Generic
            // 
            this.Generic.BackColor = System.Drawing.SystemColors.Control;
            this.Generic.Controls.Add(this.btnViewItems);
            this.Generic.Controls.Add(this.btnViewAcounts);
            this.Generic.Controls.Add(this.btnSaveSelected);
            this.Generic.Controls.Add(this.btnSaveAll);
            this.Generic.Location = new System.Drawing.Point(4, 25);
            this.Generic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Generic.Name = "Generic";
            this.Generic.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Generic.Size = new System.Drawing.Size(1141, 81);
            this.Generic.TabIndex = 0;
            this.Generic.Text = "Generic";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.DeleteSchemeButton);
            this.tabPage2.Controls.Add(this.ExportDataFile);
            this.tabPage2.Controls.Add(this.ViewSchemes);
            this.tabPage2.Controls.Add(this.btnUploadUdc);
            this.tabPage2.Controls.Add(this.btnViewUdc);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1141, 81);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "User Defined Collections";
            // 
            // DeleteSchemeButton
            // 
            this.DeleteSchemeButton.Location = new System.Drawing.Point(1009, 6);
            this.DeleteSchemeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeleteSchemeButton.Name = "DeleteSchemeButton";
            this.DeleteSchemeButton.Size = new System.Drawing.Size(119, 63);
            this.DeleteSchemeButton.TabIndex = 19;
            this.DeleteSchemeButton.Text = "Delete Scheme";
            this.DeleteSchemeButton.UseVisualStyleBackColor = true;
            this.DeleteSchemeButton.Click += new System.EventHandler(this.DeleteSchemeButton_Click);
            // 
            // ExportDataFile
            // 
            this.ExportDataFile.Location = new System.Drawing.Point(229, 7);
            this.ExportDataFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ExportDataFile.Name = "ExportDataFile";
            this.ExportDataFile.Size = new System.Drawing.Size(136, 63);
            this.ExportDataFile.TabIndex = 18;
            this.ExportDataFile.Text = "Export Data (File)";
            this.ExportDataFile.UseVisualStyleBackColor = true;
            this.ExportDataFile.Click += new System.EventHandler(this.ExportDataFile_Click);
            // 
            // ViewSchemes
            // 
            this.ViewSchemes.Location = new System.Drawing.Point(883, 6);
            this.ViewSchemes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ViewSchemes.Name = "ViewSchemes";
            this.ViewSchemes.Size = new System.Drawing.Size(119, 63);
            this.ViewSchemes.TabIndex = 17;
            this.ViewSchemes.Text = "View Schemes";
            this.ViewSchemes.UseVisualStyleBackColor = true;
            this.ViewSchemes.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 587);
            this.Controls.Add(this.tabControl1);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmSample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pepperi API Demo";
            this.Load += new System.EventHandler(this.frmSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.Generic.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Generic;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button ViewSchemes;
        private System.Windows.Forms.Button ExportDataFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button DeleteSchemeButton;
    }
}

