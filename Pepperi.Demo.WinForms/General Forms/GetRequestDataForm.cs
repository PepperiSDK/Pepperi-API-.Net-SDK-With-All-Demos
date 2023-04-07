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
    public partial class GetRequestDataForm : Form
    {
        public string Fields { get; set; }
        public string Where { get; set; }

        public GetRequestDataForm()
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
            var fields = this.FieldsTextBox.Text;
            var where = this.WhereTextBox.Text;

            this.Fields = string.IsNullOrWhiteSpace(fields) ? null : fields;
            this.Where = string.IsNullOrWhiteSpace(where) ? null : where;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
