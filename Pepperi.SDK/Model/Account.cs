using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class Account
	{
		 public String City 	{get; set; }
		 public String Country 	{get; set; }
		 public DateTime? CreationDate 	{get; set; }
		 public Decimal? Debts30 	{get; set; }
		 public Decimal? Debts60 	{get; set; }
		 public Decimal? Debts90 	{get; set; }
		 public Decimal? DebtsAbove90 	{get; set; }
		 public Double? Discount 	{get; set; }
		 public String Email 	{get; set; }
		 public String ExternalID 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public Int64? InternalID 	{get; set; }
		 public String Mobile 	{get; set; }
		 public String Name 	{get; set; }
		 public String Note 	{get; set; }
		 public String ParentExternalID 	{get; set; }
		 public String Phone 	{get; set; }
		 public String PriceListExternalID 	{get; set; }
		 public String Prop1 	{get; set; }
		 public String Prop2 	{get; set; }
		 public String Prop3 	{get; set; }
		 public String Prop4 	{get; set; }
		 public String Prop5 	{get; set; }
		 public String SpecialPriceListExternalID 	{get; set; }
		 public String State 	{get; set; }
		 public Int32? Status 	{get; set; }
		 public String Street 	{get; set; }
		 public String Type 	{get; set; }
		 public Int64? TypeDefinitionID 	{get; set; }
		 public String ZipCode 	{get; set; }
		 public Reference<Account> Parent { get; set; }
		 public Reference<PriceList> PriceList { get; set; }
		 public Reference<SpecialPriceList> SpecialPriceList { get; set; }
		 public References<AccountCatalog> Catalogs { get; set; }
		 public References<User> Users { get; set; }
	}
}
