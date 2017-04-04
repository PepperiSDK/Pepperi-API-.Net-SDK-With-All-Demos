using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public class References<TModel>
        where TModel : new()
    {
        public References()
        {
            Data = new List<TModel>();
        }
        public List<TModel> Data { get; set; }
        public string URI { get; set; }
    }
}
