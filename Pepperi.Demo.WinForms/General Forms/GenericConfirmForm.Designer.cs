namespace WindowsFormsApp1.General_Forms
{
    partial class GenericConfirmForm
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
            this.NextButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.BasicTextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(274, 124);
            this.NextButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(80, 43);
            this.NextButton.TabIndex = 0;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(12, 124);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(88, 43);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // BasicTextLabel
            // 
            this.BasicTextLabel.AutoSize = true;
            this.BasicTextLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BasicTextLabel.Location = new System.Drawing.Point(20, 18);
            this.BasicTextLabel.Margin = new System.Windows.Forms.Padding(10, 9, 3, 0);
            this.BasicTextLabel.MaximumSize = new System.Drawing.Size(300, 0);
            this.BasicTextLabel.Name = "BasicTextLabel";
            this.BasicTextLabel.Size = new System.Drawing.Size(99, 18);
            this.BasicTextLabel.TabIndex = 2;
            this.BasicTextLabel.Text = "Are you sure?";
            // 
            // GenericConfirmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 178);
            this.Controls.Add(this.BasicTextLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.NextButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "GenericConfirmForm";
            this.Padding = new System.Windows.Forms.Padding(20, 18, 20, 9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please Confirm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label BasicTextLabel;
    }
}