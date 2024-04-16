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
using WinFormApiDemo.Model;
using WinFormApiDemo.PepperiResoursesForms.Journeys;
using WinFormApiDemo.Surveys_Forms;
using WinFormApiDemo.User_Defined_Collections_Forms;
using WinFormApiDemo.Helpers;
using WindowsFormsApp1.General_Forms;

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
        private string IpaasBaseUrl = "https://integration.pepperi.com/prod/api/";
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
        private GetIpaasStartScheduledJobInfoForm getIpaasScheduledJobIdForm = new GetIpaasStartScheduledJobInfoForm();
        private GetExportAsyncRequestDataForm GetExportAsyncRequestDataForm =
            new GetExportAsyncRequestDataForm();
        private GenericJsonResponseForm GenericJsonResponseForm = new GenericJsonResponseForm();
        private GetRequestDataDynamicForm GetRequestDataDynamicForm = new GetRequestDataDynamicForm();
        private GetKeyGenericForm GetKeyGenericForm = new GetKeyGenericForm();

        private IpaasRunJobResultForm IpaasRunJobResultForm = new IpaasRunJobResultForm();

        private StandardBulkUploadRequestForm StandardBulkUploadRequestForm = new StandardBulkUploadRequestForm();

        private GR_ExportFileConfiguration GR_ExportFileConfiguration = new GR_ExportFileConfiguration();

        private GetJsonToUseForm Notifications_JsonToUseForm { get; set; } = new GetJsonToUseForm("Notification JSON to POST", defaultJson: PrettyJson(new Notification()
        {
            Body = "Body",
            Title = "Title",
            UserEmail = "yourEmail",
            NavigationPath = "The destination path when clicking the notification"
        }), shouldBeOnlySingleObject: true);
        private JourneysSearchFilesForm JourneysSearchFilesForm { get; set; }

        private GetJsonToUseForm RelatedItems_JsonToUseForm { get; set; } = new GetJsonToUseForm("Related Item JSON to Upsert", defaultJson: PrettyJson(new RelatedItem()
        {
            CollectionName = "RelatedItemsCollection1",
            ItemExternalID = "Item1",
            RelatedItems = new List<string>() { "Item2", "Item3" }
        }), shouldBeOnlySingleObject: true);

        #region This Form Methods

        public frmSample()
        {
            InitializeComponent();
            dataGridView1.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;

            InitCustomComponents();
        }

        private void InitCustomComponents()
        {

            #region Main Resource DropDown

            var bindingSource1 = new BindingSource
            {
                DataSource = GenerateMainResoourcesDropDownList()
            };
            MainResources_ResourceToUse.DataSource = bindingSource1.DataSource;
            MainResources_ResourceToUse.DisplayMember = "Text";
            MainResources_ResourceToUse.ValueMember = "Value";

            #endregion

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
                var userName = txtEmail.Text;
                var password = txtPassword.Text;

                var token = GetToken(userName, password, "User or Passsword are incorrect!");
                SaveToken(token);
                txtToken.Text = token;

                InitClientApi(token);
                MessageBox.Show("Connected succesfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not Connected!. reason: " + ex.ToString());
            }
        }

        private void SaveAndUseToken_Button_Click(object sender, EventArgs e)
        {
            try
            {
                var token = txtToken.Text;
                ValuesValidator.Validate(token, "Token is empty!");

                GetToken("TokenAuth", token, "Provided Token is incorrect!");
                SaveToken(token);

                InitClientApi(token);
                MessageBox.Show("Token was saved succesfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! Reason: " + (ex?.Message ?? ex.ToString()));
            }
        }

        private string GetToken(string username, string password, string errorMessage = "User, Passsword or Token are incorrect!")
        {
            var response = PrivateAuthentication.GetAPITokenData(APIBaseAddress, DeveloperKey, username, password, Logger);
            var token = response?.APIToken;

            ValuesValidator.Validate(token, errorMessage, false);
            return token;
        }

        private void InitClientApi(string token)
        {
            auth = new PrivateAuthentication(DeveloperKey, token);

            var authentificationManager = new AuthentificationManager(Logger, token, DeveloperKey);
            client = new ApiClient(APIBaseAddress, auth, Logger, AuthentificationManager: authentificationManager, ipaasBaseUrl: this.IpaasBaseUrl);

            udcSchemeSelectorForm = new udcSchemeSelectorForm()
            {
                Client = client
            };

            this.JourneysSearchFilesForm = new JourneysSearchFilesForm(client);

            if (surveysResponseForm == null) surveysResponseForm = new surveysResponseForm();
            if (surveysUpsertForm == null) surveysUpsertForm = new surveysUpsertForm();
        }

        private void SaveToken(string token)
        {
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "token.txt", token);
        }

        private string GetSaveToken()
        {
            return System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "token.txt");
        }

        private bool IsSavedTokenExist()
        {
            return System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "token.txt");
        }

        private void frmSample_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls12
            | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            if (!IsSavedTokenExist()) return;

            var token = GetSaveToken();
            txtToken.Text = token;
            InitClientApi(token);
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

        #endregion

        #region User Defined Collections (UDC)

        private void btnUploadUdc_Click(object sender, EventArgs e)
        {
            schecma = ProccedUdcFormSelection();
            if (schecma == null) return;

            var overwrite = false;
            var overwriteObject = false;
            var multiFilesOverwrite = false;
            using (var configForm = new udcUploadConfigurationForm())
            {
                if (configForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                };
                overwrite = configForm.Overwrite;
                overwriteObject = configForm.OverwriteObject;
                multiFilesOverwrite = configForm.MultiFilesOverwrite;
            }

            UDC_MultiFilesOverwriteFinish_Result finishResult = null;
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;
            if (file != "openFileDialog1" && schecma != "")
            {
                UDC_UploadFile_Result response = null;

                var pleaseWait = new PleaseWaitForm();
                pleaseWait.Show();
                Application.DoEvents();
                try
                {
                    string multiFilesOverwriteKey = null;
                    if (multiFilesOverwrite) {
                        multiFilesOverwriteKey = client.UserDefinedCollections.StartMultiFilesOverwrite(schecma);
                    }
                    response = client.UserDefinedCollections.BulkUploadFile(
                        schecma, file, overwriteObject, overwrite, multiFilesOverwriteKey: multiFilesOverwriteKey);

                    if (multiFilesOverwrite)
                    {
                        finishResult = client.UserDefinedCollections.FinishMultiFilesOverwrite(schecma, multiFilesOverwriteKey);
                    }

                    pleaseWait.Close();
                }
                catch (Exception ex)
                {
                    Logger.Log("Upload UDC Error, message - " + (ex?.Message ?? "No Message"));
                    pleaseWait.Close();
                    MessageBox.Show("Error with importing scheme data! Error Message: " + (ex?.Message ?? "No Message"));
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

                if (finishResult != null) {
                    MessageBox.Show("Result from Multi Files Overwrite: " + PrettyJson(finishResult));
                }
            }
            else
            {
                MessageBox.Show("select file and schema!");
            }
        }

        private void btnViewUdc_Click(object sender, EventArgs e)
        {
            schecma = ProccedUdcFormSelection();
            if (schecma == null)
            {
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
            catch (Exception)
            {
                dataGridView1.DataSource = response;
            }

        }

        private void Udc_ViewSchemes_Button_Click(object sender, EventArgs e)
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

        private string ProccedUdcFormSelection()
        {
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
                    return null;
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

            var fodlerSelectionResult = folderBrowserDialog1.ShowDialog();
            if (fodlerSelectionResult != DialogResult.OK)
            {
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
                if (existingSchemes != null && existingSchemes.Count() > 0)
                {
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

        #endregion

        #region Surveys

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

        #endregion

        #region Ipaas

        private void ipaasRunJobButton_Click(object sender, EventArgs e)
        {
            if (getIpaasScheduledJobIdForm.ShowDialog() != DialogResult.OK) return;
            var jobId = getIpaasScheduledJobIdForm.JobId;
            var jsonData = String.IsNullOrWhiteSpace(getIpaasScheduledJobIdForm.JsonData) ? null : getIpaasScheduledJobIdForm.JsonData;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                var dataUrls = client.Ipaas.ScheduledJobs.RunJob(jobId, jsonData);

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

        #endregion

        #region Journeys

        private void JourneysSearchFiles_Click(object sender, EventArgs e)
        {
            JourneysSearchFilesForm.ShowDialog();
        }

        #endregion

        #region Notifications

        private void NotificationsFindButton_Click(object sender, EventArgs e)
        {
            GetRequestDataDynamicForm.PrepareFrom(new GetRequestDataDynamicFormPrepareParams()
            {
                OpenFor = "NotificationsGet",
                UseWhereInput = true,
                UseOrderByInput = true,
                UseFieldsInput = true
            });
            if (GetRequestDataDynamicForm.ShowDialog() != DialogResult.OK) return;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                var where = GetRequestDataDynamicForm.Where;
                var orderBy = GetRequestDataDynamicForm.OrderBy;
                var fields = GetRequestDataDynamicForm.Fields;

                var finalResult = client.Notifications.Find(where: where, order_by: orderBy, fields: fields);

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                if (NotificationsShowJsonCheckbox.Checked)
                {
                    ShowJson(PrettyJson(finalResult));
                }
                dataGridView1.DataSource = finalResult;
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void Notifications_PostButton_Click(object sender, EventArgs e)
        {
            if (Notifications_JsonToUseForm.ShowDialog() != DialogResult.OK) return;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                var json = Notifications_JsonToUseForm.JsonString;
                var notification = ParseJson<Notification>(json);
                var finalResult = client.Notifications.Post(notification);

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                ShowJson(PrettyJson(finalResult));
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        #endregion

        #region Related Items

        private void RelatedItems_FindButton_Click(object sender, EventArgs e)
        {
            GetRequestDataDynamicForm.PrepareFrom(new GetRequestDataDynamicFormPrepareParams()
            {
                OpenFor = "RelatedItemsFind",
                UseWhereInput = true,
                UseFieldsInput = true,
                UseOrderByInput = true,
                UsePageInput = true,
                UsePageSizeInput = true
            });
            if (GetRequestDataDynamicForm.ShowDialog() != DialogResult.OK) return;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                var where = GetRequestDataDynamicForm.Where;
                var fields = GetRequestDataDynamicForm.Fields;
                var order_by = GetRequestDataDynamicForm.OrderBy;
                var page = GetRequestDataDynamicForm.Page;
                var page_size = GetRequestDataDynamicForm.PageSize;

                var finalResult = client.RelatedItems.Find(
                    where: where, fields: fields, order_by: order_by,
                    page: page, page_size: page_size
                    );

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                if (RelatedItems_ShowJsonCheckbox.Checked)
                {
                    ShowJson(PrettyJson(finalResult));
                }

                dataGridView1.DataSource = finalResult;
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void RelatedItems_FindByKeyButton_Click(object sender, EventArgs e)
        {
            GetKeyGenericForm.PrepareFrom("RelatedItemsFindByKey");
            if (GetKeyGenericForm.ShowDialog() != DialogResult.OK) return;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                var key = GetKeyGenericForm.Key;

                var finalResult = client.RelatedItems.FindByKey(key: key);

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                if (RelatedItems_ShowJsonCheckbox.Checked)
                {
                    ShowJson(PrettyJson(finalResult));
                }

                dataGridView1.DataSource = new List<RelatedItem>() { finalResult };
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void RelatedItems_UpsertButton_Click(object sender, EventArgs e)
        {
            if (RelatedItems_JsonToUseForm.ShowDialog() != DialogResult.OK) return;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                var json = RelatedItems_JsonToUseForm.JsonString;
                var relatedItem = ParseJson<RelatedItem>(json);

                var finalResult = client.RelatedItems.Upsert(relatedItem);

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                if (RelatedItems_ShowJsonCheckbox.Checked)
                {
                    ShowJson(PrettyJson(finalResult));
                }

                dataGridView1.DataSource = new List<RelatedItem>() { finalResult };
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void RelatedItems_ExportAsyncButton_Click(object sender, EventArgs e)
        {
            GR_ExportFileConfiguration.PrepareFrom("relatedItems");

            if (GR_ExportFileConfiguration.ShowDialog() != DialogResult.OK) return;

            var fields = GR_ExportFileConfiguration.Fields;
            var where = GR_ExportFileConfiguration.Where;
            var format = GR_ExportFileConfiguration.Format;
            var excludedKeys = GR_ExportFileConfiguration.ExcludedKeys;

            var fodlerSelectionResult = folderBrowserDialog1.ShowDialog();
            if (fodlerSelectionResult != DialogResult.OK)
            {
                return;
            }
            var folderPath = folderBrowserDialog1.SelectedPath;
            var newFileName = $"Export_RelatedItems_{DateTime.UtcNow.ToString("o").Replace(':', '-').Replace('.', '-')}.{format}";
            var fullFilePath = $"{folderPath}\\{newFileName}";

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                client.RelatedItems.ExportAsync(fullFilePath,
                    format: format,
                    where: where,
                    fields: fields,
                    excludedKeys: excludedKeys);

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                MessageBox.Show($"File was exported! File name - '{newFileName}' ({fullFilePath})");
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void RelatedItems_UploadFile_Button_Click(object sender, EventArgs e)
        {
            var overwrite = false;
            var overwriteObject = false;
            using (var configForm = new GR_ImportFileConfiguration())
            {
                if (configForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                };
                overwrite = configForm.Overwrite;
                overwriteObject = configForm.OverwriteObject;
            }

            var fodlerSelectionResult = openFileDialog1.ShowDialog();
            if (fodlerSelectionResult != DialogResult.OK) return;

            var file = openFileDialog1.FileName;

            GenericResource_UploadFile_Result response = null;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();
            try
            {
                response = client.RelatedItems.BulkUploadFile(file, overwriteObject, overwrite);

                string message = "";
                if (response.TotalFailed == 0)
                {
                    dataGridView1.DataSource = null;
                    message = "All lines were uploaded successfully!";
                }
                else
                {
                    message = "NOT all lines were uploaded successfully, some failed. See details in the below table.";
                    dataGridView1.DataSource = (List<GenericResource_UploadFile_Row>)response.FailedRows;
                }

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                MessageBox.Show(message + Environment.NewLine
                    + "Total:  " + response.Total + Environment.NewLine
                    + "Inserted:  " + response.TotalInserted + Environment.NewLine
                    + "Updated:  " + response.TotalUpdated + Environment.NewLine
                    + "Ignored:  " + response.TotalIgnored + Environment.NewLine
                    + "Merged:  " + response.TotalMergedBeforeUpload + Environment.NewLine
                    + "Failed:  " + response.TotalFailed);
            }
            catch (Exception ex)
            {
                Logger.Log("Upload Related Items Error, message - " + (ex?.Message ?? "No Message"));
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show("Error with importing Related Items Data! Error Message: " + (ex?.Message ?? "No Message"));
                return;
            }

        }

        #endregion

        #region Main Resources

        private void MainResource_FindButton_Click(object sender, EventArgs e)
        {
            var resource = (string)MainResources_ResourceToUse.SelectedValue;
            if (!ValidateMainResourceValue(resource)) return;

            GetRequestDataDynamicForm.PrepareFrom(new GetRequestDataDynamicFormPrepareParams()
            {
                OpenFor = resource,
                UseWhereInput = true,
                UseOrderByInput = true,
                UseFieldsInput = true,

                UsePageInput = true,
                UsePageSizeInput = true,

                UseIncludeDeletedCheckBox = true,
                UseIncludeNestedCheckBox = true,
                UseFullModeCheckBox = true,
                UseIsDistinctCheckBox = true
            });
            if (GetRequestDataDynamicForm.ShowDialog() != DialogResult.OK) return;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                var fields = GetRequestDataDynamicForm.Fields;
                var where = GetRequestDataDynamicForm.Where;
                var orderBy = GetRequestDataDynamicForm.OrderBy;

                var page = GetRequestDataDynamicForm.Page;
                var pageSize = GetRequestDataDynamicForm.PageSize;

                var includeDeleted = GetRequestDataDynamicForm.IncludeDeleted;
                var includeNested = GetRequestDataDynamicForm.IncludeDeleted;
                var fullMode = GetRequestDataDynamicForm.FullMode;
                var isDistinct = GetRequestDataDynamicForm.IsDistinct;

                object result = null;
                switch (resource)
                {
                    case "items":
                        result = client.Items.Find(where: where, order_by: orderBy, page: page, page_size: pageSize,
                                include_nested: includeNested, full_mode: fullMode, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    case "transactions":
                        result = client.Transactions.Find(where: where, order_by: orderBy, page: page, page_size: pageSize,
                                include_nested: includeNested, full_mode: fullMode, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    case "activities":
                        result = client.Activities.Find(where: where, order_by: orderBy, page: page, page_size: pageSize,
                                include_nested: includeNested, full_mode: fullMode, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    case "accountUsers":
                        result = client.AccountUsers.Find(where: where, order_by: orderBy, page: page, page_size: pageSize,
                                include_nested: includeNested, full_mode: fullMode, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    case "userDefinedTables":
                        result = client.UserDefinedTables.Find(where: where, order_by: orderBy, page: page, page_size: pageSize,
                                include_nested: includeNested, full_mode: fullMode, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    default:
                        pleaseWait.Close();
                        pleaseWait.ChangeMainLabel();
                        MessageBox.Show($"Can't find Logic for this Resource!");
                        return;
                }

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                if (this.MainResource_ShowJsonCheckBox.Checked)
                {
                    ShowJson(PrettyJson(result));
                }

                dataGridView1.DataSource = result;
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void MainResource_ExportAsyncButton_Click(object sender, EventArgs e)
        {
            var resource = (string)MainResources_ResourceToUse.SelectedValue;
            if (!ValidateMainResourceValue(resource)) return;

            GetRequestDataDynamicForm.PrepareFrom(new GetRequestDataDynamicFormPrepareParams()
            {
                OpenFor = resource,
                UseWhereInput = true,
                UseOrderByInput = true,
                UseFieldsInput = true,

                UsePageInput = false,
                UsePageSizeInput = false,

                UseIncludeDeletedCheckBox = true,
                UseIncludeNestedCheckBox = false,
                UseFullModeCheckBox = false,
                UseIsDistinctCheckBox = true
            });
            if (GetRequestDataDynamicForm.ShowDialog() != DialogResult.OK) return;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                pleaseWait.ChangeMainLabel("Sending request to export data...");

                var fields = GetRequestDataDynamicForm.Fields;
                var where = GetRequestDataDynamicForm.Where;
                var orderBy = GetRequestDataDynamicForm.OrderBy;

                var includeDeleted = GetRequestDataDynamicForm.IncludeDeleted;
                var isDistinct = GetRequestDataDynamicForm.IsDistinct;

                ExportAsyncResponse exportAsyncResponse = null;
                switch (resource)
                {
                    case "items":
                        exportAsyncResponse = client.Items.ExportAsync(where: where, order_by: orderBy, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    case "transactions":
                        exportAsyncResponse = client.Transactions.ExportAsync(where: where, order_by: orderBy, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    case "activities":
                        exportAsyncResponse = client.Activities.ExportAsync(where: where, order_by: orderBy, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    case "accountUsers":
                        exportAsyncResponse = client.AccountUsers.ExportAsync(where: where, order_by: orderBy, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    case "userDefinedTables":
                        exportAsyncResponse = client.UserDefinedTables.ExportAsync(where: where, order_by: orderBy, include_deleted: includeDeleted,
                                fields: fields, is_distinct: isDistinct);
                        break;
                    default:
                        pleaseWait.Close();
                        pleaseWait.ChangeMainLabel();
                        MessageBox.Show($"Can't find Logic for this Resource!");
                        return;
                }

                var jobId = exportAsyncResponse.JobID;
                pleaseWait.ChangeMainLabel($"Pooling data... Please wait");

                var finalResult = client.UserDefinedTables.WaitForExportJobToComplete(jobId);

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                ShowJson(PrettyJson(finalResult));
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private void MainResource_BulkUploadButton_Click(object sender, EventArgs e)
        {
            var resource = (string)MainResources_ResourceToUse.SelectedValue;
            if (!ValidateMainResourceValue(resource)) return;

            StandardBulkUploadRequestForm.PrepareForm(resource);
            if (StandardBulkUploadRequestForm.ShowDialog() != DialogResult.OK) return;

            var pleaseWait = new PleaseWaitForm();
            pleaseWait.Show();
            Application.DoEvents();

            try
            {
                var uploadType = StandardBulkUploadRequestForm.BulkUploadType;

                var jobId = "";

                pleaseWait.ChangeMainLabel("Sending Bulk Upload Request...");
                if (uploadType == eStandardResources_BulkUploadType.Model)
                {
                    jobId = MainResource_BulkUploadButton_UploadModel(resource);
                }
                else if (uploadType == eStandardResources_BulkUploadType.CsvPath)
                {
                    jobId = MainResource_BulkUploadButton_UploadCsv(resource);
                }
                else
                {
                    throw new Exception("Incorrect Upload Type!");
                }

                if (string.IsNullOrEmpty(jobId)) throw new Exception("Can't get JobId from response!");

                pleaseWait.ChangeMainLabel($"Waiting for job ({jobId}) to complete...");
                var jobResult = client.UserDefinedTables.WaitForBulkJobToComplete(jobId);

                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();

                ShowJson(PrettyJson(jobResult));
            }
            catch (Exception exc)
            {
                pleaseWait.Close();
                pleaseWait.ChangeMainLabel();
                MessageBox.Show($"Error! Message - {exc?.Message ?? "No Message"}");
            }
        }

        private string MainResource_BulkUploadButton_UploadModel(string resource)
        {
            var overwriteMethod = StandardBulkUploadRequestForm.ModelUpload_OverwriteMethod;
            var bulkUploadMethod = StandardBulkUploadRequestForm.ModelUpload_BulkUploadMethod;
            var fieldsToUpload = StandardBulkUploadRequestForm.ModelUpload_FieldsToUpload;
            var saveZip = StandardBulkUploadRequestForm.ModelUpload_SaveZip;

            var jsonData = StandardBulkUploadRequestForm.ParseUploadModelData<object>();

            BulkUploadResponse bulkUploadResult = null;

            switch (resource)
            {
                case "items":
                    bulkUploadResult = client.Items.BulkUpload(StandardBulkUploadRequestForm.ParseUploadModelData<Item>(),
                        overwriteMethod, bulkUploadMethod, fieldsToUpload, saveZip);
                    break;
                //case "transactions":
                //    bulkUploadResult = client.Transactions.BulkUpload(StandardBulkUploadRequestForm.ParseUploadModelData<Transaction>(), 
                //        overwriteMethod, bulkUploadMethod, fieldsToUpload, saveZip);
                //    break;
                //case "activities":
                //    bulkUploadResult = client.Activities.BulkUpload(StandardBulkUploadRequestForm.ParseUploadModelData<Activity>(), 
                //        overwriteMethod, bulkUploadMethod, fieldsToUpload, saveZip);
                //    break;
                case "accountUsers":
                    bulkUploadResult = client.AccountUsers.BulkUpload(StandardBulkUploadRequestForm.ParseUploadModelData<AccountUser>(),
                        overwriteMethod, bulkUploadMethod, fieldsToUpload, saveZip);
                    break;
                case "userDefinedTables":
                    bulkUploadResult = client.UserDefinedTables.BulkUpload(StandardBulkUploadRequestForm.ParseUploadModelData<UserDefinedTable>(),
                        overwriteMethod, bulkUploadMethod, fieldsToUpload, saveZip);
                    break;
                default:
                    throw new Exception($"Logic For this Resource (\"{resource}\") is not available!");
            }

            return bulkUploadResult.JobID;
        }

        private string MainResource_BulkUploadButton_UploadCsv(string resource)
        {
            var overwriteMethod = StandardBulkUploadRequestForm.CsvUpload_OverwriteMethod;
            var filePath = StandardBulkUploadRequestForm.CsvUpload_FilePath;
            var zipFilePath = StandardBulkUploadRequestForm.CsvUpload_FilePathToStoreZipFile;
            var encoding = StandardBulkUploadRequestForm.CsvUpload_FileEncoding;

            BulkUploadResponse bulkUploadResult = null;

            switch (resource)
            {
                case "items":
                    bulkUploadResult = client.Items.BulkUpload(filePath, overwriteMethod, encoding, FilePathToStoreZipFile: zipFilePath);
                    break;
                //case "transactions":
                //    bulkUploadResult = client.Transactions.BulkUpload(filePath, overwriteMethod, encoding, FilePathToStoreZipFile: zipFilePath);
                //    break;
                //case "activities":
                //    bulkUploadResult = client.Activities.BulkUpload(filePath, overwriteMethod, encoding, FilePathToStoreZipFile: zipFilePath);
                //    break;
                case "accountUsers":
                    bulkUploadResult = client.AccountUsers.BulkUpload(filePath, overwriteMethod, encoding, FilePathToStoreZipFile: zipFilePath);
                    break;
                case "userDefinedTables":
                    bulkUploadResult = client.UserDefinedTables.BulkUpload(filePath, overwriteMethod, encoding, FilePathToStoreZipFile: zipFilePath);
                    break;
                default:
                    throw new Exception($"Logic For this Resource (\"{resource}\") is not available!");
            }

            return bulkUploadResult.JobID;
        }

        #endregion

        #region Generic

        private List<DropdownItem> GenerateMainResoourcesDropDownList()
        {
            return new List<DropdownItem>() {
                new DropdownItem("items", "Items"),
                //new DropdownItem("transactions", "Transactions"),
                //new DropdownItem("activities", "Activities"),
                new DropdownItem("accountUsers", "Account Users"),
                new DropdownItem("userDefinedTables", "User Defined Tables")
            };
        }

        private bool ValidateMainResourceValue(string mainResource)
        {
            var found = GenerateMainResoourcesDropDownList().FirstOrDefault(item => item.Value == mainResource);
            if (found == null)
            {
                MessageBox.Show($"Incorrect Main Resource Value!");
                return false;
            }

            return true;
        }

        public static string PrettyJson<TObject>(TObject data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data,
                                    Newtonsoft.Json.Formatting.Indented,
                                    new Newtonsoft.Json.JsonSerializerSettings
                                    {
                                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                                        DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc //will serialize date time as utc
                                    });
        }

        public static TObject ParseJson<TObject>(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TObject>(data,
                                    new Newtonsoft.Json.JsonSerializerSettings
                                    {
                                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                                        DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc //will serialize date time as utc
                                    });
        }

        private void ShowJson(string json)
        {
            GenericJsonResponseForm.SetText(json);
            GenericJsonResponseForm.ShowDialog();
        }








        #endregion

    }


}