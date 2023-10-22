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
    public partial class GetExportAsyncRequestDataForm : Form
    {
        public string Fields { get; set; }
        public string Where { get; set; }
        public string OrderBy { get; set; }
        public bool IsDistinct { get; set; }
        public bool IncludeDeleted { get; set; }

        public GetExportAsyncRequestDataForm()
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
            var orderBy = this.OrderByTextBox.Text;
            var fields = this.FieldsTextBox.Text;
            var where = this.WhereTextBox.Text;

            this.OrderBy = string.IsNullOrWhiteSpace(orderBy) ? null : orderBy;
            this.Fields = string.IsNullOrWhiteSpace(fields) ? null : fields;
            this.Where = string.IsNullOrWhiteSpace(where) ? null : where;

            this.IsDistinct = this.IsDistinctCheckbox.Checked;
            this.IncludeDeleted = this.IncludeDeletedCheckbox.Checked;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ResetToDefaultButton_Click(object sender, EventArgs e)
        {
            this.OrderByTextBox.ResetText();
            this.FieldsTextBox.ResetText();
            this.WhereTextBox.ResetText();

            this.IsDistinctCheckbox.Checked = false;
            this.IncludeDeletedCheckbox.Checked = false;
        }

    }
}
