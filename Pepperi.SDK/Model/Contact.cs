using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class Contact
	{
		 public String AccountExternalID 	{get; set; }
		 public String Email 	{get; set; }
		 public String Email2 	{get; set; }
		 public String ExternalID 	{get; set; }
		 public String FirstName 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public Int64? InternalID 	{get; set; }
		 public String LastName 	{get; set; }
		 public String Mobile 	{get; set; }
		 public String Phone 	{get; set; }
		 public String Role 	{get; set; }
		 public Reference<Account> Account { get; set; }
	}
}
