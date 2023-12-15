namespace WinFormApiDemo.User_Defined_Collections_Forms
{
    partial class GR_ImportFileConfiguration
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
            this.overwriteObjectCheckBox = new System.Windows.Forms.CheckBox();
            this.overwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.NextButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.udcNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // overwriteObjectCheckBox
            // 
            this.overwriteObjectCheckBox.AutoSize = true;
            this.overwriteObjectCheckBox.Location = new System.Drawing.Point(82, 62);
            this.overwriteObjectCheckBox.Name = "overwriteObjectCheckBox";
            this.overwriteObjectCheckBox.Size = new System.Drawing.Size(144, 24);
            this.overwriteObjectCheckBox.TabIndex = 2;
            this.overwriteObjectCheckBox.Text = "Overwrite Object";
            this.overwriteObjectCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.overwriteObjectCheckBox.UseVisualStyleBackColor = true;
            // 
            // overwriteCheckBox
            // 
            this.overwriteCheckBox.AutoSize = true;
            this.overwriteCheckBox.Location = new System.Drawing.Point(82, 92);
            this.overwriteCheckBox.Name = "overwriteCheckBox";
            this.overwriteCheckBox.Size = new System.Drawing.Size(137, 24);
            this.overwriteCheckBox.TabIndex = 3;
            this.overwriteCheckBox.Text = "Overwrite Table";
            this.overwriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(196, 181);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(110, 34);
            this.NextButton.TabIndex = 4;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(12, 181);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(110, 34);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // udcNameLabel
            // 
            this.udcNameLabel.AutoSize = true;
            this.udcNameLabel.Location = new System.Drawing.Point(12, 9);
            this.udcNameLabel.Name = "udcNameLabel";
            this.udcNameLabel.Size = new System.Drawing.Size(0, 20);
            this.udcNameLabel.TabIndex = 6;
            // 
            // GR_ImportFileConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 227);
            this.Controls.Add(this.udcNameLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.overwriteCheckBox);
            this.Controls.Add(this.overwriteObjectCheckBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GR_ImportFileConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Upload Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox overwriteObjectCheckBox;
        private System.Windows.Forms.CheckBox overwriteCheckBox;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label udcNameLabel;
    }
}