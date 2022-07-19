using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.MetaData
{
    public class UserDefinedCollection_MetaData
    {
        public bool GenericResource { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public UserDefinedCollection_SyncData SyncData { get; set; }
        public IDictionary<string, UserDefinedCollection_Field> Fields { get; set; }

        public string Type { get; set; }
        public bool? Hidden { get; set; }

        public string Name { get; set; }
        public string AddonUUID { get; set; }
    }

    public class UserDefinedCollection_Field
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
        public UserDefinedCollection_FieldItems Items { get; set; }
    }

    public class UserDefinedCollection_FieldItems
    {
        public string Type { get; set; }
    }

    public class UserDefinedCollection_SyncData
    {
        public bool Sync { get; set; }
        public bool SyncFieldLevel { get; set; }
    }
}
