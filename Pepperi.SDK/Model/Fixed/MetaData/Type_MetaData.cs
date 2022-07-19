using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.MetaData
{
    public class Type_MetaData
    {
        public long? TypeID { get; set; }

        /// <summary>
        /// The external ID of the type
        /// </summary>
        public string ExternalID { get; set; }

        public string Description { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public bool? Hidden { get; set; }

        public Owner Owner { get; set; }
    }

}
