namespace WinFormApiDemo.Ipaas_Forms
{
    partial class GetIpaasStartScheduledJobInfoForm
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
            this.RunButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.JobIdTextBox = new System.Windows.Forms.TextBox();
            this.JsonDataTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelButton.Location = new System.Drawing.Point(12, 326);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(93, 34);
            this.CancelButton.TabIndex = 16;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RunButton.Location = new System.Drawing.Point(306, 326);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(99, 34);
            this.RunButton.TabIndex = 17;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(8, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Scheduled Job ID to Run";
            // 
            // JobIdTextBox
            // 
            this.JobIdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.JobIdTextBox.Location = new System.Drawing.Point(218, 25);
            this.JobIdTextBox.MaxLength = 20;
            this.JobIdTextBox.Name = "JobIdTextBox";
            this.JobIdTextBox.Size = new System.Drawing.Size(187, 26);
            this.JobIdTextBox.TabIndex = 18;
            // 
            // JsonDataTextBox
            // 
            this.JsonDataTextBox.Location = new System.Drawing.Point(12, 89);
            this.JsonDataTextBox.MaxLength = 200000;
            this.JsonDataTextBox.Multiline = true;
            this.JsonDataTextBox.Name = "JsonDataTextBox";
            this.JsonDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.JsonDataTextBox.Size = new System.Drawing.Size(393, 231);
            this.JsonDataTextBox.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(8, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "Dynamic Data (JSON)";
            // 
            // GetIpaasStartScheduledJobInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 369);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.JsonDataTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.JobIdTextBox);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.CancelButton);
            this.Name = "GetIpaasStartScheduledJobInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please Fill Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox JobIdTextBox;
        private System.Windows.Forms.TextBox JsonDataTextBox;
        private System.Windows.Forms.Label label1;
    }
}