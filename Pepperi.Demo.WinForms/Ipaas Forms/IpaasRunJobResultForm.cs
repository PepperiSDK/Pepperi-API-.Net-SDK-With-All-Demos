using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormApiDemo.Helpers;

namespace WinFormApiDemo.Ipaas_Forms
{
    public partial class IpaasRunJobResultForm : Form
    {
        public System.Windows.Forms.DataGridView dataGridView { get; set; }
        public IpaasRunJobResultForm()
        {
            InitializeComponent();
        }

        public void SetUrls(Pepperi.SDK.Model.Ipaas.IpaasDataUrls urls) {
            CsvUrl.Text = urls?.CsvUrl ?? "";
            JsonUrl.Text = urls?.JsonUrl ?? "";
        }

        private void CsvUrlCopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.CsvUrl.Text);
        }

        private void JsonUrlCopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.JsonUrl.Text);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void LoadData_Click(object sender, EventArgs e)
        {
            try {
                var url = this.JsonUrl.Text;
                if (string.IsNullOrWhiteSpace(url)) {
                    MessageBox.Show($"JSON Url is empty!");
                    return; 
                }
                var result = LoadJson(url);
                var data = JsonParser.Deserialize<object>(result);
                dataGridView.DataSource = data;
            } catch (Exception exp) {
                MessageBox.Show($"Error! Message - {exp?.Message ?? "No Message"}");
            }
        }

        private string LoadJson(string url) {
            var httpClient = new HttpClient() { };
            httpClient.Timeout = new TimeSpan(0, 5, 0);// by default wait 5 minutes
            HttpResponseMessage HttpResponseMessage = httpClient.GetAsync(url).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            return body;
        }

        private byte[] LoadCsv(string url)
        {
            var httpClient = new HttpClient() { };
            httpClient.Timeout = new TimeSpan(0, 5, 0);// by default wait 5 minutes
            HttpResponseMessage HttpResponseMessage = httpClient.GetAsync(url).Result;
            var result = HttpResponseMessage.Content.ReadAsByteArrayAsync().Result;
            return result;
        }

        private string SelectFolder() {
            string folder = null;
            using(var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    folder = fbd.SelectedPath;
                }
            }
            return folder;
        }

        private void SaveAsFileJson_Click(object sender, EventArgs e)
        {
            try
            {
                var url = this.JsonUrl.Text;
                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show($"JSON Url is empty!");
                    return;
                }

                var folderPath = SelectFolder();
                if (folderPath == null) return;

                var datetimeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).Replace(":", "_");
                var filePath = $"{folderPath}\\ipaasJobResult_{datetimeStamp}.json";
                var data = LoadJson(url);
                File.WriteAllText(filePath, data);
                MessageBox.Show($"Saved to {filePath}");
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error! Message - {exp?.Message ?? "No Message"}");
            }
        }

        private void SaveAsFileCsv_Click(object sender, EventArgs e)
        {
            try
            {
                var url = this.CsvUrl.Text;
                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show($"Csv Url is empty!");
                    return;
                }

                var folderPath = SelectFolder();
                if (folderPath == null) return;

                var datetimeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).Replace(":", "_");
                var filePath = $"{folderPath}\\ipaasJobResult_{datetimeStamp}.csv";
                var data = LoadCsv(url);
                File.WriteAllBytes(filePath, data);
                MessageBox.Show($"Saved to {filePath}");
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error! Message - {exp?.Message ?? "No Message"}");
            }
        }
    }
}
