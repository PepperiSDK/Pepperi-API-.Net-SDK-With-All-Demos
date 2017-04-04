using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class AccountCatalog
	{
		 public String AccountExternalID 	{get; set; }
		 public String CatalogExternalID 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public String PriceListExternalID 	{get; set; }
		 public Reference<Account> Account { get; set; }
		 public Reference<Catalog> Catalog { get; set; }
	}
}
