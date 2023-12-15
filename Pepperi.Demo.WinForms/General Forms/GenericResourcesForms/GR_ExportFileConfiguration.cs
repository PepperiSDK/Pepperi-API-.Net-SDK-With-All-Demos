using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApiDemo.User_Defined_Collections_Forms
{
    public partial class GR_ExportFileConfiguration : Form
    {
        public string Format { get; set; }
        public string Fields { get; set; }
        public string Where { get; set; }
        public IEnumerable<string> ExcludedKeys { get; set; }

        public string LastOpenedFor { get; set; }

        public GR_ExportFileConfiguration()
        {
            InitializeComponent();
        }

        public void PrepareFrom(string openFor)
        {
            if (LastOpenedFor != openFor)
            {
                ResetInputsToDefault();
                LastOpenedFor = openFor;
            }
        }

        private void ResetInputsToDefault()
        {
            this.FieldsTextBox.Text = "";
            this.WhereTextBox.Text = "";
            this.ExcludeKeysTextBox.Text = "";
            this.FormatListBox.SelectedItem = null;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var fields = this.FieldsTextBox.Text;
            var where = this.WhereTextBox.Text;
            var format = (string)this.FormatListBox.SelectedItem;
            var excludedKeys = this.ExcludeKeysTextBox.Text;

            if (string.IsNullOrWhiteSpace(format)) {
                MessageBox.Show("Select Extension please!");
                return;
            }

            if (format != "CSV" && format != "JSON")
            {
                MessageBox.Show("Incorrect Extension!");
                return;
            }

            this.Fields = string.IsNullOrWhiteSpace(fields) ? null : fields;
            this.Where = string.IsNullOrWhiteSpace(where) ? null : where;
            this.Format = format == "CSV" ? "csv" : "json";
            this.ExcludedKeys = string.IsNullOrWhiteSpace(excludedKeys) ? null : excludedKeys.Split(',');

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void ClearInputsButton_Click(object sender, EventArgs e) {
            ResetInputsToDefault();
        }
    }

}
