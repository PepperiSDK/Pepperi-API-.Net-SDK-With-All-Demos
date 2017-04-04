using Pepperi.SDK.Exceptions;
using Pepperi.SDK.Model.Fixed;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Helpers
{
    internal class PepperiFlatSerializer
    {
        /// <summary>
        /// converts data to flat text representation
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="data"></param>
        /// <param name="PropertiesToInclude"></param>
        /// <param name="lineValueForNullValue"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1. null values in the data are mapped to lineValueForNullValue
        /// 2. datetime values are mapped to utc date time
        /// </remarks>
        internal static FlatModel MapDataToFlatModel<TModel>(IEnumerable<TModel> data, IEnumerable<string> PropertiesToInclude, string lineValueForNullValue)
        {
            Type modelType = data.GetType().GetGenericArguments().Single();
            var modelProperties = modelType.GetProperties();

            #region validate input: Reference,References Properties can not be included

            foreach (PropertyInfo property in modelProperties)
            {
                //exculde Reference properties
                if (PropertiesToInclude.Contains(property.Name) && property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Reference<>))
                {
                    throw new PepperiException("Operation failed. Reason: Reference Properties can not be included. (" + property.Name + ")");
                }

                //exculde References properties
                if (PropertiesToInclude.Contains(property.Name) && property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(References<>))
                {
                    throw new PepperiException("Operation failed. Reason: References Properties can not be included. (" + property.Name + ")");
                }
            }

            #endregion

            #region caculate relevantProperties

            List<PropertyInfo> relevantProperties = new List<PropertyInfo>();
      
            foreach (PropertyInfo property in modelProperties)
            {
                if (PropertiesToInclude != null && PropertiesToInclude.Contains(property.Name))
                {
                    relevantProperties.Add(property);
                }
            }

            #endregion

            #region calculate flat model Headers  by relevantProperties

            List<string> Headers = new List<string>();

            foreach (var relevantProperty in relevantProperties)
            {
                Headers.Add(relevantProperty.Name);
            }

            #endregion

            #region calculate flat model Lines

            List<FlatLine> FlatLines = new List<FlatLine>();

            foreach (var dataItem in data)
            {
                FlatLine FlatLine = new FlatLine();

                foreach (var flatModelProperty in relevantProperties)
                {
                    object fieldValueAsObject = flatModelProperty.GetValue(dataItem);

                    //handle null
                    if (fieldValueAsObject == null)
                    {
                        FlatLine.Add(lineValueForNullValue);                 
                        continue;
                    }

                    //handle date
                    if (flatModelProperty.PropertyType == typeof(Nullable<DateTime>))
                    {
                        DateTime? fieldValueAsDateTime = fieldValueAsObject as DateTime?;
                        string fieldValueAsString = (fieldValueAsDateTime == null) ? lineValueForNullValue : fieldValueAsDateTime.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");
                        FlatLine.Add(fieldValueAsString);
                        continue;
                    }

                    //default
                    string fieldValue = fieldValueAsObject.ToString();
                    FlatLine.Add(fieldValue);
                }

                FlatLines.Add(FlatLine);
            }

            #endregion

            FlatModel FlatModel = new FlatModel(Headers, FlatLines);

            return FlatModel;
        }

        internal static string FlatModelToCsv(FlatModel FlatModel)
        {
            StringBuilder sbCsv = new StringBuilder();

            #region create header

            string csvHeader = string.Join(",", FlatModel.Headers);
            sbCsv.AppendLine(csvHeader);

            #endregion

            #region Create Lines

            foreach (var line in FlatModel.Lines)
            {
                StringBuilder sbCsvLine = new StringBuilder();

                for (int fieldIndex=0; fieldIndex <line.Count(); fieldIndex++)
                {
                    //add the value to the string builder
                    string fieldValue = line[fieldIndex];
                    string CsvValue = FieldValueToCsvValue(fieldValue);
                    sbCsvLine.Append(CsvValue);

                    //add seperator before the next field to the string builder
                    if (fieldIndex != line.Count() - 1)
                    {
                        sbCsvLine.Append(',');
                    }
                }

                string csvLine = sbCsvLine.ToString();
                sbCsv.AppendLine(csvLine); 
            }

            #endregion

            string result = sbCsv.ToString();
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="FilePathToStoreZipFile">Optional. We can store the generated zip file for debugging purpose.</param>
        /// <returns></returns>
        /// <remarks>
        /// 1. using utf8 (https://msdn.microsoft.com/en-us/library/system.text.encoding.utf8(v=vs.110).aspx)
        /// </remarks>
        internal static byte[] UTF8StringToZip(string theString, string FilePathToStoreZipFile)
        {
            byte[] result = null;

            using (var memoryStream = new MemoryStream())
            {
                //Create an archive and store the stream in memory.
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    //create a zip entry for the attachment
                    var zipArchiveEntry = zipArchive.CreateEntry("data.csv");

                    //Get the stream of the attachment
                    using (var entryStream = zipArchiveEntry.Open())
                    {
                        // creates a stream with BOM at the beggining
                        using (var streamWriter = new StreamWriter(entryStream, new UTF8Encoding(true)))
                        {
                            streamWriter.Write(theString);
                        }
                    }
                }

                memoryStream.Close();
                result = memoryStream.ToArray();
            }

            #region write the zip to local file (for testing)

            if (FilePathToStoreZipFile != null)
            {
                using (FileStream _FileStream = new FileStream(FilePathToStoreZipFile, FileMode.Create, FileAccess.Write))
                {
                    // Writes a block of bytes to this stream using data from a byte array.
                    _FileStream.Write(result, 0, result.Length);
                    // close file stream
                    _FileStream.Close();
                }

            }
           

            #endregion


            return result;

        }


        #region Private methods

        private static string Map(int AsciiCode)
        {
            char c = (char)AsciiCode;
            string s = c.ToString();
            return s;
        }

        /// <summary>
        /// handles special characters
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        private static string FieldValueToCsvValue(string fieldValue)
        {
            //replace new lines with empty string
            if (fieldValue.IndexOf(Environment.NewLine) > -1) 
            {
                fieldValue = fieldValue.Replace(Environment.NewLine, "");
            }

            if (fieldValue.IndexOf(Map(10)) > -1 || fieldValue.IndexOf(Map(13)) > -1) 
            {
                fieldValue = fieldValue.Replace(Map(10), "");
                fieldValue = fieldValue.Replace(Map(13), "");
            }

            if (fieldValue.IndexOf(Map(34)) > -1) //"
            {
                fieldValue = fieldValue.Replace(Map(34), Map(34)+Map(34));      //replace any occurence of " with ""
                fieldValue = Map(34) + fieldValue + Map(34);                    //wrap the value with " on both sides.
            } 
            else if (fieldValue.IndexOf(",") > -1) 
            {
                fieldValue = Map(34) + fieldValue + Map(34);                    //wrap the value with " on both sides.
            }

            return fieldValue;
        }
    
        #endregion
    }


    #region Model

    internal class FlatModel
    {
        #region Properties

        public IEnumerable<string> Headers { get; set; }
        public IEnumerable<FlatLine> Lines { get; set; }

        #endregion

        #region Constructor

        public FlatModel(IEnumerable<string> Headers, IEnumerable<FlatLine> Lines)
        {
            this.Headers = Headers;
            this.Lines = Lines;
        }

        #endregion
    }

    internal class FlatLine : List<string>
    {
    }

    #endregion

}


/*
string GetValidCsvValue(string sValToAppend, string sTextQ, string sepChar)
{
 if (sValToAppend.IndexOf(Environment.NewLine) > -1) {
  sValToAppend = sValToAppend.Replace(Environment.NewLine, "");
 }
 if (sValToAppend.IndexOf(Strings.Chr(10)) > -1 | sValToAppend.IndexOf(Strings.Chr(13)) > -1) {
  sValToAppend = sValToAppend.Replace(Strings.Chr(10), "").Replace(Strings.Chr(13), "");
 }

 if (sValToAppend.IndexOf(Strings.Chr(34)) > -1) {
  sValToAppend = sValToAppend.Replace(sTextQ, Strings.Chr(34)+Strings.Chr(34));
  sValToAppend = Strings.Chr(34) + sValToAppend + Strings.Chr(34);
 } else if (sValToAppend.IndexOf(sepChar) > -1) {
  sValToAppend = Strings.Chr(34) + sValToAppend + Strings.Chr(34);
 }
 return sValToAppend;
}

sValToAppend- field value
do not send- sTextQ        "/""
do not send - sepChar ","


 * 
*/