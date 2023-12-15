using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.Resources
{
    public class Notification
    {
        public string Key { get; set; } // read-only, auto-generate
        public DateTime? CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
        public bool? Hidden { get; set; }
        public bool? Read { get; set; } // false by default
        public string CreatorUUID { get; set; } // read-only, auto-generate
        public string CreatorName { get; set; } // read-only, auto-generate ?

        public string Body { get; set; }
        public string Title { get; set; }
        public string UserUUID { get; set; }
        public string UserEmail { get; set; }
        public string NavigationPath { get; set; } // The destination path when clicking the notification
    }
}
