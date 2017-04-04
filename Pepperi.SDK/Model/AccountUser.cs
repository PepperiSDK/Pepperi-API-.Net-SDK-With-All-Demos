using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class AccountUser
	{
		 public String AccountExternalID 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public String UserExternalID 	{get; set; }
		 public Reference<Account> Account { get; set; }
		 public Reference<User> User { get; set; }
	}
}
