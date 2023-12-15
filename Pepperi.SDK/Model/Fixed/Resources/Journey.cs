using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.Resources
{
    public class JourneyFile
    {
        public DateTime? ModificationDateTime { get; set; }
        public string FileVersion { get; set; }
        public string Folder { get; set; }
        public string MIME { get; set; }
        public string CreationDateTime { get; set; }
        public string Sync { get; set; }
        public string Description { get; set; }
        public JourneyFile_MetaData MetaData { get; set; }
        public string URL { get; set; }
        public bool Hidden { get; set; }
        public bool Cache { get; set; }
        public int FileSize { get; set; }
        public string UploadedBy { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
    }

    public class JourneyFile_MetaData
    {
        public string Email { get; set; }
        public bool IsWebApp { get; set; }
        [JsonProperty("platformType")]
        public string PlatformType { get; set; }
        public string UserLastName { get; set; }
        public string UserFirstName { get; set; }
        [JsonProperty("docale")]
        public string Locale { get; set; }
        [JsonProperty("deviceID")]
        public string DeviceID { get; set; }
        [JsonProperty("timeZoneDiff")]
        public string TimeZoneDiff { get; set; }
        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }
        [JsonProperty("systemVersion")]
        public string SystemVersion { get; set; }
        [JsonProperty("systemName")]
        public string SystemName { get; set; }
        [JsonProperty("screenType")]
        public string ScreenType { get; set; }
        [JsonProperty("deviceModel")]
        public string DeviceModel { get; set; }
        public string UUID { get; set; }

        [JsonProperty("softwareVersion")]
        public string SoftwareVersion { get; set; }
    }

    public class Journey<TData>
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public TData Data { get; set; }
    }

    public class SearchJourneysResult<TData>
    {
        public IEnumerable<Journey<TData>> Journeys { get; set; }
        public IEnumerable<SearchJourneysResult_FailedFiles> FailedFiles { get; set; }

        public SearchJourneysResult()
        {
            this.Journeys = new List<Journey<TData>>();
            this.FailedFiles = new List<SearchJourneysResult_FailedFiles>();
        }
    }

    public class SearchJourneysResult_FailedFiles
    {
        public string FileUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
}
