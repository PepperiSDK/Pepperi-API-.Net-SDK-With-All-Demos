using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApiDemo.General_Forms
{
    public partial class GetJsonToUseForm : Form
    {
        public string JsonString { get; set; }
        private bool ShouldBeOnlyArray { get; set; }
        private bool ShouldBeOnlySingleObject { get; set; }
        private bool ShouldNotBeEmpty { get; set; } = true;

        public GetJsonToUseForm(string mainLabel, bool shouldBeOnlyArray = false, bool shouldBeOnlySingleObject = false, 
                                string defaultJson = "", bool shouldNotBeEmpty = true)
        {
            InitializeComponent();
            PrepareForm(mainLabel, shouldBeOnlyArray, shouldBeOnlySingleObject, defaultJson, shouldNotBeEmpty);
        }

        public void PrepareForm(string mainLabel, bool shouldBeOnlyArray = false, bool shouldBeOnlySingleObject = false, 
                                string defaultJson = "", bool shouldNotBeEmpty = true)
        {
            this.MainLabel.Text = mainLabel;
            this.ShouldBeOnlyArray = shouldBeOnlyArray;
            this.ShouldBeOnlySingleObject = shouldBeOnlySingleObject;
            this.ShouldNotBeEmpty = shouldNotBeEmpty;
            this.jsonTextBox.Text = defaultJson;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var text = this.jsonTextBox.Text;

            if (this.ShouldNotBeEmpty && string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show($"Empty or White Spaced Text! Should be JSON of Survey!");
                return;
            }

            if (!IsValidJson(text))
            {
                MessageBox.Show($"Can't parse JSON! Please check if json text is valid!");
                return;
            }

            if (this.ShouldBeOnlyArray || this.ShouldBeOnlySingleObject) {
                var trimmed = text.Trim();
                var isArray = trimmed.StartsWith("[") && trimmed.EndsWith("]");
                if (this.ShouldBeOnlyArray && !isArray) {
                    MessageBox.Show($"JSON should be array of objects ([{{...}}, {{...}}, ...])");
                    return;
                }
                if (this.ShouldBeOnlySingleObject && isArray)
                {
                    MessageBox.Show($"JSON should be single object (not array of objects)({{...}})");
                    return;
                }
            }

            this.JsonString = text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private bool IsValidJson(string json)
        {
            try
            {
                Newtonsoft.Json.JsonConvert.DeserializeObject<object>(json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
