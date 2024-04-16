using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.General_Forms
{
    public partial class GenericConfirmForm : Form
    {
        public GenericConfirmForm(string text = "Are you sure?")
        {
            InitializeComponent();
            this.BasicTextLabel.Text = text;
        }

        public void SetBasicText(string text) {
            this.BasicTextLabel.Text = text;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
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
