namespace WinFormApiDemo.User_Defined_Collections_Forms
{
    partial class udcExportFileConfiguration
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
            this.FormatListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FieldsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.WhereTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ExcludeKeysTextBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FormatListBox
            // 
            this.FormatListBox.FormattingEnabled = true;
            this.FormatListBox.ItemHeight = 20;
            this.FormatListBox.Items.AddRange(new object[] {
            "CSV",
            "JSON"});
            this.FormatListBox.Location = new System.Drawing.Point(512, 22);
            this.FormatListBox.Name = "FormatListBox";
            this.FormatListBox.Size = new System.Drawing.Size(76, 44);
            this.FormatListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select File Format (Extension)";
            // 
            // FieldsTextBox
            // 
            this.FieldsTextBox.Location = new System.Drawing.Point(173, 115);
            this.FieldsTextBox.Name = "FieldsTextBox";
            this.FieldsTextBox.Size = new System.Drawing.Size(415, 26);
            this.FieldsTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fields";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Where";
            // 
            // WhereTextBox
            // 
            this.WhereTextBox.Location = new System.Drawing.Point(173, 144);
            this.WhereTextBox.Name = "WhereTextBox";
            this.WhereTextBox.Size = new System.Drawing.Size(415, 26);
            this.WhereTextBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Excluded Keys";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // ExcludeKeysTextBox
            // 
            this.ExcludeKeysTextBox.Location = new System.Drawing.Point(173, 173);
            this.ExcludeKeysTextBox.Name = "ExcludeKeysTextBox";
            this.ExcludeKeysTextBox.Size = new System.Drawing.Size(415, 26);
            this.ExcludeKeysTextBox.TabIndex = 6;
            this.ExcludeKeysTextBox.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelButton.Location = new System.Drawing.Point(390, 231);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(93, 34);
            this.CancelButton.TabIndex = 11;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NextButton.Location = new System.Drawing.Point(489, 231);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(99, 34);
            this.NextButton.TabIndex = 10;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // udcExportFileConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 277);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ExcludeKeysTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WhereTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FieldsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FormatListBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "udcExportFileConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UDC Export File Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox FormatListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FieldsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox WhereTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ExcludeKeysTextBox;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button NextButton;
    }
}