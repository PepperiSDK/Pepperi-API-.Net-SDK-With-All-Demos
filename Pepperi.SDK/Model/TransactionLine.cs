using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class TransactionLine
	{
		 public DateTime? CreationDateTime 	{get; set; }
		 public DateTime? DeliveryDate 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public Int64? InternalID 	{get; set; }
		 public String ItemExternalID 	{get; set; }
		 public Int16? LineNumber 	{get; set; }
		 public Double? TotalUnitsPriceAfterDiscount 	{get; set; }
		 public Double? TotalUnitsPriceBeforeDiscount 	{get; set; }
		 public String TransactionExternalID 	{get; set; }
		 public Double? UnitDiscountPercentage 	{get; set; }
		 public Double? UnitPrice 	{get; set; }
		 public Double? UnitPriceAfterDiscount 	{get; set; }
		 public Double? UnitsQuantity 	{get; set; }
		 public Reference<Transaction> Transaction { get; set; }
		 public Reference<Item> Item { get; set; }
	}
}
