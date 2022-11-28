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
    public partial class udcExportFileConfiguration : Form
    {
        public string Format { get; set; }
        public string Fields { get; set; }
        public string Where { get; set; }
        public IEnumerable<string> ExcludedKeys { get; set; }
        public bool IncludeDeleted { get; set; }
        public udcExportFileConfiguration()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var fields = this.FieldsTextBox.Text;
            var where = this.WhereTextBox.Text;
            var format = (string)this.FormatListBox.SelectedItem;
            var excludedKeys = this.ExcludeKeysTextBox.Text;
            //var includeDeleted = this.IncludeDeletedCheckbox.Checked;

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
            //this.IncludeDeleted = includeDeleted;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
