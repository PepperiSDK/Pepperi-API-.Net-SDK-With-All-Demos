using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class Transaction
	{
		 public String AccountExternalID 	{get; set; }
		 public DateTime? ActionDateTime 	{get; set; }
		 public Int64? ActivityTypeID 	{get; set; }
		 public String AgentExternalID 	{get; set; }
		 public String BillToCity 	{get; set; }
		 public String BillToCountry 	{get; set; }
		 public String BillToFax 	{get; set; }
		 public String BillToName 	{get; set; }
		 public String BillToPhone 	{get; set; }
		 public String BillToState 	{get; set; }
		 public String BillToStreet 	{get; set; }
		 public String BillToZipCode 	{get; set; }
		 public String CatalogExternalID 	{get; set; }
		 public String ContactPersonExternalID 	{get; set; }
		 public DateTime? CreationDateTime 	{get; set; }
		 public String CurrencySymbol 	{get; set; }
		 public DateTime? DeliveryDate 	{get; set; }
		 public Decimal? DiscountPercentage 	{get; set; }
		 public String ExternalID 	{get; set; }
		 public Double? GrandTotal 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public Int64? InternalID 	{get; set; }
		 public DateTime? ModificationDateTime 	{get; set; }
		 public String OriginAccountExternalID 	{get; set; }
		 public String Remark 	{get; set; }
		 public String ShipToCity 	{get; set; }
		 public String ShipToCountry 	{get; set; }
		 public String ShipToExternalID 	{get; set; }
		 public String ShipToFax 	{get; set; }
		 public String ShipToName 	{get; set; }
		 public String ShipToPhone 	{get; set; }
		 public String ShipToState 	{get; set; }
		 public String ShipToStreet 	{get; set; }
		 public String ShipToZipCode 	{get; set; }
		 public Int32? Status 	{get; set; }
		 public Double? SubmissionGeoCodeLAT 	{get; set; }
		 public Double? SubmissionGeoCodeLNG 	{get; set; }
		 public Double? SubTotal 	{get; set; }
		 public Double? SubTotalAfterItemsDiscount 	{get; set; }
		 public Double? TaxPercentage 	{get; set; }
		 public Double? TotalItemsCount 	{get; set; }
		 public String Type 	{get; set; }
		 public Reference<Account> OriginAccount    { get; set; }
		 public Reference<Account> Account  { get; set; }
		 public Reference<Catalog> Catalog  { get; set; }
		 public Reference<Contact> ContactPerson { get; set; }
		 public Reference<User> Creator     { get; set; }
		 public Reference<User> Agent       { get; set; }
		 public References<TransactionLine> TransactionLines   { get; set; }
	}
}
