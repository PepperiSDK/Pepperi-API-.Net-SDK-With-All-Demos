using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.MetaData
{
    public class UDC_MetaData
    {
        public bool? GenericResource { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public UDC_Meta_SyncData SyncData { get; set; }
        public IDictionary<string, UDC_Meta_Field> Fields { get; set; }
        public UDC_Meta_DocumentKey DocumentKey { get; set; }

        public string Type { get; set; }

        public UDC_Meta_ListView ListView { get; set; }
        public bool? Hidden { get; set; }

        public string Name { get; set; }
        public string AddonUUID { get; set; }
    }

    public class UDC_Meta_DocumentKey
    {
        public string Delimiter { get; set; }
        public string Type { get; set; }
        public IEnumerable<string> Fields { get; set; }
    }

    public class UDC_Meta_Field
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
        public UDC_Meta_FieldItems Items { get; set; }
    }

    public class UDC_Meta_FieldItems
    {
        public string Type { get; set; }
    }

    public class UDC_Meta_SyncData
    {
        public bool Sync { get; set; }
        public bool SyncFieldLevel { get; set; }
    }

    public class UDC_Meta_ListView
    {
        public IEnumerable<UDC_Meta_ListView_Column> Columns { get; set; }
        public string Type { get; set; }
        public IEnumerable<UDC_Meta_ListView_Field> Fields { get; set; }
    }

    public class UDC_Meta_ListView_Column
    {
        public int Width { get; set; }
    }

    public class UDC_Meta_ListView_Field
    {
        public bool ReadOnly { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string FieldID { get; set; }
        public bool Mandatory { get; set; }
    }
}
