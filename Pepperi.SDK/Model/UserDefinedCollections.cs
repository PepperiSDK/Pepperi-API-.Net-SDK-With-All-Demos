using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Model
{
	public class UserDefinedCollection_MyTestCollection
	{
		 public Int64? testField2 	{get; set; }
		 public string testField1 	{get; set; }
		 public Boolean? testBoolField 	{get; set; }
		 public DateTime? testDateTimeField 	{get; set; }
		 public Double? testDoubleField 	{get; set; }
		 public IEnumerable<string> testStringArrayField 	{get; set; }
		 public string key 	{get; set; }
	}


	public class UserDefinedCollection_MyTestCollectionFromPost
	{
		 public string testField3 	{get; set; }
		 public Int64? testField2 	{get; set; }
		 public string testField1 	{get; set; }
		 public string key 	{get; set; }
	}


}
