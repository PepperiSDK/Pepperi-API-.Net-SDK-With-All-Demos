using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class Catalog
	{
		 public DateTime? CreationDate 	{get; set; }
		 public String Description 	{get; set; }
		 public DateTime? ExpirationDate 	{get; set; }
		 public String ExternalID 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public Int64? InternalID 	{get; set; }
		 public Boolean? IsActive 	{get; set; }
	}
}
