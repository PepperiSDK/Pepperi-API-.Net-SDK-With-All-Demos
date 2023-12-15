using Pepperi.SDK.Model.Fixed;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormApiDemo.Model;

namespace WinFormApiDemo.General_Forms
{
    public partial class StandardBulkUploadRequestForm : Form
    {

        public eStandardResources_BulkUploadType BulkUploadType { get; set; }
        public bool FormWasLoaded { get; set; } = false;
        public EncodingInfo DefaultEncodingInfo { get; set; }
        public string LastOpenedFor { get; set; }

        #region Model Upload

        public eOverwriteMethod ModelUpload_OverwriteMethod { get; set; }
        public eBulkUploadMethod ModelUpload_BulkUploadMethod { get; set; }
        public bool ModelUpload_SaveZip { get; set; }
        public IEnumerable<string> ModelUpload_FieldsToUpload { get; set; }
        public string ModelUpload_JsonData { get; set; }

        #endregion

        #region CSV Upload

        public eOverwriteMethod CsvUpload_OverwriteMethod { get; set; }
        public string CsvUpload_FilePath { get; set; }
        public string CsvUpload_FilePathToStoreZipFile { get; set; }
        public Encoding CsvUpload_FileEncoding { get; set; }

        #endregion

        public StandardBulkUploadRequestForm()
        {
            InitializeComponent();
        }

        public void PrepareForm(string openFor)
        {
            if (LastOpenedFor != openFor)
            {
                ResetInputsToDefault();
                LastOpenedFor = openFor;
            }
        }

        private void ResetInputsToDefault()
        {
            Clear_ModelUpload();
            Clear_CsvUpload();
        }

        private void StandardBulkUploadRequestForm_Load(object sender, EventArgs e)
        {
            if (modelUpload_OverwriteMethod_ComboBox.SelectedItem == null)
                modelUpload_OverwriteMethod_ComboBox.SelectedItem = "none";
            if (modelUpload_UploadMethod_ComboBox.SelectedItem == null)
                modelUpload_UploadMethod_ComboBox.SelectedItem = "Json";

            if (csvUpload_OverwriteMethod_ComboBox.SelectedItem == null)
                csvUpload_OverwriteMethod_ComboBox.SelectedItem = "none";

            if (!FormWasLoaded)
            {
                var encodings = Encoding.GetEncodings();
                var encodingsList = new List<EncodingInfo>() { };
                for (int i = 0; i < encodings.Length; i++)
                {
                    encodingsList.Add(encodings[i]);
                }

                var csvUpload_FileEncoding_BindingSource = new BindingSource
                {
                    DataSource = encodingsList
                };

                csvUpload_FileEncoding_ComboBox.DataSource = csvUpload_FileEncoding_BindingSource.DataSource;

                csvUpload_FileEncoding_ComboBox.DisplayMember = "DisplayName";
                csvUpload_FileEncoding_ComboBox.ValueMember = "Name";

                var utf8EncodingInfo = encodingsList.FirstOrDefault(enc => enc.Name == "utf-8");
                this.DefaultEncodingInfo = utf8EncodingInfo;
                csvUpload_FileEncoding_ComboBox.SelectedItem = this.DefaultEncodingInfo;
            }


            if (!FormWasLoaded) FormWasLoaded = true;
        }

        private void modelUpload_Upload_Button_Click(object sender, EventArgs e)
        {
            try
            {
                var overwriteMethod = (string)this.modelUpload_OverwriteMethod_ComboBox.SelectedItem;
                this.ModelUpload_OverwriteMethod = ParseEnum<eOverwriteMethod>(overwriteMethod);

                var bulkUploadMethod = (string)this.modelUpload_UploadMethod_ComboBox.SelectedItem;
                this.ModelUpload_BulkUploadMethod = ParseEnum<eBulkUploadMethod>(bulkUploadMethod);

                var fieldsToUploadValue = this.modelUpload_FieldsToUpload_TextBox.Text;
                Validate(fieldsToUploadValue, "Fields to Upload are empty! Please Fill it! (comma separated)");
                var parsedFields = fieldsToUploadValue.Split(',')
                    .Select(splitted => splitted.Trim())
                    .Where(finalField => !string.IsNullOrEmpty(finalField));

                this.ModelUpload_FieldsToUpload = parsedFields;

                this.ModelUpload_JsonData = this.jsonDataTextBox.Text;
                Validate(ModelUpload_JsonData, "JSON Model Text Box is empty!");

                IEnumerable<object> parsed = null;
                try
                {
                    parsed = ParseUploadModelData<object>();
                }
                catch (Exception)
                {
                    Validate(false, "Can't parse JSON Model! Should be not empty JSON Array of Objects ([{...}, {...}])");
                }
                Validate(parsed != null && parsed.Count() > 0, "Parsed Array is empty! Should be not empty JSON Array of Objects ([{...}, {...}])");

                this.ModelUpload_SaveZip = this.modelUpload_SaveZip_CheckBox.Checked;

                this.BulkUploadType = eStandardResources_BulkUploadType.Model;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex?.Message ?? "No Message");
            }
        }

        public IEnumerable<TData> ParseUploadModelData<TData>()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TData>>(this.ModelUpload_JsonData);
        }

        private void Validate(string value, string message)
        {
            if (string.IsNullOrEmpty(value)) throw new Exception(message);
        }

        private void Validate(bool value, string message)
        {
            if (!value) throw new Exception(message);
        }

        private T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        private void modelUpload_Clear_Button_Click(object sender, EventArgs e)
        {
            Clear_ModelUpload();
        }

        private void csvUpload_SelectFile_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV Files|*.csv";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result != DialogResult.OK) return;

            var filePath = openFileDialog1.FileName;

            this.csvUpload_FilePath_TextBox.Text = filePath;
        }

        private void csvUpload_Upload_Button_Click(object sender, EventArgs e)
        {
            try
            {
                var filePath = this.csvUpload_FilePath_TextBox.Text;
                Validate(filePath, "File Path is empty! Please select .csv file!");

                var fileExtention = Path.GetExtension(filePath);
                Validate(fileExtention == ".csv", "Please select only .csv files!");

                Validate(File.Exists(filePath), "File is not exist! Please select only existing files!");
                this.CsvUpload_FilePath = filePath;

                var overwriteMethod = (string)this.csvUpload_OverwriteMethod_ComboBox.SelectedItem;
                this.CsvUpload_OverwriteMethod = ParseEnum<eOverwriteMethod>(overwriteMethod);

                var filePathToStoreZipFile = this.csvUpload_FilePathToStoreZipFile_TextBox.Text;
                this.CsvUpload_FilePathToStoreZipFile = string.IsNullOrEmpty(filePathToStoreZipFile) ? null : filePathToStoreZipFile;

                var fileEncoding = ((EncodingInfo)csvUpload_FileEncoding_ComboBox.SelectedItem).GetEncoding();
                this.CsvUpload_FileEncoding = fileEncoding;

                this.BulkUploadType = eStandardResources_BulkUploadType.CsvPath;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex?.Message ?? "No Message");
            }

        }

        private void csvUpload_Clear_Button_Click(object sender, EventArgs e)
        {
            Clear_CsvUpload();
        }

        private void Clear_CsvUpload()
        {
            csvUpload_OverwriteMethod_ComboBox.SelectedItem = "none";

            this.csvUpload_FilePath_TextBox.Text = "";
            this.csvUpload_FilePathToStoreZipFile_TextBox.Text = "";

            csvUpload_FileEncoding_ComboBox.SelectedItem = this.DefaultEncodingInfo;
        }

        private void Clear_ModelUpload()
        {
            modelUpload_OverwriteMethod_ComboBox.SelectedItem = "none";
            modelUpload_UploadMethod_ComboBox.SelectedItem = "Json";

            modelUpload_SaveZip_CheckBox.Checked = false;

            this.modelUpload_FieldsToUpload_TextBox.Text = "";
            this.jsonDataTextBox.Text = "";
        }
    }
}
