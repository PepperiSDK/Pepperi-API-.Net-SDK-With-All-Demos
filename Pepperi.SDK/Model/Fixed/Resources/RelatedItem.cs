using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.Resources
{
    public class RelatedItem
    {
        public string Key { get; set; }
        public string CollectionName { get; set; }
        public string ItemExternalID { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public IEnumerable<string> RelatedItems { get; set; }
        public Boolean? Hidden { get; set; }
    }
}
