using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Pepperi.SDK;
using Pepperi.SDK.Helpers;
using Pepperi.SDK.Model;
using Pepperi.SDK.Model.Fixed;
using Pepperi.SDK.Model.Fixed.MetaData;
using Pepperi.SDK.Model.Fixed.Resources;
using WinFormApiDemo;
using WinFormApiDemo.General_Forms;
using WinFormApiDemo.Ipaas_Forms;
using WinFormApiDemo.Surveys_Forms;
using WinFormApiDemo.User_Defined_Collections_Forms;

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
        private string schecma = "";
        private IEnumerable<string> udcNames = null;
        private bool sortAscending = false;
        private JArray response = null;
        private udcExportFileConfiguration udcExportFileConfiguration = new udcExportFileConfiguration();
        private GetRequestDataForm GetRequestDataForm = new GetRequestDataForm();
        private udcSchemeSelectorForm udcSchemeSelectorForm;
        private surveysResponseForm surveysResponseForm;
        private surveysUpsertForm surveysUpsertForm;
        private GetIpaasScheduledJobIdForm getIpaasScheduledJobIdForm = new GetIpaasScheduledJobIdForm();
        private IpaasRunJobResultForm IpaasRunJobResultForm = new IpaasRunJobResultForm();
        public frmSample()
        {
            InitializeComponent();
            dataGridView1.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();
            try
            {
                items = (List<Item>)client.Items.Find(txtFilter.Text);
                pleaseWait.Close();
            }
            catch (Exception)
            {
                pleaseWait.Close();
                MessageBox.Show("Error with retrieving accounts!");
            }
            dataGridView1.DataSource = items;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            accounts = (List<Account>)client.Accounts.Find(txtFilter.Text);
            dataGridView1.DataSource = accounts;
        }

        private void btnViewAcounts_Click(object sender, EventArgs e)
        {
            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();
            try
            {
                accounts = (List<Account>)client.Accounts.Find(txtFilter.Text);
                pleaseWait.Close();
            }
            catch (Exception)
            {
                pleaseWait.Close();
                MessageBox.Show("Error with retrieving accounts!");
            }
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

                var authentificationManager = new AuthentificationManager(Logger, token.APIToken, DeveloperKey);
                client = new ApiClient(APIBaseAddress, auth, Logger, AuthentificationManager: authentificationManager);
                udcSchemeSelectorForm = new udcSchemeSelectorForm()
                {
                    Client = client
                };
                MessageBox.Show("Connected!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Not Connected!. reason: " + ex.ToString ());
            }
        }

        private void frmSample_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls12
            | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "token.txt"))
            {
                txtToken.Text = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "token.txt");
                CompanyToken = txtToken.Text;
                auth = new Pepperi.SDK.PrivateAuthentication(DeveloperKey, CompanyToken);
                var authentificationManager = new AuthentificationManager(Logger, CompanyToken, DeveloperKey);
                client = new ApiClient(APIBaseAddress, auth, new PepperiLogger(), 
                    AuthentificationManager: authentificationManager);

                udcSchemeSelectorForm = new udcSchemeSelectorForm()
                {
                    Client = client
                };

                surveysResponseForm = new surveysResponseForm();
                surveysUpsertForm = new surveysUpsertForm();
            }
        }


        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            JArray sorted;
            if (sortAscending)
                 sorted = new JArray(response.OrderBy(obj => (string)obj[dataGridView1.Columns[e.ColumnIndex].DataPropertyName]));
            else
                sorted = new JArray(response.OrderByDescending(obj => (string)obj[dataGridView1.Columns[e.ColumnIndex].DataPropertyName]));
            sortAscending = !sortAscending;

            dataGridView1.DataSource = sorted;
        }

        private void btnUploadUdc_Click(object sender, EventArgs e)
        {
            schecma = ProccedUdcFormSelection();
            if (schecma == null) return;

            var overwrite = false;
            var overwriteObject = false;
            using (var configForm = new udcUploadConfigurationForm()) {
                if (configForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                };
                overwrite = configForm.Overwrite;
                overwriteObject = configForm.OverwriteObject;
            }

            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;
            if(file!= "openFileDialog1" && schecma!="")
            {
                UDC_UploadFile_Result response = null;

                var pleaseWait = new PleaseWaitForm();
                pleaseWait.Show();
                Application.DoEvents();
                try
                {
                    response = client.UserDefinedCollections.BulkUploadFile(schecma, file, overwriteObject, overwrite);
                    pleaseWait.Close();
                }
                catch (Exception ex)
                {
                    Logger.Log("Upload UDC Error, message - " + (ex?.Message ?? "No Message"));
                    pleaseWait.Close();
                    MessageBox.Show("Error with importing scheme data!");
                    return;
                }
                
                string message = "";
                if (response.TotalFailed == 0)
                {
                    dataGridView1.DataSource = null;
                    message = "All lines were uploaded successfully!";
                }
                else
                {
                    message = "NOT all lines were uploaded successfully, some failed. See details in the below table.";
                    dataGridView1.DataSource = (List<UDC_UploadFile_Row>)response.FailedRows;
                }

                MessageBox.Show(message + Environment.NewLine
                    + "Total:  " + response.Total + Environment.NewLine
                    + "Inserted:  " + response.TotalInserted + Environment.NewLine
                    + "Updated:  " + response.TotalUpdated + Environment.NewLine
                    + "Ignored:  " + response.TotalIgnored + Environment.NewLine
                    + "Merged:  " + response.TotalMergedBeforeUpload + Environment.NewLine
                    + "Failed:  " + response.TotalFailed);
            }
            else
            {
                MessageBox.Show("select file and schema!");
            }
        }

        private void btnViewUdc_Click(object sender, EventArgs e)
        {
            schecma = ProccedUdcFormSelection();
            if (schecma == null) {
                return;
            }

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();
            UDC_MetaData udc = null;
            try
            {
                udc = client.UserDefinedCollectionsMetaData.GetUserDefinedCollection(schecma);
                response = client.UserDefinedCollections.FindGeneric(schecma);
                pleaseWait.Close();
            }
            catch (Exception)
            {
                pleaseWait.Close();
                MessageBox.Show("Error with retrieving scheme data!");
            }

            dataGridView1.DataSource = response;
            try
            {
                if (udc?.ListView?.Fields != null)
                {
                    var fields = udc.ListView.Fields;

                    var columns = dataGridView1.Columns;
                    var listFields = fields.ToList();
                    for (int i = 0; i < listFields.Count(); i++)
                    {
                        var field = listFields[i];
                        if (columns.Contains(field.FieldID))
                            columns[field.FieldID].DisplayIndex = i;
                    }
                    if (columns.Contains("Key"))
                        columns["Key"].DisplayIndex = 0;
                }
            }
            catch (Exception) {
                dataGridView1.DataSource = response;
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PleaseWaitForm pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            List<UDC_MetaData> collections = new List<UDC_MetaData>();
            try
            {
                collections = client.UserDefinedCollectionsMetaData.GetUserDefinedCollections().ToList();
                udcNames = collections.Select(collection => collection.Name).ToList();
                udcSchemeSelectorForm.SetComboBoxValues(udcNames);
            }
            catch (Exception)
            {
                MessageBox.Show("Error with retrieving schemes names!");
                return;
            }
            finally
            {
                pleaseWait.Close();
            }
            dataGridView1.DataSource = collections;
        }

        private string ProccedUdcFormSelection() {
            List<string> collections = new List<string>();
            PleaseWaitForm pleaseWait = new PleaseWaitForm();
            if (udcNames == null)
            {

                pleaseWait.Show();
                Application.DoEvents();

                try
                {
                    collections = client.UserDefinedCollectionsMetaData.GetUserDefinedCollections().Select(collection => collection.Name).ToList();
                    udcNames = collections;
                    udcSchemeSelectorForm.SetComboBoxValues(collections);
                    pleaseWait.Close();
                }
                catch (Exception)
                {
                    pleaseWait.Close();
                    MessageBox.Show("Error with retrieving schemes names!");
                    throw;
                }
            }

            if (udcSchemeSelectorForm.ShowDialog() != DialogResult.OK)
            {
                return null;
            };

            return udcSchemeSelectorForm.SelectedUdc;
        }

        private void ExportDataFile_Click(object sender, EventArgs e)
        {
            schecma = ProccedUdcFormSelection();
            if (schecma == null) return;

            if (udcExportFileConfiguration.ShowDialog() != DialogResult.OK) return;

            var fields = udcExportFileConfiguration.Fields;
            var where = udcExportFileConfiguration.Where;
            var format = udcExportFileConfiguration.Format;
            var excludedKeys = udcExportFileConfiguration.ExcludedKeys;
            var includeDeleted = udcExportFileConfiguration.IncludeDeleted;

            var fodlerSelectionResult = folderBrowserDialog1.ShowDialog();
            if (fodlerSelectionResult != DialogResult.OK) {
                return;
            }
            var folderPath = folderBrowserDialog1.SelectedPath;
            var newFileName = $"Export_{schecma}_{DateTime.UtcNow.ToString("o").Replace(':', '-').Replace('.', '-')}.{format}";
            var fullFilePath = $"{folderPath}\\{newFileName}";

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();
            try
            {
                client.UserDefinedCollections.ExportAsync(schecma, fullFilePath,
                    format: format,
                    where: where,
                    fields: fields,
                    excludedKeys: excludedKeys);
                pleaseWait.Close();
                MessageBox.Show($"File was exported! File name - '{newFileName}' ({fullFilePath})");
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                MessageBox.Show($"Error with file Export! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void DeleteSchemeButton_Click(object sender, EventArgs e)
        {
            schecma = ProccedUdcFormSelection();
            if (schecma == null) return;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();
            try
            {
                client.UserDefinedCollectionsMetaData.DeleteUserDefinedCollection(schecma);
                var existingSchemes = udcSchemeSelectorForm.GetComboBoxValues();
                if (existingSchemes != null && existingSchemes.Count() > 0) {
                    var newSchemes = existingSchemes.Where(schemeName => schemeName != schecma).ToList();
                    udcSchemeSelectorForm.SetComboBoxValues(newSchemes);
                    udcNames = newSchemes;
                }

                pleaseWait.Close();
                MessageBox.Show($"Scheme was removed!");
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                MessageBox.Show($"Error with Scheme Delete! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void getSurveys_Click(object sender, EventArgs e)
        {
            if (GetRequestDataForm.ShowDialog() != DialogResult.OK) return;

            var fields = GetRequestDataForm.Fields;
            var where = GetRequestDataForm.Where;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();
            try
            {
                var surveys = client.Surveys.Find(fields: fields, where: where);
                pleaseWait.Close();

                var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(
                                    surveys,
                                    Newtonsoft.Json.Formatting.Indented,
                                    new Newtonsoft.Json.JsonSerializerSettings
                                    {
                                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                                        DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc //will serialize date time as utc
                                    });

                dataGridView1.DataSource = surveys;

                surveysResponseForm.SetText(serialized);
                surveysResponseForm.ShowDialog();
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
            
        }

        private void surveysUpsert_Click(object sender, EventArgs e)
        {
            if (surveysUpsertForm.ShowDialog() != DialogResult.OK) return;
            var surveyToUpsertJson = surveysUpsertForm.SurveyToUpsertJson;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();
            try
            {
                var surveyToUpsertParsed = Newtonsoft.Json.JsonConvert.DeserializeObject<Survey>(surveyToUpsertJson);
                var survey = client.Surveys.Upsert(surveyToUpsertParsed);
                pleaseWait.Close();

                var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(
                                    survey,
                                    Newtonsoft.Json.Formatting.Indented,
                                    new Newtonsoft.Json.JsonSerializerSettings
                                    {
                                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                                        DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc //will serialize date time as utc
                                    });

                surveysResponseForm.SetText(serialized);
                surveysResponseForm.ShowDialog();
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void btnViewTemplates_Click(object sender, EventArgs e)
        {
            if (GetRequestDataForm.ShowDialog() != DialogResult.OK) return;

            var fields = GetRequestDataForm.Fields;
            var where = GetRequestDataForm.Where;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();
            try
            {
                var surveys = client.Surveys.FindTemplates(fields: fields, where: where);
                pleaseWait.Close();

                var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(
                                    surveys,
                                    Newtonsoft.Json.Formatting.Indented,
                                    new Newtonsoft.Json.JsonSerializerSettings
                                    {
                                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                                        DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc //will serialize date time as utc
                                    });

                dataGridView1.DataSource = surveys;

                surveysResponseForm.SetText(serialized);
                surveysResponseForm.ShowDialog();
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void ipaasRunJobButton_Click(object sender, EventArgs e)
        {
            if (getIpaasScheduledJobIdForm.ShowDialog() != DialogResult.OK) return;
            var jobId = getIpaasScheduledJobIdForm.JobId;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                var dataUrls = client.Ipaas.ScheduledJobs.RunJob(jobId);
                pleaseWait.Close();

                IpaasRunJobResultForm.dataGridView = dataGridView1;
                IpaasRunJobResultForm.SetUrls(dataUrls);
                IpaasRunJobResultForm.ShowDialog();
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }
    }


}