using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.MetaData
{
    public class UserDefinedTable_MetaData
    {
        public string TableID { get; set; }

        public UserDefinedTale_MainKeyType MainKeyType { get; set; }

        public UserDefinedTale_SecondaryKeyType SecondaryKeyType { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public bool? Hidden { get; set; }

        public Owner Owner { get; set; }

    }

    public class UserDefinedTale_MainKeyType
    {
        public long? ID { get; set; }

        public string Name { get; set; }
    }

    public class UserDefinedTale_SecondaryKeyType
    {
        public long? ID { get; set; }

        public string Name { get; set; }
    }

}
