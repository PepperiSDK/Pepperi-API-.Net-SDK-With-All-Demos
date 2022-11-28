using Pepperi.ModelGenerator.Model;
using Pepperi.SDK;
using Pepperi.SDK.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Pepperi.SDK.Helpers;
using Pepperi.SDK.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Pepperi.ModelGenerator.Model.Common;
using Pepperi.SDK.Model.Fixed;
using Pepperi.SDK.Model.Fixed.MetaData;
using Pepperi.SDK.Contracts;

namespace Pepperi.ModelGenerator
{
    /// <summary>
    /// generates the models
    /// </summary>
    /// <remarks>
    /// 1. The relations (Reference, References proeprties) are hard coded in the generator, rather than using metadata api.
    /// </remarks>
    internal class ModelGenerator
    {
        #region Properties

        private ApiClient ApiClient { get; set; }

        private Dictionary<eModelClassName, List<string>> ModelClassNameToHardCodedFields { get; set; }

        #endregion

        #region constructor

        public ModelGenerator(ApiClient ApiClient)
        {
            this.ApiClient = ApiClient;
            this.ModelClassNameToHardCodedFields = new Dictionary<eModelClassName, List<string>>();


            foreach (eModelClassName ModelClassName in Enum.GetValues(typeof(eModelClassName)))
            {
                ModelClassNameToHardCodedFields.Add(ModelClassName, new List<string>());
            }


            #region Add hard coded feilds

            ModelClassNameToHardCodedFields[eModelClassName.Account] =
                                new List<string>()
                                {
                                    "public Reference<Account> Parent { get; set; }",
                                    "public Reference<PriceList> PriceList { get; set; }",
                                    "public Reference<SpecialPriceList> SpecialPriceList { get; set; }",
                                    "public References<AccountCatalog> Catalogs { get; set; }",
                                    "public References<User> Users { get; set; }",
                                };


            ModelClassNameToHardCodedFields[eModelClassName.AccountCatalog] =
                                new List<string>()
                                {
                                    "public Reference<Account> Account { get; set; }",
                                    "public Reference<Catalog> Catalog { get; set; }",
                                };


            ModelClassNameToHardCodedFields[eModelClassName.AccountUser] =
                               new List<string>()
                                {
                                    "public Reference<Account> Account { get; set; }",
                                    "public Reference<User> User { get; set; }",
                                };



            ModelClassNameToHardCodedFields[eModelClassName.AccountInventory] =
                               new List<string>()
                                {
                                    "public Reference<Account> Account { get; set; }",
                                    "public Reference<Item> Item { get; set; }",
                                };

            ModelClassNameToHardCodedFields[eModelClassName.Activity] =
                                new List<string>()
                                {
                                    "public Reference<Account> Account { get; set; }",
                                    "public Reference<Contact> ContactPerson { get; set; }",
                                    "public Reference<User> Agent { get; set; }",
                                    "public Reference<User> Creator { get; set; }",
                                    "public References<Contact> ContactPersonList { get; set; }",
                                };

            ModelClassNameToHardCodedFields[eModelClassName.Item] =
                              new List<string>()
                                {
                                    "public References<Inventory> Inventory { get; set; }",
                                    "public Reference<Item> Parent { get; set; }",
                                };

            ModelClassNameToHardCodedFields[eModelClassName.Inventory] =
                               new List<string>()
                                {
                                    "public Reference<Item> Item { get; set; }",
                                };


            ModelClassNameToHardCodedFields[eModelClassName.Contact] =
                              new List<string>()
                                {
                                    "public Reference<Account> Account { get; set; }"
                                };


            ModelClassNameToHardCodedFields[eModelClassName.ItemPrice] =
                              new List<string>()
                                {
                                    "public Reference<PriceList> PriceList { get; set; }",
                                    "public Reference<Item> Item { get; set; }",
                                };

            ModelClassNameToHardCodedFields[eModelClassName.Transaction] =
                             new List<string>()
                                {
                                    "public Reference<Account> OriginAccount    { get; set; }",
                                    "public Reference<Account> Account  { get; set; }",
                                    "public Reference<Catalog> Catalog  { get; set; }",
                                    "public Reference<Contact> ContactPerson { get; set; }",
                                    "public Reference<User> Creator     { get; set; }",
                                    "public Reference<User> Agent       { get; set; }",

                                     "public References<TransactionLine> TransactionLines   { get; set; }",
                                };


            ModelClassNameToHardCodedFields[eModelClassName.TransactionLine] =
                                new List<string>()
                                {
                                    "public Reference<Transaction> Transaction { get; set; }",
                                    "public Reference<Item> Item { get; set; }",
                                };




            #endregion
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelNamespace"></param>
        /// <param name="outputDirectory"></param>
        /// <param name="generateCustomFields">eg, TSA fields</param>
        public void GenerateModelsCode(string modelNamespace, string outputDirectory, bool generateCustomFields, bool generateUDC = false)
        {
            #region validate input

            if (modelNamespace == null || modelNamespace.Trim().Length == 0)
            {
                throw new PepperiException("Invalid argument. The model namespace can not be empty.");
            }

            bool directoryExists = Directory.Exists(outputDirectory);
            if (directoryExists == false)
            {
                throw new PepperiException("The directory " + outputDirectory == null ? "null" : outputDirectory + "does not exist.");
            }

            #endregion

            #region Generate Code

            Dictionary<string, string> fileName_To_ClassCode = new Dictionary<string, string>();


            foreach (eModelClassName ClassName in Enum.GetValues(typeof(eModelClassName)))
            {
                IEnumerable<Field_MetaData> Fields_MetaData = GetFieldsMetadataByClassName(ClassName);
                List<string> HardCodedFields = ModelClassNameToHardCodedFields[ClassName];
                string ClassNameAsString = ClassName.ToString();
                string ClassCode = GenerateClassCode(modelNamespace, ClassNameAsString, Fields_MetaData, HardCodedFields, generateCustomFields);
                string FileName = ClassNameAsString + ".cs";
                fileName_To_ClassCode.Add(FileName, ClassCode);
            }

            #region User Defined Collections

            if (generateUDC)
            {
                var userDefinedCollectionsCode = GenerateCodeForUserDefinedCollections(modelNamespace);
                fileName_To_ClassCode.Add("UserDefinedCollections.cs", userDefinedCollectionsCode);
            }

            #endregion


            #endregion

            #region Save Files

            foreach (string fileName in fileName_To_ClassCode.Keys)
            {
                string classCode = fileName_To_ClassCode[fileName];
                SaveFile(outputDirectory, classCode, fileName);
            }

            #endregion            
        }

        #endregion

        #region private metohds

        private IEnumerable<Field_MetaData> GetFieldsMetadataByClassName(eModelClassName ClassName)
        {
            IEnumerable<Field_MetaData> result = null;
            switch (ClassName)
            {
                case eModelClassName.Account:
                    {
                        // Workaround for PriceListExternalID
                        result = CheckAddExternalField(ApiClient.AccountsMetaData.GetFields(), "PriceListExternalID");
                        break;
                    }

                case eModelClassName.AccountCatalog:
                    {
                        result = ApiClient.AccountCatalogsMetaData.GetFields();
                        break;
                    }

                case eModelClassName.AccountInventory:
                    {
                        result = ApiClient.AccountInventoryMetaData.GetFields();
                        break;
                    }

                case eModelClassName.AccountUser:
                    {
                        result = ApiClient.AccountUsersMetaData.GetFields();
                        break;
                    }

                case eModelClassName.Activity:
                    {
                        // Workaround for ContactPersonExternalID
                        result = CheckAddExternalField(ApiClient.ActivitiesMetaData.GetFields(), "ContactPersonExternalID");
                        break;
                    }

                case eModelClassName.Catalog:
                    {
                        result = ApiClient.CatalogsMetaData.GetFields();
                        break;
                    }

                case eModelClassName.Contact:
                    {
                        // Workaround for AccountExternalID
                        result = CheckAddExternalField(ApiClient.ContactsMetaData.GetFields(), "AccountExternalID");
                        break;
                    }
                case eModelClassName.Inventory:
                    {
                        // Workaround for ItemExternalID
                        result = CheckAddExternalField(ApiClient.InventoryMetaData.GetFields(), "ItemExternalID");
                        break;
                    }

                case eModelClassName.Item:
                    {
                        // Workaround for MainCategoryID
                        result = CheckAddExternalField(ApiClient.ItemsMetaData.GetFields(), "MainCategoryID");
                        break;
                    }

                case eModelClassName.ItemDimensions1:
                    {
                        result = ApiClient.ItemDimensions1_MetaData.GetFields();
                        break;
                    }

                case eModelClassName.ItemDimensions2:
                    {
                        result = ApiClient.ItemDimensions2_MetaData.GetFields();
                        break;
                    }

                case eModelClassName.PriceList:
                    {
                        result = ApiClient.PriceListsMetaData.GetFields();
                        break;
                    }

                case eModelClassName.ItemPrice:
                    {
                        // Workaround for ItemExternalID, PriceListExternalID
                        var additionalFields = new List<Field_MetaData>{
                            new Field_MetaData {
                                FieldID = "ItemExternalID",
                                Format = "String"
                            },
                            new Field_MetaData {
                                FieldID = "PriceListExternalID",
                                Format = "String"
                            }
                        };
                        result = CheckAddExternalFields(ApiClient.ItemPricesMetaData.GetFields(), additionalFields);
                        break;
                    }

                case eModelClassName.SpecialPriceList:
                    {
                        result = ApiClient.SpecialPriceListsMetaData.GetFields();
                        break;
                    }


                case eModelClassName.Transaction:
                    {
                        // Workaround for AccountExternalID
                        result = CheckAddExternalField(ApiClient.TransactionsMetaData.GetFields(), "AccountExternalID");
                        break;
                    }

                case eModelClassName.TransactionLine:
                    {
                        // Workaround for TransactionExternalID
                        result = CheckAddExternalField(ApiClient.TransactionLinesMetaData.GetFields(), "TransactionExternalID");
                        break;
                    }

                case eModelClassName.User:
                    {
                        result = ApiClient.UsersMetaData.GetFields();
                        break;
                    }

                /*
                The model class is not generated by the generator (Reason: the metadata is not accurate.)
                case eClassName.UserDefinedTable:
                {
                    result = ApiClient.UserDefinedTables.GetFieldsMetaData();
                    break;
                }
                */


                //case eModelClassName.Variant1:
                //{
                //    result = ApiClient.Variants1.GetFieldsMetaData();
                //    break;
                //}

                //case eModelClassName.Variant2:
                //{
                //    result = ApiClient.Variants2.GetFieldsMetaData();
                //    break;
                //}

                default:
                    {
                        throw new PepperiException("Failed to generate code. Reason: class name " + ClassName.ToString() + " is not supported.");
                    }
            }

            return result;

        }

        private IEnumerable<Field_MetaData> CheckAddExternalField(IEnumerable<Field_MetaData> fields, string fieldName, string returnFormat = "String")
        {
            return CheckAddExternalFields(fields, new List<Field_MetaData>{
                new Field_MetaData {
                    FieldID = fieldName,
                    Format = returnFormat
                }
            });
        }

        private IEnumerable<Field_MetaData> CheckAddExternalFields(IEnumerable<Field_MetaData> fields, IEnumerable<Field_MetaData> fieldsToAdd)
        {
            if (fieldsToAdd == null || fieldsToAdd.Count() == 0) return fields;

            var uniqueFields = fieldsToAdd.Where(fieldToAdd => fields.Where(field => field.FieldID == fieldToAdd.FieldID).Count() == 0);

            if (uniqueFields.Count() == 0) return fields;

            return fields.Concat(uniqueFields);
        }

        private string GenerateCodeForUserDefinedCollections(string modelNamespace)
        {
            var schemes = ApiClient.UserDefinedCollectionsMetaData.GetUserDefinedCollections();

            StringBuilder sbCode = new StringBuilder();
            sbCode.AppendLine("using System;");
            sbCode.AppendLine("using System.Collections.Generic;");
            sbCode.AppendLine("using System.Linq;");
            sbCode.AppendLine("using System.Text;");
            sbCode.AppendLine("using System.Threading.Tasks;");
            sbCode.AppendLine("using Pepperi.SDK.Model.Fixed;");
            sbCode.AppendLine("");
            sbCode.AppendLine("namespace " + modelNamespace);
            sbCode.AppendLine("{");

            var result = new Dictionary<string, string>();
            foreach (var scheme in schemes)
            {
                sbCode.AppendLine(GenerateCodeForUserDefinedCollection(scheme));
                sbCode.AppendLine("");
            }

            sbCode.AppendLine("}");
            return sbCode.ToString();
        }

        private string GenerateCodeForUserDefinedCollection(UDC_MetaData scheme)
        {
            var schemeName = scheme.Name;
            var className = $"UserDefinedCollection_{schemeName}";
            var fields = scheme.Fields;

            StringBuilder sbCode = new StringBuilder();

            sbCode.AppendLine("\tpublic class " + className);
            sbCode.AppendLine("\t{");

            foreach (var field in fields)
            {
                sbCode.AppendLine(GenerateCodeForUserDefinedField(field.Key, field.Value));
            }

            // Hidden Field
            GenerateCodeForUserDefinedField("Hidden", new UDC_Meta_Field()
            {
                Type = "Boolean",
                Mandatory = false
            });

            sbCode.AppendLine("\t}"); //close the class


            string classSourceCode = sbCode.ToString();
            return classSourceCode;
        }

        private string GenerateCodeForUserDefinedField(string fieldName, UDC_Meta_Field field)
        {
            var dataType = field.Type;
            if (dataType == null || dataType.Trim().Length == 0)
            {
                return ""; // incorrect field
            }
            string nullableValue = dataType.ToLower() != "string" && dataType.ToLower() != "array" ? "?" : string.Empty;
            var mappedType = MapPepperiFieldTypeToDotNet(dataType, field?.Items?.Type);
            if (mappedType == null)
            {
                return "";
            }
            return "\t\t public " + mappedType + nullableValue + " " + fieldName + " \t{get; set; }";  //eg, publc int? Ammount { get; set; };
        }

        private string MapPepperiFieldTypeToDotNet(string fieldType, string itemsType)
        {
            switch (fieldType)
            {
                case "String":
                    return "string";
                case "Integer":
                    return "Int64";
                case "Double":
                    return "Double";
                case "Bool":
                    return "Boolean";
                case "Array":
                    if (itemsType != "Array")
                    {
                        var mappedItemType = MapPepperiFieldTypeToDotNet(itemsType, null);
                        return $"IEnumerable<{mappedItemType}>";
                    }
                    return null;
                case "DateTime":
                    return "DateTime";
                default:
                    return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelNamespace"></param>
        /// <param name="clasName"></param>
        /// <param name="Fields_MetaData"></param>
        /// <param name="HardCodedFields"></param>
        /// <param name="generateCustomFields"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1. skip custom properties, according to configuration
        /// 2. skip properties without data type
        /// 3. all the properties are nullable
        ///             so it will not be sent on post body if user did not select it
        ///             the client wil not send the server null properties 
        ///             the server will not change properties that are not sent and replace properties that are sent
        ///             for data types that are not string we add ? to make it nullable 
        /// </remarks>
        private string GenerateClassCode(string modelNamespace, string clasName, IEnumerable<Field_MetaData> Fields_MetaData, List<string> HardCodedFields, bool generateCustomFields)
        {
            StringBuilder sbCode = new StringBuilder();
            StringBuilder sbReport = new StringBuilder();

            #region using statements

            sbCode.AppendLine("using System;");
            sbCode.AppendLine("using System.Collections.Generic;");
            sbCode.AppendLine("using System.Linq;");
            sbCode.AppendLine("using System.Text;");
            sbCode.AppendLine("using System.Threading.Tasks;");
            sbCode.AppendLine("using Pepperi.SDK.Model.Fixed;");
            sbCode.AppendLine("");

            #endregion

            #region namespace

            sbCode.AppendLine("namespace " + modelNamespace);
            sbCode.AppendLine("{");


            #region class

            sbCode.AppendLine("\tpublic class " + clasName);

            sbCode.AppendLine("\t{");

            #region metadaa fields

            foreach (var Field_MetaData in Fields_MetaData)
            {
                string DataType = Field_MetaData.Format;
                string Name = Field_MetaData.FieldID;
                long? UIType = null;
                if (Field_MetaData != null && Field_MetaData.UIType != null && Field_MetaData.UIType.ID != null)
                {
                    UIType = Field_MetaData.UIType.ID;
                }
                string Reference = null;
                if (Field_MetaData != null
                            && Field_MetaData.TypeSpecificFields != null
                            && Field_MetaData.TypeSpecificFields.ReferenceToResourceType != null
                    )
                {
                    Reference = Field_MetaData.TypeSpecificFields.ReferenceToResourceType;
                }



                if (generateCustomFields == false && Name.Contains("TSA"))
                {
                    //skip custom fields
                    continue;
                }

                if (DataType.Trim().Length == 0 || UIType == 45)    //UiType=45 is for 1 to many relations
                {
                    sbReport.AppendFormat("No data type for class {0} DataType={1} Name= {2} UIType={3} \r\n",
                        clasName,
                        DataType == null ? "null" : DataType,
                        Name == null ? "null" : Name,
                        UIType == null ? "null" : UIType.ToString()
                    );

                    //skip properties without data type:    eg, PriceLevel in account or SpecialPriceLevel in account
                    continue;
                }


                //if (Reference == "images")
                if (UIType == 20)   //Image
                {
                    sbCode.AppendLine("\t\t public Image " + Name + " \t{get; set; }");  //eg, publc Image Image2 { get; set; }
                }

                else
                {
                    //add? for the types that are not string 
                    string nullableValue = DataType.ToLower() != "string" ? "?" : string.Empty;
                    sbCode.AppendLine("\t\t public " + DataType + nullableValue + " " + Name + " \t{get; set; }");  //eg, publc int? Ammount { get; set; }
                }
            }

            #endregion


            #region hard coded fields (1:1 1:many)

            foreach (string hardCodedField in HardCodedFields)
            {
                sbCode.AppendLine("\t\t " + hardCodedField);
            }

            #endregion

            sbCode.AppendLine("\t}"); //close the class

            #endregion

            sbCode.AppendLine("}"); //close the namespace

            #endregion


            Console.WriteLine(sbReport.ToString());

            string classSourceCode = sbCode.ToString();
            return classSourceCode;


        }

        private void SaveFile(string outputDirectory, string fileText, string fileName)
        {
            string path = Path.Combine(outputDirectory, fileName);
            System.IO.File.WriteAllText(@path, fileText);            // WriteAllText creates a file, writes the specified string to the file, and then closes the file.    You do NOT need to call Flush() or Close().

        }

        #endregion

    }
}



/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
                    
+No data type for class Account DataType= Name= PriceLevel UIType=45
+No data type for class Account DataType= Name= SpecialPriceLevel UIType=45
             
reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________
?account has property called ClientOrganizationPortfolioItemStocks of type List<AccountItemStock>     uri:/PepperiAPInt.Data.svc/V1.0/AccountItemStock?where=AccountWrntyID=9967235
+account has property calld  Catalogs of type List<Catalog>                                           uri:/PepperiAPInt.Data.svc/V1.0/catalogs?InternalWhereClause=AccountWrntyID=9967235&InternalType=35
+account has property called ContactPersons of type List<contact>                                     uri:/PepperiAPInt.Data.svc/V1.0/contacts?where=AccountWrntyID=9967235                
+account has property called Users of type List<User>                                                 uri:/PepperiAPInt.Data.svc/V1.0/users?InternalWhereClause=AccountWrntyID=9967235&InternalType=35
 */

/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
            
?No data type for class Activity DataType= Name= ContactPersonList UIType=22
+No data type for class Activity DataType= Name= Account UIType=45
+No data type for class Activity DataType= Name= ContactPerson UIType=45 
+No data type for class Activity DataType= Name= Agent UIType=45
+No data type for class Activity DataType= Name= Creator UIType=45 user
             
 reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________

 +activity has property called ContactPersonList of type List<Contact>       uri: 

*/
/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
                    
reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________ 
*/


/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
                    
reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________
*/


/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
            
reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________       
*/
/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
                                       
No data type for class Item DataType= Name= Image  UIType=20			
No data type for class Item DataType= Name= Image2 UIType=20			
No data type for class Item DataType= Name= Image3 UIType=20			
No data type for class Item DataType= Name= Image4 UIType=20			
No data type for class Item DataType= Name= Image5 UIType=20			
No data type for class Item DataType= Name= Image6 UIType=20			

reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________
                   
?item has property called PriceListPortfolioItems  of type List<PriceLevelItem>                     uri:    /PepperiAPInt.Data.svc/V1.0/PriceLevelItem?where=ItemWrntyID=27235906
*/

/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
             
reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________ 
*/

/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
                    
No data type for class Transaction DataType= Name= Signature UIType=25
+No data type for class Transaction DataType= Name= OriginAccount UIType=45
+No data type for class Transaction DataType= Name= Account UIType=45
+No data type for class Transaction DataType= Name= Catalog UIType=45
+No data type for class Transaction DataType= Name= Employee UIType=45 
+No data type for class Transaction DataType= Name= Agent UIType=45 
             
             
reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________
                   
+transaction has property called TransactionLines of type List<TransactionLine>       uri: "/PepperiAPInt.Data.svc/V1.0/TransactionLine?where=TransactionWrntyID=13627540"
*/
/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
                    
No data type for class TransactionLine DataType= Name= Image UIType=20
+No data type for class TransactionLine DataType= Name= Transaction UIType=45
+No data type for class TransactionLine DataType= Name= Item UIType=45
             
reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________       
*/

/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
                    
reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________
*/



/*
The model class is not generated by the generator (Reason: the metadata is not accurate.)
ClassNameToHardCodedFields.Add(
                    eClassName.UserDefinedTable,
                    new List<string>()
                    {
                    });
*/

/*
reference to object 1:1, 1:many (look at UIType=45 in the generation report)
____________________________________________________________________________
           
reference to collection 1:1 1: many (look in get by id: URI with array of data)
_______________________________________________________________________________             
*/
