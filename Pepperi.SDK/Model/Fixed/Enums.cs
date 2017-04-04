using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public enum eBulkUploadMethod
    {
        Json,
        Zip
    }

    /// <summary>
    /// parameter for Bulk upload sent to API
    /// </summary>
    public enum eOverwriteMethod
    {
        none,                       //entities that are not given in the input, remains unchanged on Pepperi.
        full,                       //entities that are not given in the input, are removed from Pepperi.
        selective
    }


    /// <summary>
    /// Object status in Pepperi.
    /// </summary>
    public enum eStatus
    {
        InCreation = 1,             //editable 
        Submited = 2,               //read only.    Changes were successfully saved to Pepperi. It is ready for upload to ERP (via FTP or API).
        OnHold = 4,                 //read only.
        Cancelled = 5,              //read only.
        NeedRevision = 6,           //editable
        Closed = 7,                 //read only
        Failed = 8,                 //read only
        NeedApproval = 9,           //editable by manager only
        ERP = 12,                   //read only
        Invoice = 14,               //read only
        Published = 17,             //activities only - for activity planning – opend in ipad in planning
        Paid = 19,                  // read only – payment made succesfully
    }

    
    public enum eUiControlType
    {
        TextBox = 1,
        LimitedLengthTextBox = 2,
        TextArea = 3,
        Date = 5,
        DateAndTime = 6,
        NumberInetger = 7,
        NumberReal = 8,
        Currency = 9,
        Boolean = 10,
        ComboBox = 11,
        MultiTickBox = 12,
        Email = 18,
        Image = 20,
        Attachment = 24,
        Link = 26,
        Phone = 44
    }

    /// <summary>
    /// as we set the value from samples, we take it from the enum
    /// </summary>
    public enum eUserDefinedFieldType
    {
        TypeSafeAttributeConfigBoolean = 1,
        TypeSafeAttributeConfigDouble = 2,
        TypeSafeAttributeConfigDateTime = 3,
        TypeSafeAttributeConfigDate = 4,
        TypeSafeAttributeConfigInteger = 5,
        TypeSafeAttributeConfigString = 6,
        TypeSafeAttributeConfigSingleStringValueFromList = 7,
        TypeSafeAttributeConfigMultiStringValuesFromList = 8
    }
}
