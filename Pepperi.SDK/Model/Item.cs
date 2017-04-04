using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class Item
	{
		 public Boolean? AllowDecimal 	{get; set; }
		 public Double? CaseQuantity 	{get; set; }
		 public Decimal? CostPrice 	{get; set; }
		 public String Dimension1Code 	{get; set; }
		 public String Dimension1Name 	{get; set; }
		 public String Dimension2Code 	{get; set; }
		 public String Dimension2Name 	{get; set; }
		 public Decimal? Discount 	{get; set; }
		 public String ExternalID 	{get; set; }
		 public DateTime? FutureAvailabilityDate 	{get; set; }
		 public Double? FutureAvailabilityQuantity 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public Image Image 	{get; set; }
		 public Image Image2 	{get; set; }
		 public Image Image3 	{get; set; }
		 public Image Image4 	{get; set; }
		 public Image Image5 	{get; set; }
		 public Image Image6 	{get; set; }
		 public Int64? InternalID 	{get; set; }
		 public String LongDescription 	{get; set; }
		 public String MainCategoryID 	{get; set; }
		 public Double? MinimumQuantity 	{get; set; }
		 public String Name 	{get; set; }
		 public String ParentExternalID 	{get; set; }
		 public Decimal? Price 	{get; set; }
		 public String Prop1 	{get; set; }
		 public String Prop2 	{get; set; }
		 public String Prop3 	{get; set; }
		 public String Prop4 	{get; set; }
		 public String Prop5 	{get; set; }
		 public String Prop6 	{get; set; }
		 public String Prop7 	{get; set; }
		 public String Prop8 	{get; set; }
		 public String Prop9 	{get; set; }
		 public Decimal? SecondaryPrice 	{get; set; }
		 public String UPC 	{get; set; }
		 public References<Inventory> Inventory { get; set; }
	}
}
