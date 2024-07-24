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
using WinFormApiDemo.Helpers;

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
                var pageKey = this.PageKeyTextBox.Text;

                //var pageSizeText = this.PageSizeTextBox.Text;
                //int? pageSize = null;


                //if (!string.IsNullOrEmpty(pageSizeText)) {
                //    var pageSizeWasParsed = int.TryParse(pageSizeText, out var parsedPageSize);
                //    ValuesValidator.Validate(pageSizeWasParsed, $"Can't parse page size, should be number, but got \"{pageSizeText}\"", false);
                //    pageSize = parsedPageSize;
                //}

                pleaseWait.Show();
                Application.DoEvents();

                var searchedFiles = ApiClient.Journeys.SearchFiles(where, pageKey);

                var json = frmSample.PrettyJson(searchedFiles);
                this.ResponseTextBox.Text = json;

                pleaseWait.Close();
            }
            catch (Exception ex) {
                pleaseWait.Close();
                MessageBox.Show($"Error Occured. Message: {ex?.Message ?? "No Message"}");
            }
        }
        private void SearchAllFiles_Click(object sender, EventArgs e)
        {
            var pleaseWait = new PleaseWaitForm();
            try
            {
                var where = this.WhereTextBox.Text;

                pleaseWait.Show();
                Application.DoEvents();

                var searchedFiles = ApiClient.Journeys.SearchAllFiles(where);

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

        private void SearchJourneysButton_Click(object sender, EventArgs e)
        {
            var pleaseWait = new PleaseWaitForm();
            try
            {
                var where = this.WhereTextBox.Text;
                var pageKey = this.PageKeyTextBox.Text;

                pleaseWait.Show();
                Application.DoEvents();

                var searchedFiles = ApiClient.Journeys.SearchJourneys(where, pageKey);

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

        private void SearchAllJourneysButton_Click(object sender, EventArgs e)
        {
            var pleaseWait = new PleaseWaitForm();
            try
            {
                var where = this.WhereTextBox.Text;

                pleaseWait.Show();
                Application.DoEvents();

                var searchedFiles = ApiClient.Journeys.SearchAllJourneys(where);

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
