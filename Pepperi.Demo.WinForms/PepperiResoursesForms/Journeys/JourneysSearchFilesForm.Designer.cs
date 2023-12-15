namespace WinFormApiDemo.PepperiResoursesForms.Journeys
{
    partial class JourneysSearchFilesForm
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
            this.WhereTextBox = new System.Windows.Forms.TextBox();
            this.Where = new System.Windows.Forms.Label();
            this.ResponseTextBox = new System.Windows.Forms.TextBox();
            this.Response = new System.Windows.Forms.Label();
            this.SearchFilesButton = new System.Windows.Forms.Button();
            this.SearchJourneysButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WhereTextBox
            // 
            this.WhereTextBox.Location = new System.Drawing.Point(19, 38);
            this.WhereTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.WhereTextBox.Name = "WhereTextBox";
            this.WhereTextBox.Size = new System.Drawing.Size(954, 22);
            this.WhereTextBox.TabIndex = 0;
            // 
            // Where
            // 
            this.Where.AutoSize = true;
            this.Where.Location = new System.Drawing.Point(16, 18);
            this.Where.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Where.Name = "Where";
            this.Where.Size = new System.Drawing.Size(48, 16);
            this.Where.TabIndex = 1;
            this.Where.Text = "Where";
            // 
            // ResponseTextBox
            // 
            this.ResponseTextBox.BackColor = System.Drawing.Color.White;
            this.ResponseTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResponseTextBox.Location = new System.Drawing.Point(19, 98);
            this.ResponseTextBox.Multiline = true;
            this.ResponseTextBox.Name = "ResponseTextBox";
            this.ResponseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResponseTextBox.Size = new System.Drawing.Size(810, 366);
            this.ResponseTextBox.TabIndex = 2;
            // 
            // Response
            // 
            this.Response.AutoSize = true;
            this.Response.Location = new System.Drawing.Point(19, 79);
            this.Response.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Response.Name = "Response";
            this.Response.Size = new System.Drawing.Size(71, 16);
            this.Response.TabIndex = 3;
            this.Response.Text = "Response";
            // 
            // SearchFilesButton
            // 
            this.SearchFilesButton.Location = new System.Drawing.Point(835, 404);
            this.SearchFilesButton.Name = "SearchFilesButton";
            this.SearchFilesButton.Size = new System.Drawing.Size(139, 60);
            this.SearchFilesButton.TabIndex = 4;
            this.SearchFilesButton.Text = "Search Files";
            this.SearchFilesButton.UseVisualStyleBackColor = true;
            this.SearchFilesButton.Click += new System.EventHandler(this.SearchFilesButton_Click);
            // 
            // SearchJourneysButton
            // 
            this.SearchJourneysButton.Location = new System.Drawing.Point(834, 98);
            this.SearchJourneysButton.Name = "SearchJourneysButton";
            this.SearchJourneysButton.Size = new System.Drawing.Size(139, 60);
            this.SearchJourneysButton.TabIndex = 5;
            this.SearchJourneysButton.Text = "Search Journeys";
            this.SearchJourneysButton.UseVisualStyleBackColor = true;
            this.SearchJourneysButton.Click += new System.EventHandler(this.SearchJourneysButton_Click);
            // 
            // JourneysSearchFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 476);
            this.Controls.Add(this.SearchJourneysButton);
            this.Controls.Add(this.SearchFilesButton);
            this.Controls.Add(this.Response);
            this.Controls.Add(this.ResponseTextBox);
            this.Controls.Add(this.Where);
            this.Controls.Add(this.WhereTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "JourneysSearchFilesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "JourneysSearchFilesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox WhereTextBox;
        private System.Windows.Forms.Label Where;
        private System.Windows.Forms.TextBox ResponseTextBox;
        private System.Windows.Forms.Label Response;
        private System.Windows.Forms.Button SearchFilesButton;
        private System.Windows.Forms.Button SearchJourneysButton;
    }
}