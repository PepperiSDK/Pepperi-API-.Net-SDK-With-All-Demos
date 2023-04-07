using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApiDemo.Ipaas_Forms
{
    public partial class GetIpaasScheduledJobIdForm : Form
    {
        public int JobId { get; set; }
        public GetIpaasScheduledJobIdForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            var jobIdString = this.JobIdTextBox.Text;

            var parsed = int.TryParse(jobIdString, out int jobId);

            if (!parsed) {
                MessageBox.Show($"Scheduled Job ID should be int number!");
                return;
            }

            if (jobId <= 0) {
                MessageBox.Show($"Scheduled Job ID should be positive int number (> 0)!");
                return;
            }

            this.JobId = jobId;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
