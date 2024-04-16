using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public class MultipartOverwriteImportOptions
    {
        public string SchemeName { get; set; }
        public IEnumerable<MultipartOverwriteFileImport> FilesImports { get; set; }
        public int PoolingInternvalInMs { get; set; } = 5000;
        public int NumberOfPoolingAttempts { get; set; } = 500;
    }

    public class MultipartOverwriteFileImport
    {
        public string FilePath { get; set; }
        public string JsonData { get; set; }
        public byte[] BytesData { get; set; }
        public string FileExtention { get; set; }

    }

    public class MultipartOverwriteImportResult
    {
        public IEnumerable<MultipartOverwriteFileImportResult> FilesImports { get; set; }
    }

    public class MultipartOverwriteFileImportResult
    {
        public UDC_UploadFile_Result UploadFileResult { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class MultipartOverwriteInitializeResponse
    {
        public string MultipartOverwriteKey { get; set; }
    }
}
