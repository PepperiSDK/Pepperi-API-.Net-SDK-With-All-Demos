using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
    public class ItemPrice
    {
        public DateTime? CreationDateTime { get; set; }
        public Boolean? Hidden { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public Decimal? Price { get; set; }
        public String ItemExternalID { get; set; }
        public String PriceListExternalID { get; set; }
        public Reference<PriceList> PriceList { get; set; }
        public Reference<Item> Item { get; set; }
    }
}
