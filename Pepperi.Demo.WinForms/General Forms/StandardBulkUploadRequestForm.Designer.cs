using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WinFormApiDemo.General_Forms
{
    partial class StandardBulkUploadRequestForm
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
            this.MainInfoLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.modelUpload_SaveZip_CheckBox = new System.Windows.Forms.CheckBox();
            this.modelUpload_Clear_Button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.modelUpload_Upload_Button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.modelUpload_FieldsToUpload_TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.modelUpload_UploadMethod_ComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.modelUpload_OverwriteMethod_ComboBox = new System.Windows.Forms.ComboBox();
            this.jsonDataTextBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.csvUpload_FileEncoding_ComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.csvUpload_Clear_Button = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.csvUpload_FilePathToStoreZipFile_TextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.csvUpload_Upload_Button = new System.Windows.Forms.Button();
            this.csvUpload_OverwriteMethod_ComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.csvUpload_SelectFile_Button = new System.Windows.Forms.Button();
            this.csvUpload_FilePath_TextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainInfoLabel
            // 
            this.MainInfoLabel.AutoSize = true;
            this.MainInfoLabel.Location = new System.Drawing.Point(22, 9);
            this.MainInfoLabel.Name = "MainInfoLabel";
            this.MainInfoLabel.Size = new System.Drawing.Size(734, 13);
            this.MainInfoLabel.TabIndex = 0;
            this.MainInfoLabel.Text = "This Form allows you to make bulk upload with 2 options: via existing csv file (n" +
    "eed to select it) and via model (need to put json and select upload methods)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 34);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 404);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.modelUpload_SaveZip_CheckBox);
            this.tabPage1.Controls.Add(this.modelUpload_Clear_Button);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.modelUpload_Upload_Button);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.modelUpload_FieldsToUpload_TextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.modelUpload_UploadMethod_ComboBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.modelUpload_OverwriteMethod_ComboBox);
            this.tabPage1.Controls.Add(this.jsonDataTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Via Model";
            // 
            // modelUpload_SaveZip_CheckBox
            // 
            this.modelUpload_SaveZip_CheckBox.AutoSize = true;
            this.modelUpload_SaveZip_CheckBox.Location = new System.Drawing.Point(625, 113);
            this.modelUpload_SaveZip_CheckBox.Name = "modelUpload_SaveZip_CheckBox";
            this.modelUpload_SaveZip_CheckBox.Size = new System.Drawing.Size(119, 17);
            this.modelUpload_SaveZip_CheckBox.TabIndex = 11;
            this.modelUpload_SaveZip_CheckBox.Text = "Save ZIP (For CSV)";
            this.modelUpload_SaveZip_CheckBox.UseVisualStyleBackColor = true;
            // 
            // modelUpload_Clear_Button
            // 
            this.modelUpload_Clear_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelUpload_Clear_Button.Location = new System.Drawing.Point(417, 6);
            this.modelUpload_Clear_Button.Name = "modelUpload_Clear_Button";
            this.modelUpload_Clear_Button.Size = new System.Drawing.Size(107, 23);
            this.modelUpload_Clear_Button.TabIndex = 10;
            this.modelUpload_Clear_Button.Text = "Clear (to default)";
            this.modelUpload_Clear_Button.UseVisualStyleBackColor = true;
            this.modelUpload_Clear_Button.Click += new System.EventHandler(this.modelUpload_Clear_Button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Model Data (JSON Array of Objects)";
            // 
            // modelUpload_Upload_Button
            // 
            this.modelUpload_Upload_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelUpload_Upload_Button.Location = new System.Drawing.Point(628, 318);
            this.modelUpload_Upload_Button.Name = "modelUpload_Upload_Button";
            this.modelUpload_Upload_Button.Size = new System.Drawing.Size(134, 54);
            this.modelUpload_Upload_Button.TabIndex = 8;
            this.modelUpload_Upload_Button.Text = "Upload";
            this.modelUpload_Upload_Button.UseVisualStyleBackColor = true;
            this.modelUpload_Upload_Button.Click += new System.EventHandler(this.modelUpload_Upload_Button_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(622, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 28);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fields To Upload (coma separated)";
            // 
            // modelUpload_FieldsToUpload_TextBox
            // 
            this.modelUpload_FieldsToUpload_TextBox.Location = new System.Drawing.Point(625, 180);
            this.modelUpload_FieldsToUpload_TextBox.MaxLength = 1000000;
            this.modelUpload_FieldsToUpload_TextBox.Multiline = true;
            this.modelUpload_FieldsToUpload_TextBox.Name = "modelUpload_FieldsToUpload_TextBox";
            this.modelUpload_FieldsToUpload_TextBox.Size = new System.Drawing.Size(134, 109);
            this.modelUpload_FieldsToUpload_TextBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(622, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Upload Method";
            // 
            // modelUpload_UploadMethod_ComboBox
            // 
            this.modelUpload_UploadMethod_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modelUpload_UploadMethod_ComboBox.FormattingEnabled = true;
            this.modelUpload_UploadMethod_ComboBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.modelUpload_UploadMethod_ComboBox.Items.AddRange(new object[] {
            "Json",
            "Zip"});
            this.modelUpload_UploadMethod_ComboBox.Location = new System.Drawing.Point(625, 71);
            this.modelUpload_UploadMethod_ComboBox.Name = "modelUpload_UploadMethod_ComboBox";
            this.modelUpload_UploadMethod_ComboBox.Size = new System.Drawing.Size(137, 21);
            this.modelUpload_UploadMethod_ComboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(622, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Overwrite Method";
            // 
            // modelUpload_OverwriteMethod_ComboBox
            // 
            this.modelUpload_OverwriteMethod_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modelUpload_OverwriteMethod_ComboBox.FormattingEnabled = true;
            this.modelUpload_OverwriteMethod_ComboBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.modelUpload_OverwriteMethod_ComboBox.Items.AddRange(new object[] {
            "none",
            "full",
            "selective"});
            this.modelUpload_OverwriteMethod_ComboBox.Location = new System.Drawing.Point(625, 31);
            this.modelUpload_OverwriteMethod_ComboBox.Name = "modelUpload_OverwriteMethod_ComboBox";
            this.modelUpload_OverwriteMethod_ComboBox.Size = new System.Drawing.Size(137, 21);
            this.modelUpload_OverwriteMethod_ComboBox.TabIndex = 2;
            // 
            // jsonDataTextBox
            // 
            this.jsonDataTextBox.Location = new System.Drawing.Point(9, 31);
            this.jsonDataTextBox.MaxLength = 1000000;
            this.jsonDataTextBox.Multiline = true;
            this.jsonDataTextBox.Name = "jsonDataTextBox";
            this.jsonDataTextBox.Size = new System.Drawing.Size(607, 341);
            this.jsonDataTextBox.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.csvUpload_FileEncoding_ComboBox);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.csvUpload_Clear_Button);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.csvUpload_FilePathToStoreZipFile_TextBox);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.csvUpload_Upload_Button);
            this.tabPage2.Controls.Add(this.csvUpload_OverwriteMethod_ComboBox);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.csvUpload_SelectFile_Button);
            this.tabPage2.Controls.Add(this.csvUpload_FilePath_TextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 378);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Via CSV File";
            // 
            // csvUpload_FileEncoding_ComboBox
            // 
            this.csvUpload_FileEncoding_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.csvUpload_FileEncoding_ComboBox.FormattingEnabled = true;
            this.csvUpload_FileEncoding_ComboBox.Location = new System.Drawing.Point(184, 145);
            this.csvUpload_FileEncoding_ComboBox.Name = "csvUpload_FileEncoding_ComboBox";
            this.csvUpload_FileEncoding_ComboBox.Size = new System.Drawing.Size(193, 21);
            this.csvUpload_FileEncoding_ComboBox.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(181, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "File Encoding";
            // 
            // csvUpload_Clear_Button
            // 
            this.csvUpload_Clear_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csvUpload_Clear_Button.Location = new System.Drawing.Point(9, 349);
            this.csvUpload_Clear_Button.Name = "csvUpload_Clear_Button";
            this.csvUpload_Clear_Button.Size = new System.Drawing.Size(107, 23);
            this.csvUpload_Clear_Button.TabIndex = 15;
            this.csvUpload_Clear_Button.Text = "Clear (to default)";
            this.csvUpload_Clear_Button.UseVisualStyleBackColor = true;
            this.csvUpload_Clear_Button.Click += new System.EventHandler(this.csvUpload_Clear_Button_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(404, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "File Path to Store Zip File (Should be .csv) (Used for testing - leave empty to n" +
    "ot use)";
            // 
            // csvUpload_FilePathToStoreZipFile_TextBox
            // 
            this.csvUpload_FilePathToStoreZipFile_TextBox.Location = new System.Drawing.Point(9, 87);
            this.csvUpload_FilePathToStoreZipFile_TextBox.Name = "csvUpload_FilePathToStoreZipFile_TextBox";
            this.csvUpload_FilePathToStoreZipFile_TextBox.Size = new System.Drawing.Size(668, 20);
            this.csvUpload_FilePathToStoreZipFile_TextBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Overwrite Method";
            // 
            // csvUpload_Upload_Button
            // 
            this.csvUpload_Upload_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csvUpload_Upload_Button.Location = new System.Drawing.Point(628, 318);
            this.csvUpload_Upload_Button.Name = "csvUpload_Upload_Button";
            this.csvUpload_Upload_Button.Size = new System.Drawing.Size(134, 54);
            this.csvUpload_Upload_Button.TabIndex = 11;
            this.csvUpload_Upload_Button.Text = "Upload";
            this.csvUpload_Upload_Button.UseVisualStyleBackColor = true;
            this.csvUpload_Upload_Button.Click += new System.EventHandler(this.csvUpload_Upload_Button_Click);
            // 
            // csvUpload_OverwriteMethod_ComboBox
            // 
            this.csvUpload_OverwriteMethod_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.csvUpload_OverwriteMethod_ComboBox.FormattingEnabled = true;
            this.csvUpload_OverwriteMethod_ComboBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.csvUpload_OverwriteMethod_ComboBox.Items.AddRange(new object[] {
            "none",
            "full",
            "selective"});
            this.csvUpload_OverwriteMethod_ComboBox.Location = new System.Drawing.Point(9, 145);
            this.csvUpload_OverwriteMethod_ComboBox.Name = "csvUpload_OverwriteMethod_ComboBox";
            this.csvUpload_OverwriteMethod_ComboBox.Size = new System.Drawing.Size(137, 21);
            this.csvUpload_OverwriteMethod_ComboBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "File Path to Use (should be .csv)";
            // 
            // csvUpload_SelectFile_Button
            // 
            this.csvUpload_SelectFile_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csvUpload_SelectFile_Button.Location = new System.Drawing.Point(683, 30);
            this.csvUpload_SelectFile_Button.Name = "csvUpload_SelectFile_Button";
            this.csvUpload_SelectFile_Button.Size = new System.Drawing.Size(79, 20);
            this.csvUpload_SelectFile_Button.TabIndex = 9;
            this.csvUpload_SelectFile_Button.Text = "Select File";
            this.csvUpload_SelectFile_Button.UseVisualStyleBackColor = true;
            this.csvUpload_SelectFile_Button.Click += new System.EventHandler(this.csvUpload_SelectFile_Button_Click);
            // 
            // csvUpload_FilePath_TextBox
            // 
            this.csvUpload_FilePath_TextBox.Location = new System.Drawing.Point(9, 30);
            this.csvUpload_FilePath_TextBox.Name = "csvUpload_FilePath_TextBox";
            this.csvUpload_FilePath_TextBox.Size = new System.Drawing.Size(668, 20);
            this.csvUpload_FilePath_TextBox.TabIndex = 2;
            // 
            // StandardBulkUploadRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.MainInfoLabel);
            this.Name = "StandardBulkUploadRequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BulkUpload Request";
            this.Load += new System.EventHandler(this.StandardBulkUploadRequestForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MainInfoLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox jsonDataTextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox modelUpload_OverwriteMethod_ComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox modelUpload_UploadMethod_ComboBox;
        private System.Windows.Forms.Button modelUpload_Upload_Button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox modelUpload_FieldsToUpload_TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button modelUpload_Clear_Button;
        private System.Windows.Forms.CheckBox modelUpload_SaveZip_CheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button csvUpload_SelectFile_Button;
        private System.Windows.Forms.TextBox csvUpload_FilePath_TextBox;
        private System.Windows.Forms.Button csvUpload_Upload_Button;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox csvUpload_FilePathToStoreZipFile_TextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox csvUpload_OverwriteMethod_ComboBox;
        private System.Windows.Forms.Button csvUpload_Clear_Button;
        private System.Windows.Forms.ComboBox csvUpload_FileEncoding_ComboBox;
        private System.Windows.Forms.Label label8;
    }
}