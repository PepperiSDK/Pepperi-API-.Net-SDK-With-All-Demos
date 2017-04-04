using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class SpecialPriceList
	{
		 public String CurrencySymbol 	{get; set; }
		 public String Description 	{get; set; }
		 public String ExternalID 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public Int64? InternalID 	{get; set; }
	}
}
