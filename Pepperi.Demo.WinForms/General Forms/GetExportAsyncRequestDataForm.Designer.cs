namespace WinFormApiDemo.General_Forms
{
    partial class GetExportAsyncRequestDataForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.WhereTextBox = new System.Windows.Forms.TextBox();
            this.OrderByLabel = new System.Windows.Forms.Label();
            this.OrderByTextBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FieldsTextBox = new System.Windows.Forms.TextBox();
            this.IncludeDeletedCheckbox = new System.Windows.Forms.CheckBox();
            this.IsDistinctCheckbox = new System.Windows.Forms.CheckBox();
            this.ResetToDefaultButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Where";
            // 
            // WhereTextBox
            // 
            this.WhereTextBox.Location = new System.Drawing.Point(91, 21);
            this.WhereTextBox.Name = "WhereTextBox";
            this.WhereTextBox.Size = new System.Drawing.Size(602, 26);
            this.WhereTextBox.TabIndex = 10;
            // 
            // OrderByLabel
            // 
            this.OrderByLabel.AutoSize = true;
            this.OrderByLabel.Location = new System.Drawing.Point(12, 53);
            this.OrderByLabel.Name = "OrderByLabel";
            this.OrderByLabel.Size = new System.Drawing.Size(71, 20);
            this.OrderByLabel.TabIndex = 9;
            this.OrderByLabel.Text = "Order By";
            // 
            // OrderByTextBox
            // 
            this.OrderByTextBox.Location = new System.Drawing.Point(91, 53);
            this.OrderByTextBox.Name = "OrderByTextBox";
            this.OrderByTextBox.Size = new System.Drawing.Size(602, 26);
            this.OrderByTextBox.TabIndex = 8;
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelButton.Location = new System.Drawing.Point(17, 245);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(92, 34);
            this.CancelButton.TabIndex = 15;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NextButton.Location = new System.Drawing.Point(595, 245);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(98, 34);
            this.NextButton.TabIndex = 14;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Fields";
            // 
            // FieldsTextBox
            // 
            this.FieldsTextBox.Location = new System.Drawing.Point(91, 85);
            this.FieldsTextBox.Name = "FieldsTextBox";
            this.FieldsTextBox.Size = new System.Drawing.Size(602, 26);
            this.FieldsTextBox.TabIndex = 16;
            // 
            // IncludeDeletedCheckbox
            // 
            this.IncludeDeletedCheckbox.AutoSize = true;
            this.IncludeDeletedCheckbox.Location = new System.Drawing.Point(17, 141);
            this.IncludeDeletedCheckbox.Name = "IncludeDeletedCheckbox";
            this.IncludeDeletedCheckbox.Size = new System.Drawing.Size(140, 24);
            this.IncludeDeletedCheckbox.TabIndex = 20;
            this.IncludeDeletedCheckbox.Text = "Include Deleted";
            this.IncludeDeletedCheckbox.UseVisualStyleBackColor = true;
            // 
            // IsDistinctCheckbox
            // 
            this.IsDistinctCheckbox.AutoSize = true;
            this.IsDistinctCheckbox.Location = new System.Drawing.Point(211, 141);
            this.IsDistinctCheckbox.Name = "IsDistinctCheckbox";
            this.IsDistinctCheckbox.Size = new System.Drawing.Size(98, 24);
            this.IsDistinctCheckbox.TabIndex = 21;
            this.IsDistinctCheckbox.Text = "Is Distinct";
            this.IsDistinctCheckbox.UseVisualStyleBackColor = true;
            // 
            // ResetToDefaultButton
            // 
            this.ResetToDefaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResetToDefaultButton.Location = new System.Drawing.Point(590, 135);
            this.ResetToDefaultButton.Name = "ResetToDefaultButton";
            this.ResetToDefaultButton.Size = new System.Drawing.Size(103, 34);
            this.ResetToDefaultButton.TabIndex = 22;
            this.ResetToDefaultButton.Text = "Clear Inputs";
            this.ResetToDefaultButton.UseVisualStyleBackColor = true;
            this.ResetToDefaultButton.Click += new System.EventHandler(this.ResetToDefaultButton_Click);
            // 
            // GetExportAsyncRequestDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 291);
            this.Controls.Add(this.ResetToDefaultButton);
            this.Controls.Add(this.IsDistinctCheckbox);
            this.Controls.Add(this.IncludeDeletedCheckbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FieldsTextBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WhereTextBox);
            this.Controls.Add(this.OrderByLabel);
            this.Controls.Add(this.OrderByTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GetExportAsyncRequestDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please Fill Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox WhereTextBox;
        private System.Windows.Forms.Label OrderByLabel;
        private System.Windows.Forms.TextBox OrderByTextBox;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FieldsTextBox;
        private System.Windows.Forms.CheckBox IncludeDeletedCheckbox;
        private System.Windows.Forms.CheckBox IsDistinctCheckbox;
        private System.Windows.Forms.Button ResetToDefaultButton;
    }
}