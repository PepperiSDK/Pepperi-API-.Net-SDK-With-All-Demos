namespace WinFormApiDemo.Ipaas_Forms
{
    partial class IpaasRunJobResultForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.JsonUrl = new System.Windows.Forms.TextBox();
            this.CsvUrl = new System.Windows.Forms.TextBox();
            this.CsvUrlCopyButton = new System.Windows.Forms.Button();
            this.JsonUrlCopyButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.LoadData = new System.Windows.Forms.Button();
            this.SaveAsFileJson = new System.Windows.Forms.Button();
            this.SaveAsFileCsv = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "JSON URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "CSV URL";
            // 
            // JsonUrl
            // 
            this.JsonUrl.Location = new System.Drawing.Point(118, 27);
            this.JsonUrl.Name = "JsonUrl";
            this.JsonUrl.Size = new System.Drawing.Size(412, 26);
            this.JsonUrl.TabIndex = 2;
            // 
            // CsvUrl
            // 
            this.CsvUrl.Location = new System.Drawing.Point(118, 67);
            this.CsvUrl.Name = "CsvUrl";
            this.CsvUrl.Size = new System.Drawing.Size(412, 26);
            this.CsvUrl.TabIndex = 3;
            // 
            // CsvUrlCopyButton
            // 
            this.CsvUrlCopyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CsvUrlCopyButton.Location = new System.Drawing.Point(539, 67);
            this.CsvUrlCopyButton.Name = "CsvUrlCopyButton";
            this.CsvUrlCopyButton.Size = new System.Drawing.Size(61, 26);
            this.CsvUrlCopyButton.TabIndex = 4;
            this.CsvUrlCopyButton.Text = "Copy";
            this.CsvUrlCopyButton.UseVisualStyleBackColor = true;
            this.CsvUrlCopyButton.Click += new System.EventHandler(this.CsvUrlCopyButton_Click);
            // 
            // JsonUrlCopyButton
            // 
            this.JsonUrlCopyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.JsonUrlCopyButton.Location = new System.Drawing.Point(539, 27);
            this.JsonUrlCopyButton.Name = "JsonUrlCopyButton";
            this.JsonUrlCopyButton.Size = new System.Drawing.Size(61, 26);
            this.JsonUrlCopyButton.TabIndex = 5;
            this.JsonUrlCopyButton.Text = "Copy";
            this.JsonUrlCopyButton.UseVisualStyleBackColor = true;
            this.JsonUrlCopyButton.Click += new System.EventHandler(this.JsonUrlCopyButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseButton.Location = new System.Drawing.Point(23, 153);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(93, 34);
            this.CloseButton.TabIndex = 17;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // LoadData
            // 
            this.LoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadData.Location = new System.Drawing.Point(572, 153);
            this.LoadData.Name = "LoadData";
            this.LoadData.Size = new System.Drawing.Size(137, 34);
            this.LoadData.TabIndex = 18;
            this.LoadData.Text = "Load To Grid";
            this.LoadData.UseVisualStyleBackColor = true;
            this.LoadData.Click += new System.EventHandler(this.LoadData_Click);
            // 
            // SaveAsFileJson
            // 
            this.SaveAsFileJson.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveAsFileJson.Location = new System.Drawing.Point(606, 27);
            this.SaveAsFileJson.Name = "SaveAsFileJson";
            this.SaveAsFileJson.Size = new System.Drawing.Size(103, 26);
            this.SaveAsFileJson.TabIndex = 19;
            this.SaveAsFileJson.Text = "Save as File";
            this.SaveAsFileJson.UseVisualStyleBackColor = true;
            this.SaveAsFileJson.Click += new System.EventHandler(this.SaveAsFileJson_Click);
            // 
            // SaveAsFileCsv
            // 
            this.SaveAsFileCsv.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveAsFileCsv.Location = new System.Drawing.Point(606, 67);
            this.SaveAsFileCsv.Name = "SaveAsFileCsv";
            this.SaveAsFileCsv.Size = new System.Drawing.Size(103, 26);
            this.SaveAsFileCsv.TabIndex = 20;
            this.SaveAsFileCsv.Text = "Save as File";
            this.SaveAsFileCsv.UseVisualStyleBackColor = true;
            this.SaveAsFileCsv.Click += new System.EventHandler(this.SaveAsFileCsv_Click);
            // 
            // IpaasRunJobResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 199);
            this.Controls.Add(this.SaveAsFileCsv);
            this.Controls.Add(this.SaveAsFileJson);
            this.Controls.Add(this.LoadData);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.JsonUrlCopyButton);
            this.Controls.Add(this.CsvUrlCopyButton);
            this.Controls.Add(this.CsvUrl);
            this.Controls.Add(this.JsonUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "IpaasRunJobResultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Run Job Result";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox JsonUrl;
        private System.Windows.Forms.TextBox CsvUrl;
        private System.Windows.Forms.Button CsvUrlCopyButton;
        private System.Windows.Forms.Button JsonUrlCopyButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button LoadData;
        private System.Windows.Forms.Button SaveAsFileJson;
        private System.Windows.Forms.Button SaveAsFileCsv;
    }
}