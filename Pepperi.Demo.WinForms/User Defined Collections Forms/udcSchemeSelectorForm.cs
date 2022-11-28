using Pepperi.SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApiDemo
{
    public partial class udcSchemeSelectorForm : Form
    {
        public ApiClient Client { get; set; }
        public IEnumerable<string> RefreshedUdc { get; set; } = null;
        public bool WasRefreshed { get; set; } = false;
        public udcSchemeSelectorForm()
        {
            InitializeComponent();
        }

        public void SetComboBoxValues(IEnumerable<string> values) {
            this.comboBox1.DataSource = values;
        }

        public IEnumerable<string> GetComboBoxValues()
        {
            return (IEnumerable<string>)this.comboBox1.DataSource;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var inputString = (string)this.comboBox1.SelectedValue;
            if (string.IsNullOrWhiteSpace(inputString)) {
                MessageBox.Show("Schema can't be empty or WhiteSpaced!");
                return;
            }

            this.SelectedUdc = inputString;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void udcSelectionRefresh_Click(object sender, EventArgs e)
        {
            List<string> collections = new List<string>();
            PleaseWaitForm pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                collections = Client.UserDefinedCollectionsMetaData.GetUserDefinedCollections().Select(collection => collection.Name).ToList();
                RefreshedUdc = collections;
                WasRefreshed = true;
                SetComboBoxValues(collections);
            }
            catch (Exception)
            {
                MessageBox.Show("Error with retrieving schemes names!");
            }
            finally
            {
                pleaseWait.Close();
            }
        }
    }
}
