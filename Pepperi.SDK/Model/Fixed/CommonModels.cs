using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{

    #region User Defined Collection

    public class UDC_UploadFile_Result
    {
        public string Message { get; set; }

        public int Total { get; set; }
        public int TotalInserted { get; set; }
        public int TotalUpdated { get; set; }
        public int TotalIgnored { get; set; }
        public int TotalFailed { get; set; }
        public int TotalMergedBeforeUpload { get; set; }
        public IEnumerable<UDC_UploadFile_Row> FailedRows { get; set; }
    }

    public class UDC_UploadFile_Row
    {
        public string Key { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
    }

    public class UDC_UploadFile_AuditLog_ResultObject
    {
        public string URI { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class UDC_UploadFile_Request
    {
        public string URI { get; set; }
        public bool? OverwriteObject { get; set; }
        public bool? OverwriteTable { get; set; }
    }

    public class UDC_Base_Row
    {
        public bool Hidden { get; set; }
        public string Key { get; set; }
    }

    public class UDC_ExportFile_Request
    {
        public string Format { get; set; }
        public string Where { get; set; }
        public string Fields { get; set; }
        public string Delimiter { get; set; }
        public IEnumerable<string> ExcludedKeys { get; set; }
    }

    public class UDC_ImportData_Request<TData>
    {
        public IEnumerable<TData> Objects { get; set; }
        public bool? OverwriteObject { get; set; }
    }

    public class UDC_ImportData_Response
    {
        public string Key { get; set; }
        public string Status { get; set; } // Update, Ignore, Insert
    }

    #endregion

    #region Generic Resource

    public class GenericResource_UploadFile_Result
    {
        public string Message { get; set; }

        public int Total { get; set; }
        public int TotalInserted { get; set; }
        public int TotalUpdated { get; set; }
        public int TotalIgnored { get; set; }
        public int TotalFailed { get; set; }
        public int TotalMergedBeforeUpload { get; set; }
        public IEnumerable<GenericResource_UploadFile_Row> FailedRows { get; set; }
    }

    public class GenericResource_UploadFile_Row
    {
        public string Key { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
    }

    public class GenericResource_UploadFile_AuditLog_ResultObject
    {
        public string URI { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class GenericResource_UploadFile_Request
    {
        public string URI { get; set; }
        public bool? OverwriteObject { get; set; }
        public bool? OverwriteTable { get; set; }
    }

    public class GenericResource_Base_Row
    {
        public bool Hidden { get; set; }
        public string Key { get; set; }
    }

    public class GenericResource_ExportFile_Request
    {
        public string Format { get; set; }
        public string Where { get; set; }
        public string Fields { get; set; }
        public string Delimiter { get; set; }
        public IEnumerable<string> ExcludedKeys { get; set; }
    }

    public class GenericResource_ImportData_Request<TData>
    {
        public IEnumerable<TData> Objects { get; set; }
        public bool? OverwriteObject { get; set; }
    }

    public class GenericResource_ImportData_Response
    {
        public string Key { get; set; }
        public string Status { get; set; } // Update, Ignore, Insert
    }

    #endregion
}
