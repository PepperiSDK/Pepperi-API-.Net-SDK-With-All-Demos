using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class Inventory
	{
		 public Double? InStockQuantity 	{get; set; }
		 public Int64? InternalID 	{get; set; }
		 public String ItemExternalID 	{get; set; }
		 public Reference<Item> Item { get; set; }
	}
}
