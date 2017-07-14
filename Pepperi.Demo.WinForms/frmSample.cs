using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pepperi.SDK;
using Pepperi.SDK.Helpers;
using Pepperi.SDK.Model;
using Pepperi.SDK.Model.Fixed;

namespace WindowsFormsApp1
{
    public partial class frmSample : Form
    {
        private ApiClient client;
        private PrivateAuthentication auth;
        private List<Item> items;
        private List<Account> accounts;
        private string DeveloperKey = "8JZNRlMG2gFeudl51qNFHOMGPSV0XXPP";
        private string CompanyToken = "";
        private string APIBaseAddress = "https://api.pepperi.com/v1.0/";
        PepperiLogger Logger = new PepperiLogger();


        public frmSample()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {      
            items = (List<Item>)client.Items.Find(txtFilter.Text);
            dataGridView1.DataSource = items;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            accounts = (List<Account>)client.Accounts.Find(txtFilter.Text);
            dataGridView1.DataSource = accounts;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            var fields = new List<string>();
            fields.Add("ExternalID");
            fields.Add("MainCategoryID");
            fields.Add("Price");
            var response = client.Items.BulkUpload(items, eOverwriteMethod.none, eBulkUploadMethod.Zip, fields, true);
            var responseInfo = client.Items.WaitForBulkJobToComplete(response.JobID);
            MessageBox.Show(responseInfo.Status);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.CurrentRow.Index;
            var item = items[index];
            var updatedItme = client.Items.Upsert(item);
            MessageBox.Show("done!");

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var token = PrivateAuthentication.GetAPITokenData(APIBaseAddress, DeveloperKey, txtEmail.Text, txtPassword.Text, Logger);
                if (token.APIToken == Guid.Empty.ToString())
                    throw new Exception("user or passsword are incorrect.");
                txtToken.Text = token.APIToken;
                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "token.txt", token.APIToken);
                auth = new PrivateAuthentication(DeveloperKey, token.APIToken);
                client = new ApiClient(APIBaseAddress, auth, Logger);
                MessageBox.Show("Connected!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Not Connected!. reason: " + ex.ToString ());
            }
        }

        private void frmSample_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "token.txt"))
            {
                txtToken.Text = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "token.txt");
                CompanyToken = txtToken.Text;
                auth = new Pepperi.SDK.PrivateAuthentication(DeveloperKey, CompanyToken);
                client = new ApiClient(APIBaseAddress, auth, new PepperiLogger());
            }
        }
    }


}