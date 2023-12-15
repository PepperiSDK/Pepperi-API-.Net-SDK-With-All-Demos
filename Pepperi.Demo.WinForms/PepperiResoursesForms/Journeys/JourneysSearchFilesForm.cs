using Pepperi.SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace WinFormApiDemo.PepperiResoursesForms.Journeys
{
    public partial class JourneysSearchFilesForm : Form
    {
        private ApiClient ApiClient { get; set; }
        public JourneysSearchFilesForm(ApiClient apiClient)
        {
            InitializeComponent();

            this.WhereTextBox.Text = $"ModificationDateTime>'{DateTime.UtcNow.AddHours(-24).ToString("o")}'";

            this.ApiClient = apiClient;
        }

        private void SearchFilesButton_Click(object sender, EventArgs e)
        {
            var pleaseWait = new PleaseWaitForm();
            try {
                var where = this.WhereTextBox.Text;

                pleaseWait.Show();
                Application.DoEvents();

                var searchedFiles = ApiClient.Journeys.SearchFiles(where);

                var json = frmSample.PrettyJson(searchedFiles);
                this.ResponseTextBox.Text = json;

                pleaseWait.Close();
            }
            catch (Exception ex) {
                pleaseWait.Close();
                MessageBox.Show($"Error Occured. Message: {ex?.Message ?? "No Message"}");
            }
        }

        private void SearchJourneysButton_Click(object sender, EventArgs e)
        {
            var pleaseWait = new PleaseWaitForm();
            try
            {
                var where = this.WhereTextBox.Text;

                pleaseWait.Show();
                Application.DoEvents();

                var searchedFiles = ApiClient.Journeys.SearchJourneys(where);

                var json = frmSample.PrettyJson(searchedFiles);
                this.ResponseTextBox.Text = json;

                pleaseWait.Close();
            }
            catch (Exception ex)
            {
                pleaseWait.Close();
                MessageBox.Show($"Error Occured. Message: {ex?.Message ?? "No Message"}");
            }
        }
    }
}
