﻿using System;
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
    public partial class udcUploadConfigurationForm : Form
    {
        public bool OverwriteObject { get; set; } = false;
        public bool Overwrite { get; set; } = false;
        public bool MultiFilesOverwrite { get; set; } = false;

        public udcUploadConfigurationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            this.Overwrite = this.overwriteCheckBox.Checked;
            this.OverwriteObject = this.overwriteObjectCheckBox.Checked;
            this.MultiFilesOverwrite = this.MultiFilesOverwriteCheckbox.Checked;

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
