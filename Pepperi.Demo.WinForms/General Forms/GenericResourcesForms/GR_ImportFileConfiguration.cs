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
    public partial class GR_ImportFileConfiguration : Form
    {
        public bool OverwriteObject { get; set; } = false;
        public bool Overwrite { get; set; } = false;

        public string LastOpenedFor { get; set; }

        public GR_ImportFileConfiguration()
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
            this.overwriteCheckBox.Checked = false;
            this.overwriteObjectCheckBox.Checked = false;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            this.Overwrite = this.overwriteCheckBox.Checked;
            this.OverwriteObject = this.overwriteObjectCheckBox.Checked;

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
