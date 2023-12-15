namespace WinFormApiDemo.General_Forms
{
    partial class GetRequestDataDynamicForm
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.WhereTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FieldsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OrderByTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PageSizeTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PageTextBox = new System.Windows.Forms.TextBox();
            this.IncludeNestedCheckBox = new System.Windows.Forms.CheckBox();
            this.IncludeDeletedCheckBox = new System.Windows.Forms.CheckBox();
            this.IsDistinctCheckBox = new System.Windows.Forms.CheckBox();
            this.FullModeCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClearInputsButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelButton.Location = new System.Drawing.Point(17, 270);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.NextButton.Location = new System.Drawing.Point(604, 270);
            this.NextButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(106, 34);
            this.NextButton.TabIndex = 14;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(17, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Where";
            // 
            // WhereTextBox
            // 
            this.WhereTextBox.Location = new System.Drawing.Point(114, 9);
            this.WhereTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.WhereTextBox.Name = "WhereTextBox";
            this.WhereTextBox.Size = new System.Drawing.Size(596, 27);
            this.WhereTextBox.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(17, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "Fields";
            // 
            // FieldsTextBox
            // 
            this.FieldsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FieldsTextBox.Location = new System.Drawing.Point(114, 69);
            this.FieldsTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FieldsTextBox.Name = "FieldsTextBox";
            this.FieldsTextBox.Size = new System.Drawing.Size(596, 26);
            this.FieldsTextBox.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(17, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "Order By";
            // 
            // OrderByTextBox
            // 
            this.OrderByTextBox.Enabled = false;
            this.OrderByTextBox.Location = new System.Drawing.Point(114, 39);
            this.OrderByTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OrderByTextBox.Name = "OrderByTextBox";
            this.OrderByTextBox.Size = new System.Drawing.Size(596, 27);
            this.OrderByTextBox.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(23, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "Page";
            // 
            // PageSizeTextBox
            // 
            this.PageSizeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PageSizeTextBox.Location = new System.Drawing.Point(114, 184);
            this.PageSizeTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PageSizeTextBox.Name = "PageSizeTextBox";
            this.PageSizeTextBox.Size = new System.Drawing.Size(126, 26);
            this.PageSizeTextBox.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(23, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "Page Size";
            // 
            // PageTextBox
            // 
            this.PageTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PageTextBox.Location = new System.Drawing.Point(114, 154);
            this.PageTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PageTextBox.Name = "PageTextBox";
            this.PageTextBox.Size = new System.Drawing.Size(126, 26);
            this.PageTextBox.TabIndex = 25;
            // 
            // IncludeNestedCheckBox
            // 
            this.IncludeNestedCheckBox.AutoSize = true;
            this.IncludeNestedCheckBox.Location = new System.Drawing.Point(30, 28);
            this.IncludeNestedCheckBox.Name = "IncludeNestedCheckBox";
            this.IncludeNestedCheckBox.Size = new System.Drawing.Size(77, 22);
            this.IncludeNestedCheckBox.TabIndex = 26;
            this.IncludeNestedCheckBox.Text = "Nested";
            this.IncludeNestedCheckBox.UseVisualStyleBackColor = true;
            // 
            // IncludeDeletedCheckBox
            // 
            this.IncludeDeletedCheckBox.AutoSize = true;
            this.IncludeDeletedCheckBox.Location = new System.Drawing.Point(30, 58);
            this.IncludeDeletedCheckBox.Name = "IncludeDeletedCheckBox";
            this.IncludeDeletedCheckBox.Size = new System.Drawing.Size(80, 22);
            this.IncludeDeletedCheckBox.TabIndex = 27;
            this.IncludeDeletedCheckBox.Text = "Deleted";
            this.IncludeDeletedCheckBox.UseVisualStyleBackColor = true;
            // 
            // IsDistinctCheckBox
            // 
            this.IsDistinctCheckBox.AutoSize = true;
            this.IsDistinctCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IsDistinctCheckBox.Location = new System.Drawing.Point(469, 187);
            this.IsDistinctCheckBox.Name = "IsDistinctCheckBox";
            this.IsDistinctCheckBox.Size = new System.Drawing.Size(94, 22);
            this.IsDistinctCheckBox.TabIndex = 28;
            this.IsDistinctCheckBox.Text = "Is Distinct";
            this.IsDistinctCheckBox.UseVisualStyleBackColor = true;
            // 
            // FullModeCheckBox
            // 
            this.FullModeCheckBox.AutoSize = true;
            this.FullModeCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullModeCheckBox.Location = new System.Drawing.Point(469, 156);
            this.FullModeCheckBox.Name = "FullModeCheckBox";
            this.FullModeCheckBox.Size = new System.Drawing.Size(95, 22);
            this.FullModeCheckBox.TabIndex = 29;
            this.FullModeCheckBox.Text = "Full Mode";
            this.FullModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IncludeNestedCheckBox);
            this.groupBox1.Controls.Add(this.IncludeDeletedCheckBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(297, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 100);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Include";
            // 
            // ClearInputsButton
            // 
            this.ClearInputsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearInputsButton.Location = new System.Drawing.Point(657, 154);
            this.ClearInputsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClearInputsButton.Name = "ClearInputsButton";
            this.ClearInputsButton.Size = new System.Drawing.Size(54, 48);
            this.ClearInputsButton.TabIndex = 31;
            this.ClearInputsButton.Text = "Clear Inputs";
            this.ClearInputsButton.UseVisualStyleBackColor = true;
            this.ClearInputsButton.Click += new System.EventHandler(this.ClearInputsButton_Click);
            // 
            // GetRequestDataDynamicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 316);
            this.Controls.Add(this.ClearInputsButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FullModeCheckBox);
            this.Controls.Add(this.IsDistinctCheckBox);
            this.Controls.Add(this.PageTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PageSizeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OrderByTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WhereTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FieldsTextBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.NextButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GetRequestDataDynamicForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please Fill Info";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox WhereTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FieldsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox OrderByTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PageSizeTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PageTextBox;
        private System.Windows.Forms.CheckBox IncludeNestedCheckBox;
        private System.Windows.Forms.CheckBox IncludeDeletedCheckBox;
        private System.Windows.Forms.CheckBox IsDistinctCheckBox;
        private System.Windows.Forms.CheckBox FullModeCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ClearInputsButton;
    }
}