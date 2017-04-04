using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    /// <summary>
    /// Holds image data for upload (psot) and get (download)
    /// </summary>
    public class Image
    {
        public string URL       { get; set; }   //URL or Content is sent to the server on upload (post)     URL is returned from server on download (get)
        public string Content   { get; set; }   //URL or Content is sent to the server on upload (post)       //byte[] => encoded to base64=> (byptes and convenrt to 64)
        public string MimeType  { get; set; }   //MimeType is sent to the server on upload (post)
        public string FileName  { get; set; }   //FileName is sent to the server on upload (post)
    }
}
