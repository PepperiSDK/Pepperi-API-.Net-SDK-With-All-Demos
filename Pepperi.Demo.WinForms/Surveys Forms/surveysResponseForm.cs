using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApiDemo.Surveys_Forms
{
    public partial class surveysResponseForm : Form
    {
        public surveysResponseForm()
        {
            InitializeComponent();
        }

        public void SetText(string value)
        {
            this.textBox1.Text = value;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
