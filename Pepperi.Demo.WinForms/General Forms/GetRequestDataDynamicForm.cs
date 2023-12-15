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
    public partial class GetRequestDataDynamicForm : Form
    {
        public string Fields { get; set; }
        public string Where { get; set; }
        public string OrderBy { get; set; }


        public int? Page { get; set; }
        public int? PageSize { get; set; }


        public bool? IncludeDeleted { get; set; }
        public bool? IncludeNested { get; set; }
        public bool? FullMode { get; set; }
        public bool? IsDistinct { get; set; }

        public string LastOpenedFor { get; set; }

        public GetRequestDataDynamicForm()
        {
            InitializeComponent();
        }

        public void PrepareFrom(GetRequestDataDynamicFormPrepareParams request) {
            if (LastOpenedFor != request.OpenFor) {
                ResetInputsToDefault();
                LastOpenedFor = request.OpenFor;
            }

            this.FieldsTextBox.Enabled = request.UseFieldsInput;
            this.WhereTextBox.Enabled = request.UseWhereInput;
            this.OrderByTextBox.Enabled = request.UseOrderByInput;

            this.PageTextBox.Enabled = request.UsePageInput;
            this.PageSizeTextBox.Enabled = request.UsePageSizeInput;

            this.IncludeDeletedCheckBox.Enabled = request.UseIncludeDeletedCheckBox;
            this.IncludeNestedCheckBox.Enabled = request.UseIncludeNestedCheckBox;
            this.FullModeCheckBox.Enabled = request.UseFullModeCheckBox;
            this.IsDistinctCheckBox.Enabled = request.UseIsDistinctCheckBox;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var fields = this.FieldsTextBox.Text;
            var where = this.WhereTextBox.Text;
            var orderBy = this.OrderByTextBox.Text;

            var page = this.PageTextBox.Text;
            var pageSize = this.PageSizeTextBox.Text;

            this.Fields = string.IsNullOrWhiteSpace(fields) ? null : fields;
            this.Where = string.IsNullOrWhiteSpace(where) ? null : where;
            this.OrderBy = string.IsNullOrWhiteSpace(orderBy) ? null : orderBy;

            if (!string.IsNullOrWhiteSpace(page))
            {
                var pageWasParsed = int.TryParse(page, out var parsedPage);
                if (!pageWasParsed) {
                    MessageBox.Show("Can't parse Page param! It should be correct number or empty!");
                    return;
                }
                this.Page = parsedPage;
            }
            else {
                this.Page = null;
            }

            if (!string.IsNullOrWhiteSpace(pageSize))
            {
                var pageSizeWasParsed = int.TryParse(pageSize, out var parsedPageSize);
                if (!pageSizeWasParsed)
                {
                    MessageBox.Show("Can't parse Page Size param! It should be correct number or empty!");
                    return;
                }
                this.PageSize = parsedPageSize;
            }
            else
            {
                this.PageSize = null;
            }

            this.IncludeDeleted = this.IncludeDeletedCheckBox.Checked;
            this.IncludeNested = this.IncludeNestedCheckBox.Checked;
            this.FullMode = this.FullModeCheckBox.Checked;
            this.IsDistinct = this.IsDistinctCheckBox.Checked;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ResetInputsToDefault() {
            this.FieldsTextBox.Text = "";
            this.WhereTextBox.Text = "";
            this.OrderByTextBox.Text = "";
        }

        private void ClearInputsButton_Click(object sender, EventArgs e)
        {
            this.FieldsTextBox.Text = "";
            this.WhereTextBox.Text = "";
            this.OrderByTextBox.Text = "";

            this.PageTextBox.Text = "";
            this.PageSizeTextBox.Text = "";

            this.IncludeDeletedCheckBox.Checked = false;
            this.IncludeNestedCheckBox.Checked = false;
            this.FullModeCheckBox.Checked = false;
            this.IsDistinctCheckBox.Checked = false;
        }
    }

    public class GetRequestDataDynamicFormPrepareParams
    {
        internal string OpenFor { get; set; }
        internal bool UseFieldsInput { get; set; } = false;
        internal bool UseWhereInput { get; set; } = false;
        internal bool UseOrderByInput { get; set; } = false;
        internal bool UsePageInput { get; set; } = false;
        internal bool UsePageSizeInput { get; set; } = false;


        internal bool UseIncludeDeletedCheckBox { get; set; } = false;

        internal bool UseIncludeNestedCheckBox { get; set; } = false;

        internal bool UseFullModeCheckBox { get; set; } = false;

        internal bool UseIsDistinctCheckBox { get; set; } = false;
    }
}
