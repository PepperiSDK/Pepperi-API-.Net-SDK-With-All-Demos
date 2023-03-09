using Pepperi.SDK.Model.Fixed.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApiDemo.Surveys_Forms
{
    public partial class surveysUpsertForm : Form
    {
        public string SurveyToUpsertJson { get; set; }

        public surveysUpsertForm()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var text = this.jsonUpsertText.Text;

            if (string.IsNullOrWhiteSpace(text)) {
                MessageBox.Show($"Empty or White Spaced Text! Should be JSON of Survey!");
                return;
            }

            if (!IsValidJson(text)) {
                MessageBox.Show($"Can't parse JSON! Please check if Survey is valid!");
                return;
            }

            this.SurveyToUpsertJson = text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private bool IsValidJson(string json) {
            try
            {
                Newtonsoft.Json.JsonConvert.DeserializeObject<Survey>(json);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}
