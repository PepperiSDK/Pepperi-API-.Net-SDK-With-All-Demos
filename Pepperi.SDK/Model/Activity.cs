using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class Activity
	{
		 public String AccountExternalID 	{get; set; }
		 public DateTime? ActionDateTime 	{get; set; }
		 public Int64? ActivityTypeID 	{get; set; }
		 public String AgentExternalID 	{get; set; }
		 public String ContactPersonExternalID 	{get; set; }
		 public DateTime? CreationDateTime 	{get; set; }
		 public Double? CreationGeoCodeLAT 	{get; set; }
		 public Double? CreationGeoCodeLNG 	{get; set; }
		 public String ExternalID 	{get; set; }
		 public Boolean? Hidden 	{get; set; }
		 public Int64? InternalID 	{get; set; }
		 public DateTime? ModificationDateTime 	{get; set; }
		 public Int32? PlannedDuration 	{get; set; }
		 public DateTime? PlannedEndTime 	{get; set; }
		 public DateTime? PlannedStartTime 	{get; set; }
		 public Int32? Status 	{get; set; }
		 public Double? SubmissionGeoCodeLAT 	{get; set; }
		 public Double? SubmissionGeoCodeLNG 	{get; set; }
		 public String Title 	{get; set; }
		 public String Type 	{get; set; }
		 public Reference<Account> Account { get; set; }
		 public Reference<Contact> ContactPerson { get; set; }
		 public Reference<User> Agent { get; set; }
		 public Reference<User> Creator { get; set; }
		 public References<Contact> ContactPersonList { get; set; }
	}
}
