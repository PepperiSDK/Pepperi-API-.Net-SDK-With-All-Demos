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
    public partial class GetKeyGenericForm : Form
    {
        public string Key { get; set; }
        public string LastOpenedFor { get; set; }


        public GetKeyGenericForm()
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            this.Key = this.KeyTextBox.Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ResetInputsToDefault()
        {
            this.KeyTextBox.Text = "";
        }
    }
}
