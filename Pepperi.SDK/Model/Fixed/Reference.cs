using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public class Reference<TModel>
        where TModel : new()
    {
        public Reference()
        {
            Data = new TModel();
        }
        public TModel Data { get; set; }
        public string URI { get; set; }
    }
}
