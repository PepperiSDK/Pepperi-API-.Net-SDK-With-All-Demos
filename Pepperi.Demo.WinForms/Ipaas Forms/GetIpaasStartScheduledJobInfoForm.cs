using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormApiDemo.Helpers;

namespace WinFormApiDemo.Ipaas_Forms
{
    public partial class GetIpaasStartScheduledJobInfoForm : Form
    {
        public int JobId { get; set; }
        public string JsonData { get; set; }
        public GetIpaasStartScheduledJobInfoForm()
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

            var jsonData = this.JsonDataTextBox.Text;

            if (!string.IsNullOrWhiteSpace(jsonData)) {
                var dataParsed = JsonParser.TryDeserialize<Newtonsoft.Json.Linq.JObject>(jsonData);

                if (dataParsed == null) {
                    MessageBox.Show($"JSON Data can't be parsed! Please leave it empty or put correct JSON there!");
                    return;
                }
            }

            this.JobId = jobId;
            this.JsonData = jsonData;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
