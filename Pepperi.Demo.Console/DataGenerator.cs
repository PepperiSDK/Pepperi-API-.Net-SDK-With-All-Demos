using Pepperi.SDK;
using Pepperi.SDK.Contracts;
using Pepperi.SDK.Exceptions;
using Pepperi.SDK.Model;
using Pepperi.SDK.Model.Fixed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.Demo.Console
{
    /// <summary>
    /// used to Generate data in memeory and upload it to Pepperi as Bulk or By single elements using Pepperi API
    /// </summary>
    public class DataGenerator
    {
        #region properties

        private ILogger Logger { get; set; }
        private ApiClient ApiClient { get; set; }
        private PepperiDbContext PepperiDbContext { get; set; }     //We store in memory the data uploaded to Pepperi. We use it to link to entities. Note: if data was uploaded using bulk, the entities do not have internal id but only the external id. Otherwise, entities have external and iternal id.
        private On_DataGenerator_Progress On_DataGenerator_Progress { get; set; }
        private Random Random { get; set; }

        #endregion

        #region Constructor

        public DataGenerator(ILogger Logger, ApiClient ApiClient, On_DataGenerator_Progress On_DataGenerator_Progress = null)
        {
            this.Logger = Logger;
            this.ApiClient = ApiClient;
            this.PepperiDbContext = new PepperiDbContext();
            this.On_DataGenerator_Progress = On_DataGenerator_Progress;
            this.Random = new Random(DateTime.Now.Millisecond);
        }

        #endregion

        #region Public methods

        public PepperiDbContext GenerateAndUploadData(DataQuatities DataQuatities)
        {
            GetBulkJobInfoResponse GetBulkJobInfoResponse = null;

            #region Item

            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("generating items data...."); }
            List<Item> Items = GenerateItems(DataQuatities.generate_X_Item);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading items......"); }
            GetBulkJobInfoResponse = UploadAsBulk(Items);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished items upload successfully!"); }

            #endregion

            #region PriceList

            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("generating price list data...."); }
            List<PriceList> PriceLists = GeneratePriceLists(DataQuatities.generate_X_PriceList);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading price list......"); }
            GetBulkJobInfoResponse = UploadAsBulk(PriceLists);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished price list upload successfully!"); }

            #endregion

            #region ItemPrice

            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("generating item prices data...."); }
            List<ItemPrice> ItemPrices = GenerateItemPrices();
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading item prices ...."); }
            GetBulkJobInfoResponse = UploadAsBulk(ItemPrices);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished item prices upload successfully!"); }

            #endregion

            #region Inventory

            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("generating inventory data...."); }
            List<Inventory> Inventory = GenerateInventory();
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading inventory ...."); }
            GetBulkJobInfoResponse = UploadAsBulk(Inventory);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished inventory upload successfully!"); }

            #endregion

            #region Account : using price List 

            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("generating accounts data...."); }
            List<Account> Accounts = GenerateAccounts(DataQuatities.generate_X_accounts);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading accounts ...."); }
            GetBulkJobInfoResponse = UploadAsBulk(Accounts);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished accounts upload successfully!"); }

            #endregion

            #region Contact: using account

            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("generating contacts data...."); }
            List<Contact> Contacts = GenerateContacts(DataQuatities.generate_X_contactsPerAccount);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading contacts ...."); }
            GetBulkJobInfoResponse = UploadAsBulk(Contacts);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished contacts upload successfully!"); }

            #endregion

            #region Transaction: using account

            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("generating transactions data...."); }
            List<Transaction> Transactions = GenerateTransactions(DataQuatities.generate_X_transactionsPerAccount);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading transactions ...."); }
            GetBulkJobInfoResponse = UploadAsBulk(Transactions);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished transactions upload successfully!"); }

            #endregion

            #region TransactionLine: using Transaction

            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("generating transaction lines data...."); }
            List<TransactionLine> TransactionLines = GenerateTransactionLines(DataQuatities.generate_X_transactionLinesPerTransaction);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading transaction lines ...."); }
            GetBulkJobInfoResponse = UploadAsBulk(TransactionLines);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished transaction lines upload successfully!"); }

            #endregion

            #region Activities: using account

            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("generating activities data...."); }
            List<Activity> Activities = GenerateActivities(DataQuatities.generate_X_activitierPerAccount);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading activities ...."); }
            GetBulkJobInfoResponse = UploadAsBulk(Activities);
            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished activities upload successfully!"); }

            #endregion

            #region Item Images

            //if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading 5 images ...."); }
            //UploadImagesForItems(5);

            #endregion


            if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("finished uploading all data successfully!"); }

            return PepperiDbContext;

        }

        #endregion

        #region Items: Standalone

        public List<Item> GenerateItems(int numberOfElements, eDataGeneratorLinkMethod DataGeneratorLinkMethod = eDataGeneratorLinkMethod.use_ExternalID_Property, eRandomMethod RandomMethod = eRandomMethod.useIndex)
        {
            var models = new List<Item>();
            for (int i = 0; i < numberOfElements; i++)
            {
                var model = Generate(new Item(), DataGeneratorLinkMethod, i, numberOfElements, RandomMethod);
                models.Add(model);
            }

            return models;
        }

        public GetBulkJobInfoResponse UploadAsBulk(IEnumerable<Item> models)
        {
            List<string> fieldsToUpload = new List<string>() { "ExternalID", "MainCategoryID", "UPC", "Name", "LongDescription", "Price", "Prop1", "Prop2", "Prop3" };
            var BulkUploadResponse = ApiClient.Items.BulkUpload(models, eOverwriteMethod.full, eBulkUploadMethod.Zip, fieldsToUpload, true);
            GetBulkJobInfoResponse GetBulkJobInfoResponse = ApiClient.Items.WaitForBulkJobToComplete(BulkUploadResponse.JobID);

            //note: we do not validate result here 
            if (GetBulkJobInfoResponse.Status != "Ok")
            {
                throw new Exception("Bulk upload failed. Status = " + GetBulkJobInfoResponse.Status == null ? "null" : GetBulkJobInfoResponse.Status + " Error = " + GetBulkJobInfoResponse.Error == null ? "null " : GetBulkJobInfoResponse.Error);
            }

            PepperiDbContext.Items = models.ToList();           //add to the context
            return GetBulkJobInfoResponse;
        }

        public void UploadWithoutBulk(IEnumerable<Item> models)
        {
            foreach (var model in models)
            {
                var savedModel = ApiClient.Items.Upsert(model);
                PepperiDbContext.Items.Add(savedModel);         //add to the context
            }
        }

        private Item Generate(Item model, eDataGeneratorLinkMethod DataGeneratorLinkMethod, int index, int numberOfElements, eRandomMethod RandomMethod)
        {
            string randomString = (RandomMethod == eRandomMethod.useIndex) ? index.ToString() : Guid.NewGuid().ToString();


            model.ExternalID = "ExternalID_" + randomString;
            model.MainCategory = "Category_" + calc_Block_index(index, numberOfElements, numberOfElements / 10).ToString();             //Category will be created.     Change the Category every 0.1 of the items


            model.LongDescription = "LongDescription_" + randomString;
            model.Name = "Item_Name_" + randomString;
            model.Price = generateRandomDecimal(100, 400, 2);
            model.Prop1 = "Prop1_" + calc_Block_index(index, numberOfElements, numberOfElements / 100).ToString();                          //Change the Prop1 evey 0.01 of the items
            model.Prop2 = "Prop2_" + calc_Block_index(index, numberOfElements, numberOfElements / 100).ToString();                          //Change the Prop2 evey 0.01 of the items
            model.Prop3 = "Prop3_" + calc_Block_index(index, numberOfElements, numberOfElements / 100).ToString();                          //Change the Prop3 evey 0.01 of the items
            model.UPC = "UPC_" + randomString;

            return model;
        }

        #endregion

        #region PriceList: Standalone

        public List<PriceList> GeneratePriceLists(int numberOfElements, eDataGeneratorLinkMethod DataGeneratorLinkMethod = eDataGeneratorLinkMethod.use_ExternalID_Property, eRandomMethod RandomMethod = eRandomMethod.useIndex)
        {
            var models = new List<PriceList>();
            for (int i = 0; i < numberOfElements; i++)
            {
                var model = Generate(new PriceList(), DataGeneratorLinkMethod, i, numberOfElements, RandomMethod);
                models.Add(model);
            }

            return models;
        }

        public GetBulkJobInfoResponse UploadAsBulk(IEnumerable<PriceList> models)
        {
            List<string> fieldsToUpload = new List<string>() { "ExternalID", "Hidden", "Description", "CurrencySymbol" };
            var BulkUploadResponse = ApiClient.PriceLists.BulkUpload(models, eOverwriteMethod.full, eBulkUploadMethod.Zip, fieldsToUpload, true);
            GetBulkJobInfoResponse GetBulkJobInfoResponse = ApiClient.PriceLists.WaitForBulkJobToComplete(BulkUploadResponse.JobID);

            //note: we do not validate result here 
            if (GetBulkJobInfoResponse.Status != "Ok")
            {
                throw new Exception("Bulk upload failed. Status = " + GetBulkJobInfoResponse.Status == null ? "null" : GetBulkJobInfoResponse.Status + " Error = " + GetBulkJobInfoResponse.Error == null ? "null " : GetBulkJobInfoResponse.Error);
            }

            PepperiDbContext.PriceLists = models.ToList();           //add to the context
            return GetBulkJobInfoResponse;
        }

        public void UploadWithoutBulk(IEnumerable<PriceList> models)
        {
            foreach (var model in models)
            {
                var savedModel = ApiClient.PriceLists.Upsert(model);
                PepperiDbContext.PriceLists.Add(savedModel);         //add to the context
            }
        }

        private PriceList Generate(PriceList model, eDataGeneratorLinkMethod DataGeneratorLinkMethod, int index, int numberOfElements, eRandomMethod RandomMethod)
        {
            string randomString = (RandomMethod == eRandomMethod.useIndex) ? index.ToString() : Guid.NewGuid().ToString();

            model.ExternalID = "ExternalID_" + randomString;
            model.Hidden = false;
            model.Description = "PriceList_Description_" + randomString;
            model.CurrencySymbol = "$";

            return model;
        }

        #endregion

        #region ItemPrice: for every Item, for every priceList,              Generate ItemPrice

        public List<ItemPrice> GenerateItemPrices(eDataGeneratorLinkMethod DataGeneratorLinkMethod = eDataGeneratorLinkMethod.use_ExternalID_Property, eRandomMethod RandomMethod = eRandomMethod.useIndex)
        {
            int numberOfElements = PepperiDbContext.Items.Count() * PepperiDbContext.PriceLists.Count();
            int i = 0;

            var models = new List<ItemPrice>();

            foreach (var Item in PepperiDbContext.Items)
            {
                foreach (var PriceList in PepperiDbContext.PriceLists)
                {
                    var model = Generate(new ItemPrice(), Item, PriceList, DataGeneratorLinkMethod, i, numberOfElements, RandomMethod);
                    models.Add(model);
                    i++;
                }
            }

            return models;
        }

        public GetBulkJobInfoResponse UploadAsBulk(IEnumerable<ItemPrice> models)
        {
            List<string> fieldsToUpload = new List<string>() { "PriceListExternalID", "ItemExternalID", "Hidden", "Price" };
            var BulkUploadResponse = ApiClient.ItemPrices.BulkUpload(models, eOverwriteMethod.full, eBulkUploadMethod.Zip, fieldsToUpload, true);
            GetBulkJobInfoResponse GetBulkJobInfoResponse = ApiClient.ItemPrices.WaitForBulkJobToComplete(BulkUploadResponse.JobID);

            //note: we do not validate result here 
            if (GetBulkJobInfoResponse.Status != "Ok")
            {
                throw new Exception("Bulk upload failed. Status = " + GetBulkJobInfoResponse.Status == null ? "null" : GetBulkJobInfoResponse.Status + " Error = " + GetBulkJobInfoResponse.Error == null ? "null " : GetBulkJobInfoResponse.Error);
            }

            PepperiDbContext.ItemPrices = models.ToList();           //add to the context

            return GetBulkJobInfoResponse;
        }

        public void UploadWithoutBulk(IEnumerable<ItemPrice> models)
        {
            foreach (var model in models)
            {
                var savedModel = ApiClient.ItemPrices.Upsert(model);
                PepperiDbContext.ItemPrices.Add(savedModel);         //add to the context
            }
        }

        private ItemPrice Generate(ItemPrice model, Item Item, PriceList PriceList, eDataGeneratorLinkMethod DataGeneratorLinkMethod, int index, int numberOfElements, eRandomMethod RandomMethod)
        {
            string randomString = (RandomMethod == eRandomMethod.useIndex) ? index.ToString() : Guid.NewGuid().ToString();


            //Link to price list
            switch (DataGeneratorLinkMethod)
            {
                //case eDataGeneratorLinkMethod.use_ExternalID_Property:
                //    model.PriceListExternalID = PriceList.ExternalID;
                //    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.PriceList = new Reference<PriceList>();
                    model.PriceList.Data = new PriceList();
                    model.PriceList.Data.ExternalID = PriceList.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.PriceList = new Reference<PriceList>();
                    model.PriceList.Data = new PriceList();
                    model.PriceList.Data.InternalID = PriceList.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }


            //Link to item
            switch (DataGeneratorLinkMethod)
            {
                //case eDataGeneratorLinkMethod.use_ExternalID_Property:
                //    model.ItemExternalID = Item.ExternalID;
                //    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.Item = new Reference<Item>();
                    model.Item.Data = new Item();
                    model.Item.Data.ExternalID = Item.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.Item = new Reference<Item>();
                    model.Item.Data = new Item();
                    model.Item.Data.InternalID = Item.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }

            model.Hidden = false;
            model.Price = generateRandomDecimal(10, 500, 2);


            return model;
        }


        #endregion

        #region Inventory: for every item, generate Inventory

        public List<Inventory> GenerateInventory(eDataGeneratorLinkMethod DataGeneratorLinkMethod = eDataGeneratorLinkMethod.use_ExternalID_Property, eRandomMethod RandomMethod = eRandomMethod.useIndex)
        {
            int numberOfElements = PepperiDbContext.Items.Count();
            int i = 0;

            var models = new List<Inventory>();

            foreach (var Item in PepperiDbContext.Items)
            {
                var model = Generate(new Inventory(), Item, DataGeneratorLinkMethod, i, numberOfElements, RandomMethod);
                models.Add(model);
                i++;
            }

            return models;
        }

        public GetBulkJobInfoResponse UploadAsBulk(IEnumerable<Inventory> models)
        {
            List<string> fieldsToUpload = new List<string>() { "ItemExternalID", "InStockQuantity" };
            var BulkUploadResponse = ApiClient.Inventory.BulkUpload(models, eOverwriteMethod.full, eBulkUploadMethod.Zip, fieldsToUpload, true);
            GetBulkJobInfoResponse GetBulkJobInfoResponse = ApiClient.Inventory.WaitForBulkJobToComplete(BulkUploadResponse.JobID);

            //note: we do not validate result here 
            if (GetBulkJobInfoResponse.Status != "Ok")
            {
                throw new Exception("Bulk upload failed. Status = " + GetBulkJobInfoResponse.Status == null ? "null" : GetBulkJobInfoResponse.Status + " Error = " + GetBulkJobInfoResponse.Error == null ? "null " : GetBulkJobInfoResponse.Error);
            }

            PepperiDbContext.Inventory = models.ToList();           //add to the context

            return GetBulkJobInfoResponse;
        }

        public void UploadWithoutBulk(IEnumerable<Inventory> models)
        {
            foreach (var model in models)
            {
                var savedModel = ApiClient.Inventory.Upsert(model);
                PepperiDbContext.Inventory.Add(savedModel);         //add to the context
            }
        }

        private Inventory Generate(Inventory model, Item Item, eDataGeneratorLinkMethod DataGeneratorLinkMethod, int index, int numberOfElements, eRandomMethod RandomMethod)
        {
            string randomString = (RandomMethod == eRandomMethod.useIndex) ? index.ToString() : Guid.NewGuid().ToString();

            //Link to item
            switch (DataGeneratorLinkMethod)
            {
                //case eDataGeneratorLinkMethod.use_ExternalID_Property:
                //    model.ItemExternalID = Item.ExternalID;
                //    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.Item = new Reference<Item>();
                    model.Item.Data = new Item();
                    model.Item.Data.ExternalID = Item.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.Item = new Reference<Item>();
                    model.Item.Data = new Item();
                    model.Item.Data.InternalID = Item.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }


            model.InStockQuantity = generateRandomDouble(1, 2000, 0);

            return model;
        }

        #endregion

        #region Account: Standalone

        public List<Account> GenerateAccounts(int numberOfElements, eDataGeneratorLinkMethod DataGeneratorLinkMethod = eDataGeneratorLinkMethod.use_ExternalID_Property, eRandomMethod RandomMethod = eRandomMethod.useIndex)
        {
            var models = new List<Account>();
            for (int i = 0; i < numberOfElements; i++)
            {
                var model = Generate(new Account(), DataGeneratorLinkMethod, i, numberOfElements, RandomMethod);
                models.Add(model);
            }

            return models;
        }

        public GetBulkJobInfoResponse UploadAsBulk(IEnumerable<Account> models)
        {
            List<string> fieldsToUpload = new List<string>() { "ExternalID", "PriceListExternalID", "City", "Debts30", "Debts60", "Debts90", "DebtsAbove90", "Email", "Mobile", "Name", "Note", "Phone", "Prop1", "Prop2", "Prop3", "Prop4", "Prop5", "Street" };
            var BulkUploadResponse = ApiClient.Accounts.BulkUpload(models, eOverwriteMethod.full, eBulkUploadMethod.Zip, fieldsToUpload, true);
            GetBulkJobInfoResponse GetBulkJobInfoResponse = ApiClient.Accounts.WaitForBulkJobToComplete(BulkUploadResponse.JobID);

            //note: we do not validate result here 
            if (GetBulkJobInfoResponse.Status != "Ok")
            {
                throw new Exception("Bulk upload failed. Status = " + GetBulkJobInfoResponse.Status == null ? "null" : GetBulkJobInfoResponse.Status + " Error = " + GetBulkJobInfoResponse.Error == null ? "null " : GetBulkJobInfoResponse.Error);
            }

            PepperiDbContext.Accounts = models.ToList();           //add to the context

            return GetBulkJobInfoResponse;
        }

        public void UploadWithoutBulk(IEnumerable<Account> models)
        {
            foreach (var model in models)
            {
                var savedModel = ApiClient.Accounts.Upsert(model);
                PepperiDbContext.Accounts.Add(savedModel);         //add to the context
            }
        }

        private Account Generate(Account model, eDataGeneratorLinkMethod DataGeneratorLinkMethod, int index, int numberOfElements, eRandomMethod RandomMethod)
        {
            string randomString = (RandomMethod == eRandomMethod.useIndex) ? index.ToString() : Guid.NewGuid().ToString();

            model.ExternalID = "ExternalID_" + randomString;

            //Link to PriceList
            int BlockIndex = calc_Block_index(index, numberOfElements, numberOfElements / 10);  //split acconuts to 10 blocks, each gets differet PriceList
            int PriceListIndex = (PepperiDbContext.PriceLists.Count() >= BlockIndex ? BlockIndex : BlockIndex % PepperiDbContext.PriceLists.Count());
            PriceList PriceList = PepperiDbContext.PriceLists[PriceListIndex];

            //SpecialPriceList: do not link
            //Catalogs:         do not link
            //ContactPersons:   do not link
            //Users:            do not link

            switch (DataGeneratorLinkMethod)
            {
                //case eDataGeneratorLinkMethod.use_ExternalID_Property:
                //    model.PriceListExternalID = PriceList.ExternalID;
                //    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.PriceList = new Reference<PriceList>();
                    model.PriceList.Data = new PriceList();
                    model.PriceList.Data.ExternalID = PriceList.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.PriceList = new Reference<PriceList>();
                    model.PriceList.Data = new PriceList();
                    model.PriceList.Data.InternalID = PriceList.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }


            model.City = "City_" + randomString;
            model.Debts30 = generateRandomDecimal(1, 25, 2);
            model.Debts60 = generateRandomDecimal(33, 57, 2);
            model.Debts90 = generateRandomDecimal(65, 85, 2);
            model.DebtsAbove90 = generateRandomDecimal(100, 200, 2);
            model.Email = "Email_" + randomString;
            model.Mobile = "Mobile_" + randomString;
            model.Name = "Account_Name_" + randomString;
            model.Note = "Note_" + randomString;
            model.Phone = "Phone_" + randomString;
            model.Prop1 = "Prop1_" + calc_Block_index(index, numberOfElements, numberOfElements / 100).ToString();                          //Change the Prop1 evey 0.01 of the accounts
            model.Prop2 = "Prop2_" + calc_Block_index(index, numberOfElements, numberOfElements / 100).ToString();                          //Change the Prop2 evey 0.01 of the accounts
            model.Prop3 = "Prop3_" + calc_Block_index(index, numberOfElements, numberOfElements / 100).ToString();                          //Change the Prop3 evey 0.01 of the accounts
            model.Prop4 = "Prop4_" + calc_Block_index(index, numberOfElements, numberOfElements / 100).ToString();                          //Change the Prop3 evey 0.01 of the accounts
            model.Prop5 = "Prop5_" + calc_Block_index(index, numberOfElements, numberOfElements / 100).ToString();                          //Change the Prop3 evey 0.01 of the accounts
            model.Street = "Street_" + randomString;

            return model;
        }

        #endregion

        #region Contact: for every account generate X contacts

        public List<Contact> GenerateContacts(int numberOfContactPerAccount, eDataGeneratorLinkMethod DataGeneratorLinkMethod = eDataGeneratorLinkMethod.use_ExternalID_Property, eRandomMethod RandomMethod = eRandomMethod.useIndex)
        {
            int numberOfElements = PepperiDbContext.Accounts.Count() * numberOfContactPerAccount;

            var models = new List<Contact>();

            int i = 0;
            for (int accountIndex = 0; accountIndex < PepperiDbContext.Accounts.Count(); accountIndex++)
            {
                Account Account = PepperiDbContext.Accounts.ElementAt(accountIndex);

                for (int contactOfAccountIndex = 0; contactOfAccountIndex < numberOfContactPerAccount; contactOfAccountIndex++)
                {
                    var model = Generate(new Contact(), Account, DataGeneratorLinkMethod, i, numberOfElements, RandomMethod);
                    models.Add(model);
                    i++;
                }
            }

            return models;
        }

        public GetBulkJobInfoResponse UploadAsBulk(IEnumerable<Contact> models)
        {
            List<string> fieldsToUpload = new List<string>() { "ExternalID", "Email", "FirstName", "LastName", "Hidden", "AccountExternalID" };
            var BulkUploadResponse = ApiClient.Contacts.BulkUpload(models, eOverwriteMethod.full, eBulkUploadMethod.Zip, fieldsToUpload, true);
            GetBulkJobInfoResponse GetBulkJobInfoResponse = ApiClient.Contacts.WaitForBulkJobToComplete(BulkUploadResponse.JobID);

            //note: we do not validate result here 
            if (GetBulkJobInfoResponse.Status != "Ok")
            {
                throw new Exception("Bulk upload failed. Status = " + GetBulkJobInfoResponse.Status == null ? "null" : GetBulkJobInfoResponse.Status + " Error = " + GetBulkJobInfoResponse.Error == null ? "null " : GetBulkJobInfoResponse.Error);
            }

            PepperiDbContext.Contacts = models.ToList();           //add to the context
            return GetBulkJobInfoResponse;
        }

        public void UploadWithoutBulk(IEnumerable<Contact> models)
        {
            foreach (var model in models)
            {
                var savedModel = ApiClient.Contacts.Upsert(model);
                PepperiDbContext.Contacts.Add(savedModel);         //add to the context
            }
        }

        private Contact Generate(Contact model, Account Account, eDataGeneratorLinkMethod DataGeneratorLinkMethod, int index, int numberOfElements, eRandomMethod RandomMethod)
        {
            string randomString = (RandomMethod == eRandomMethod.useIndex) ? index.ToString() : Guid.NewGuid().ToString();

            model.ExternalID = "ExternalID_" + randomString;
            model.Email = model.ExternalID + "@gmail.com";
            model.FirstName = "Contact_FirstName" + randomString;
            model.LastName = "Contact_LastName" + randomString;
            model.Hidden = false;

            //Link to Account
            switch (DataGeneratorLinkMethod)
            {
                //case eDataGeneratorLinkMethod.use_ExternalID_Property:
                //    model.AccountExternalID = Account.ExternalID;
                //    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.Account = new Reference<Account>();
                    model.Account.Data = new Account();
                    model.Account.Data.ExternalID = Account.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.Account = new Reference<Account>();
                    model.Account.Data = new Account();
                    model.Account.Data.InternalID = Account.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }


            return model;

        }

        #endregion

        #region Transaction (SubType): for every account generate X transactions

        public List<Transaction> GenerateTransactions(int numberOfTransactionsPerAccount, eDataGeneratorLinkMethod DataGeneratorLinkMethod = eDataGeneratorLinkMethod.use_ExternalID_Property, eRandomMethod RandomMethod = eRandomMethod.useIndex)
        {
            int numberOfElements = PepperiDbContext.Accounts.Count() * numberOfTransactionsPerAccount;

            var models = new List<Transaction>();

            int i = 0;
            for (int accountIndex = 0; accountIndex < PepperiDbContext.Accounts.Count(); accountIndex++)
            {
                Account Account = PepperiDbContext.Accounts.ElementAt(accountIndex);

                for (int transactionOfAccountIndex = 0; transactionOfAccountIndex < numberOfTransactionsPerAccount; transactionOfAccountIndex++)
                {
                    var model = Generate(new Transaction(), Account, transactionOfAccountIndex, DataGeneratorLinkMethod, i, numberOfElements, RandomMethod);
                    models.Add(model);
                    i++;
                }
            }

            return models;
        }

        public GetBulkJobInfoResponse UploadAsBulk(IEnumerable<Transaction> models)
        {
            //for Transaction, the url of the BulkUpload must include the SUBTypeID of transaction
            //Pick Random Transaction sub type ID from the concrete types
            TypeMetadata TypeMetadata = ApiClient.Transactions.GetSubTypesMetadata().FirstOrDefault();
            if (TypeMetadata == null || TypeMetadata.SubTypeID == null)
            {
                throw new PepperiException("No subtypes for transction.");
            }


            List<string> fieldsToUpload = new List<string>() { "ExternalID", "AccountExternalID", "ActionDateTime", "BillToCity", "BillToCountry", "BillToFax", "BillToName", "BillToPhone", "BillToState", "BillToStreet", "BillToZipCode", "Remark", "ShipToCity", "ShipToCountry", "ShipToFax", "ShipToName", "ShipToPhone", "ShipToState", "ShipToStreet", "ShipToZipCode", "Status", "TotalItemsCount" };
            var BulkUploadResponse = ApiClient.Transactions.BulkUpload(models, eOverwriteMethod.full, eBulkUploadMethod.Zip, fieldsToUpload, true, TypeMetadata.SubTypeID);
            GetBulkJobInfoResponse GetBulkJobInfoResponse = ApiClient.Transactions.WaitForBulkJobToComplete(BulkUploadResponse.JobID);

            //note: we do not validate result here 
            if (GetBulkJobInfoResponse.Status != "Ok")
            {
                throw new Exception("Bulk upload failed. Status = " + GetBulkJobInfoResponse.Status == null ? "null" : GetBulkJobInfoResponse.Status + " Error = " + GetBulkJobInfoResponse.Error == null ? "null " : GetBulkJobInfoResponse.Error);
            }

            PepperiDbContext.Transactions = models.ToList();           //add to the context

            return GetBulkJobInfoResponse;
        }

        public void UploadWithoutBulk(IEnumerable<Transaction> models)
        {
            foreach (var model in models)
            {
                var savedModel = ApiClient.Transactions.Upsert(model);
                PepperiDbContext.Transactions.Add(savedModel);         //add to the context
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Account"></param>
        /// <param name="DataGeneratorLinkMethod"></param>
        /// <param name="index"></param>
        /// <param name="numberOfElements"></param>
        /// <param name="RandomMethod"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1.  Transaction is "abstract" type
        ///     The concrete type is identified by ActivityTypeID
        ///     For Bulk or CSV upload, the ActivityTypeID is sent on the url   
        ///     For single Upsert,      the ActivityTypeID is set on the object
        ///     The values of the ActivityTypeID are taken from the SubTypeMetadata action in the base endpoint
        ///2. This method sets the value of ActivityTypeID only when data will not be uploaded via bult
        /// </remarks>
        private Transaction Generate(Transaction model, Account Account, int transactionOfAccountIndex, eDataGeneratorLinkMethod DataGeneratorLinkMethod, int index, int numberOfElements, eRandomMethod RandomMethod)
        {
            string randomString = (RandomMethod == eRandomMethod.useIndex) ? index.ToString() : Guid.NewGuid().ToString();

            model.ExternalID = "ExternalID_" + randomString;

            //sets the type of the transaction
            //This is relevant only whe the data is generated for non bulk upload
            //For bulk upload, the activity type id is sent to API on the url as resource identifier    (baseEndoint gets it as  subTypeID)
            if (DataGeneratorLinkMethod != eDataGeneratorLinkMethod.use_ExternalID_Property)
            {
                model.ActivityTypeID = 137743;
            }
            else
            {
                //on bulk uplaod the value comes from url
            }

            //Link to Account
            switch (DataGeneratorLinkMethod)
            {
                //case eDataGeneratorLinkMethod.use_ExternalID_Property:
                //    model.AccountExternalID = Account.ExternalID;
                //    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.Account = new Reference<Account>();
                    model.Account.Data = new Account();
                    model.Account.Data.ExternalID = Account.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.Account = new Reference<Account>();
                    model.Account.Data = new Account();
                    model.Account.Data.InternalID = Account.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }

            //LinkToAgent: try without
            //LinkToCategory: Try without (it should use the default category created with the account).

            model.ActionDateTime = generateUTCDateTime(transactionOfAccountIndex / (-2), (-1) * transactionOfAccountIndex);    //change day evey 2 transactions of te account, seconds are sequence  


            model.BillToCity = "BillToCity_" + randomString;
            model.BillToCountry = "BillToCountry_" + randomString;
            model.BillToFax = "BillToFax_" + randomString;
            model.BillToName = "BillToName_" + randomString;
            model.BillToPhone = "BillToPhone_" + randomString;
            model.BillToState = "BillToState_" + randomString;
            model.BillToStreet = "BillToStreet_" + randomString;
            model.BillToZipCode = "BillToZipCode_" + randomString;
            //model.GrandTotal            = generateRandomDouble(100, 300, 2);
            model.Remark = "Remark_" + randomString;
            model.ShipToCity = "ShipToCity_" + randomString;
            model.ShipToCountry = "ShipToCountry_" + randomString;
            model.ShipToFax = "ShipToFax_" + randomString;
            model.ShipToName = "ShipToName_" + randomString;
            model.ShipToPhone = "ShipToPhone_" + randomString;
            model.ShipToState = "ShipToState_" + randomString;
            model.ShipToStreet = "ShipToStreet_" + randomString;
            model.ShipToZipCode = "ShipToZipCode_" + randomString;
            model.Status = (int)eStatus.InCreation;
            //model.TotalItemsCount = generateRandomDouble(100, 300, 2);

            return model;
        }

        #endregion

        #region TransactionLine (SubType): for every transaction, generate X transactionLines (each transaction line linked to random item)

        public List<TransactionLine> GenerateTransactionLines(short numberOfTransactionLinesPerTransaction, eDataGeneratorLinkMethod DataGeneratorLinkMethod = eDataGeneratorLinkMethod.use_ExternalID_Property, eRandomMethod RandomMethod = eRandomMethod.useIndex)
        {
            int numberOfElements = PepperiDbContext.Transactions.Count() * numberOfTransactionLinesPerTransaction;

            var models = new List<TransactionLine>();

            int i = 0;
            for (int transactionIndex = 0; transactionIndex < PepperiDbContext.Transactions.Count(); transactionIndex++)
            {
                Transaction Transaction = PepperiDbContext.Transactions.ElementAt(transactionIndex);

                for (short TransactionLineOfTransactionIndex = 1; TransactionLineOfTransactionIndex <= numberOfTransactionLinesPerTransaction; TransactionLineOfTransactionIndex++)
                {
                    var model = Generate(new TransactionLine(), Transaction, TransactionLineOfTransactionIndex, DataGeneratorLinkMethod, i, numberOfElements, RandomMethod);
                    models.Add(model);
                    i++;
                }
            }

            return models;
        }

        public GetBulkJobInfoResponse UploadAsBulk(IEnumerable<TransactionLine> models)
        {
            //for TransactionLines, the url of the BulkUpload must include the SUBTypeID of transaction
            //Pick Random Transaction sub type ID from the concrete types
            TypeMetadata TypeMetadata = ApiClient.Transactions.GetSubTypesMetadata().FirstOrDefault();
            if (TypeMetadata == null || TypeMetadata.SubTypeID == null)
            {
                throw new PepperiException("No subtypes for transction.");
            }


            List<string> fieldsToUpload = new List<string>() { "TransactionExternalID", "ItemExternalID", "LineNumber", "UnitPriceAfterDiscount", "UnitsQuantity" };
            var BulkUploadResponse = ApiClient.TransactionLines.BulkUpload(models, eOverwriteMethod.full, eBulkUploadMethod.Zip, fieldsToUpload, true, TypeMetadata.SubTypeID);
            GetBulkJobInfoResponse GetBulkJobInfoResponse = ApiClient.TransactionLines.WaitForBulkJobToComplete(BulkUploadResponse.JobID);

            //note: we do not validate result here 
            if (GetBulkJobInfoResponse.Status != "Ok")
            {
                throw new Exception("Bulk upload failed. Status = " + GetBulkJobInfoResponse.Status == null ? "null" : GetBulkJobInfoResponse.Status + " Error = " + GetBulkJobInfoResponse.Error == null ? "null " : GetBulkJobInfoResponse.Error);
            }

            PepperiDbContext.TransactionLines = models.ToList();           //add to the context

            return GetBulkJobInfoResponse;
        }

        public void UploadWithoutBulk(IEnumerable<TransactionLine> models)
        {
            foreach (var model in models)
            {
                var savedModel = ApiClient.TransactionLines.Upsert(model);
                PepperiDbContext.TransactionLines.Add(savedModel);         //add to the context
            }
        }

        private TransactionLine Generate(TransactionLine model, Transaction Transaction, short TransactionLineOfTransactionIndex, eDataGeneratorLinkMethod DataGeneratorLinkMethod, int index, int numberOfElements, eRandomMethod RandomMethod)
        {
            string randomString = (RandomMethod == eRandomMethod.useIndex) ? index.ToString() : Guid.NewGuid().ToString();

            //Link to Transaction
            switch (DataGeneratorLinkMethod)
            {
                //case eDataGeneratorLinkMethod.use_ExternalID_Property:
                //    model.TransactionExternalID = Transaction.ExternalID;
                //    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.Transaction = new Reference<Transaction>();
                    model.Transaction.Data = new Transaction();
                    model.Transaction.Data.ExternalID = Transaction.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.Transaction = new Reference<Transaction>();
                    model.Transaction.Data = new Transaction();
                    model.Transaction.Data.InternalID = Transaction.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }

            //Link to item (random item. Same item can show on serveral lines of the same transaction)
            Item Item = PepperiDbContext.GetRandomItem();
            switch (DataGeneratorLinkMethod)
            {
                case eDataGeneratorLinkMethod.use_ExternalID_Property:
                    model.ItemExternalID = Item.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.Item = new Reference<Item>();
                    model.Item.Data = new Item();
                    model.Item.Data.ExternalID = Item.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.Item = new Reference<Item>();
                    model.Item.Data = new Item();
                    model.Item.Data.InternalID = Item.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }

            model.LineNumber = TransactionLineOfTransactionIndex;                           //sequence per transaction, start with 1
            model.UnitPriceAfterDiscount = generateRandomDouble(100, 400, 2);
            model.UnitsQuantity = generateRandomDouble(100, 400, 0);

            return model;
        }

        #endregion

        #region Activity (SubType): for every account generate X activities

        public List<Activity> GenerateActivities(int numberOfActivitiesPerAccount, eDataGeneratorLinkMethod DataGeneratorLinkMethod = eDataGeneratorLinkMethod.use_ExternalID_Property, eRandomMethod RandomMethod = eRandomMethod.useIndex)
        {
            int numberOfElements = PepperiDbContext.Accounts.Count() * numberOfActivitiesPerAccount;

            var models = new List<Activity>();

            int i = 0;
            for (int accountIndex = 0; accountIndex < PepperiDbContext.Accounts.Count(); accountIndex++)
            {
                Account Account = PepperiDbContext.Accounts.ElementAt(accountIndex);

                for (int activityOfAccountIndex = 0; activityOfAccountIndex < numberOfActivitiesPerAccount; activityOfAccountIndex++)
                {
                    var model = Generate(new Activity(), Account, DataGeneratorLinkMethod, i, numberOfElements, RandomMethod);
                    models.Add(model);
                    i++;
                }
            }

            return models;
        }

        public GetBulkJobInfoResponse UploadAsBulk(IEnumerable<Activity> models)
        {
            //for Activity, the url of the BulkUpload must include the SUBTypeID of activity
            //Pick Random Activity sub type ID from the concrete types
            TypeMetadata TypeMetadata = ApiClient.Activities.GetSubTypesMetadata().FirstOrDefault();
            if (TypeMetadata == null || TypeMetadata.SubTypeID == null)
            {
                throw new PepperiException("No subtypes for activity.");
            }


            List<string> fieldsToUpload = new List<string>() { "ExternalID", "AccountExternalID", "ContactPersonExternalID", "ActionDateTime", "Title", "Status" };
            var BulkUploadResponse = ApiClient.Activities.BulkUpload(models, eOverwriteMethod.full, eBulkUploadMethod.Zip, fieldsToUpload, true, TypeMetadata.SubTypeID);
            GetBulkJobInfoResponse GetBulkJobInfoResponse = ApiClient.Activities.WaitForBulkJobToComplete(BulkUploadResponse.JobID);

            //note: we do not validate result here 
            if (GetBulkJobInfoResponse.Status != "Ok")
            {
                throw new Exception("Bulk upload failed. Status = " + GetBulkJobInfoResponse.Status == null ? "null" : GetBulkJobInfoResponse.Status + " Error = " + GetBulkJobInfoResponse.Error == null ? "null " : GetBulkJobInfoResponse.Error);
            }

            PepperiDbContext.Activities = models.ToList();           //add to the context

            return GetBulkJobInfoResponse;
        }

        public void UploadWithoutBulk(IEnumerable<Activity> models)
        {
            foreach (var model in models)
            {
                var savedModel = ApiClient.Activities.Upsert(model);
                PepperiDbContext.Activities.Add(savedModel);         //add to the context
            }
        }

        private Activity Generate(Activity model, Account Account, eDataGeneratorLinkMethod DataGeneratorLinkMethod, int index, int numberOfElements, eRandomMethod RandomMethod)
        {
            string randomString = (RandomMethod == eRandomMethod.useIndex) ? index.ToString() : Guid.NewGuid().ToString();

            model.ExternalID = "ACT_ExternalID_" + randomString;

            //sets the type of the activity
            //This is relevant only when the data is generated for non bulk upload
            //For bulk upload, the activity type id is sent to API on the url as resource identifier    (baseEndoint gets it as  subTypeID)
            if (DataGeneratorLinkMethod != eDataGeneratorLinkMethod.use_ExternalID_Property)
            {
                //model.ActivityTypeID = TBD
            }
            else
            {
                //on bulk uplaod the value comes from url
            }


            //Link to Account
            switch (DataGeneratorLinkMethod)
            {
                case eDataGeneratorLinkMethod.use_ExternalID_Property:
                    model.AccountExternalID = Account.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.Account = new Reference<Account>();
                    model.Account.Data = new Account();
                    model.Account.Data.ExternalID = Account.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.Account = new Reference<Account>();
                    model.Account.Data = new Account();
                    model.Account.Data.InternalID = Account.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }

            //Link to Contact
            Contact Contact = PepperiDbContext.GetRandomContact_OfAccount(Account.ExternalID);
            switch (DataGeneratorLinkMethod)
            {
                //case eDataGeneratorLinkMethod.use_ExternalID_Property:
                //    model.ContactPersonExternalID = Contact.ExternalID;
                //    break;
                case eDataGeneratorLinkMethod.use_ExternalID_of_reference:
                    model.ContactPerson = new Reference<Contact>();
                    model.ContactPerson.Data = new Contact();
                    model.ContactPerson.Data.ExternalID = Contact.ExternalID;
                    break;
                case eDataGeneratorLinkMethod.use_internalID_of_reference:
                    model.ContactPerson = new Reference<Contact>();
                    model.ContactPerson.Data = new Contact();
                    model.ContactPerson.Data.InternalID = Contact.InternalID;
                    break;
                default:
                    throw new Exception("UnexpectedLinkMode");
            }


            //Link To Agent - do not link


            model.ActionDateTime = generateUTCDateTime(index / (-2), (-1) * index);    //change day evey 2 items, seconds are sequence  
            model.Title = "Activity_Title_" + randomString;
            model.Status = (int)eStatus.InCreation;

            return model;
        }

        #endregion


        //#region Item Images

        //private void UploadImagesForItems(int numberOfItems)
        //{
        //    for (int i = 0; i < numberOfItems; i++)
        //    {
        //        if (On_DataGenerator_Progress != null) { On_DataGenerator_Progress("uploading image for item external id: " + PepperiDbContext.Items[i].ExternalID); }
        //        UploadImageForItem(PepperiDbContext.Items[i]);
        //    }
        //}

        //private void UploadImageForItem(Item ItemFromContext)
        //{
        //    string Image1_UploadUrl = "https://upload.wikimedia.org/wikipedia/en/a/a9/Example.jpg";
        //    byte[] Image1_AsByteArray = null;
        //    using (var wc = new System.Net.WebClient())
        //    {
        //        Image1_AsByteArray = wc.DownloadData(Image1_UploadUrl);
        //    }

        //    string Image1_AsBase64String = Convert.ToBase64String(Image1_AsByteArray);


        //    Item Item = new Item();
        //    Item.ExternalID = ItemFromContext.ExternalID;

        //    Item.Image = new Image();
        //    Item.Image.URL = Image1_UploadUrl;

        //    Item.Image2 = new Image();
        //    Item.Image2.URL = Image1_UploadUrl;

        //    Item UpsertResponse2 = ApiClient.Items.Upsert(Item);
        //}

        //#endregion


        #region private helpers


        /// <summary>
        /// 
        /// </summary>
        /// <param name="AddDays"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1. We do not create ms (Pepperi does not save it)
        /// 2. The time should be utc. The SDK serializer serialzies DateTime as utc and so does Api deserializer
        /// </remarks>
        private DateTime generateUTCDateTime(int AddDays, int AddSeconds)
        {
            DateTime NowTime = DateTime.UtcNow;                                                                                                 //with ms
            //var KindOfNowTime = NowTime.Kind; 

            NowTime = NowTime.AddDays(AddDays);
            NowTime = NowTime.AddSeconds(AddSeconds);

            DateTime result =
                new DateTime(NowTime.Year, NowTime.Month, NowTime.Day, NowTime.Hour, NowTime.Minute, NowTime.Second, NowTime.Kind)              //without ms
                .ToUniversalTime();
            //var kindOfResult = result.Kind;

            return result;
        }

        private int generateRandomInt(int min, int max)
        {
            int result = Random.Next(min, max);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="numberOfDecimalDigits"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1. If you want a numeric real literal to be treated as decimal, use the m suffix 
        /// </remarks>
        private decimal generateRandomDecimal(decimal min, decimal max, int numberOfDecimalDigits)
        {
            decimal random = min + Random.Next(0, 100) / 100m * (max - min);
            decimal divPart = Math.Truncate(random);

            decimal modPart = 0;
            if (numberOfDecimalDigits > 0)
            {
                int modulo = 1;
                for (int i = 1; i <= numberOfDecimalDigits; i++)
                {
                    modulo = modulo * 10;
                }

                modPart = 1m * Random.Next(0, modulo - 1) / modulo;
            }

            decimal result = divPart + modPart;

            if (result < min)
            {
                result = min;
            }
            if (result > max)
            {
                result = max;
            }

            return result;
        }

        private double generateRandomDouble(double min, double max, int numberOfDecimalDigits)
        {
            double random = min + Random.Next(0, 100) / 100.0 * (max - min);
            double divPart = Math.Truncate(random);


            double modPart = 0;
            if (numberOfDecimalDigits > 0)
            {
                int modulo = 1;
                for (int i = 1; i <= numberOfDecimalDigits; i++)
                {
                    modulo = modulo * 10;
                }

                modPart = 1.0 * Random.Next(0, modulo - 1) / modulo;
            }

            double result = divPart + modPart;
            if (result < min)
            {
                result = min;
            }
            if (result > max)
            {
                result = max;
            }

            return result;
        }

        private int calc_Block_index(int index, int numberOfElements, int changeValueEveryXElements)
        {
            int blockSize = changeValueEveryXElements;
            int currentBlock = blockSize == 0 ? 1 : index / blockSize;
            return currentBlock;
        }

        #endregion

    }


    /// <summary>
    /// allows the calling application get progress notification from the generator (and log them)
    /// </summary>
    /// <param name="message"></param>
    public delegate void On_DataGenerator_Progress(string message);

    /// <summary>
    /// holds the data stored in Pepperi by Bulk or single Api
    /// </summary>
    /// <remarks>
    /// in case bulk is used to generate the data, we do not have the internal id in the result, but only the external id
    /// </remarks>
    public class PepperiDbContext
    {
        #region Properties

        private Random Random { get; set; }

        public List<Account> Accounts { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Inventory> Inventory { get; set; }
        public List<Item> Items { get; set; }
        public List<ItemPrice> ItemPrices { get; set; }      //every item has price per price list (that belongs to account)
        public List<PriceList> PriceLists { get; set; }      //acocnut has price list
        public List<Transaction> Transactions { get; set; }
        public List<TransactionLine> TransactionLines { get; set; }

        #endregion


        #region constructor

        public PepperiDbContext()
        {
            this.Random = new Random(DateTime.Now.Millisecond);

            this.Accounts = new List<Account>();
            this.Activities = new List<Activity>();
            this.Contacts = new List<Contact>();
            this.Inventory = new List<Inventory>();
            this.Items = new List<Item>();
            this.ItemPrices = new List<ItemPrice>();
            this.PriceLists = new List<PriceList>();
            this.Transactions = new List<Transaction>();
            this.TransactionLines = new List<TransactionLine>();
        }

        #endregion

        #region Public methods

        public Account GetRandomAccount()
        {
            if (this.Accounts == null || this.Accounts.Count() == 0)
            {
                throw new Exception("no Accounts in db");
            }

            int index = Random.Next(0, this.Accounts.Count() - 1);

            var result = this.Accounts.ElementAt(index);

            return result;

        }

        public Contact GetRandomContact_OfAccount(string AccountExternalID)
        {
            IEnumerable<Contact> AccountContacts = this.Contacts;//.Where(contact => contact.AccountExternalID == AccountExternalID);

            if (AccountContacts == null || AccountContacts.Count() == 0)
            {
                throw new Exception("Account does not have contacts in context");
            }

            int index = Random.Next(0, AccountContacts.Count() - 1);

            var result = AccountContacts.ElementAt(index);

            return result;
        }

        public Item GetRandomItem()
        {
            if (this.Items == null || this.Items.Count() == 0)
            {
                throw new Exception("no items in db");
            }

            int index = Random.Next(0, this.Items.Count() - 1);

            var result = this.Items.ElementAt(index);

            return result;

        }

        public ItemPrice GetRandomItemPrice()
        {
            if (this.ItemPrices == null || this.ItemPrices.Count() == 0)
            {
                throw new Exception("no ItemPrices in db");
            }

            int index = Random.Next(0, this.ItemPrices.Count() - 1);

            var result = this.ItemPrices.ElementAt(index);

            return result;

        }

        public PriceList GetRandomPriceList()
        {
            if (this.PriceLists == null || this.PriceLists.Count() == 0)
            {
                throw new Exception("no PriceLists in db");
            }

            int index = Random.Next(0, this.PriceLists.Count() - 1);

            var result = this.PriceLists.ElementAt(index);

            return result;

        }

        #endregion


    }

    public class DataQuatities
    {
        public int generate_X_Item { get; set; }
        public int generate_X_PriceList { get; set; }
        public int generate_X_accounts { get; set; }
        public int generate_X_contactsPerAccount { get; set; }
        public int generate_X_transactionsPerAccount { get; set; }
        public short generate_X_transactionLinesPerTransaction { get; set; }
        public int generate_X_activitierPerAccount { get; set; }
    }

    public enum eDataGeneratorLinkMethod
    {
        use_ExternalID_Property,        //for bulk API calls
        use_ExternalID_of_reference,    //for non bulk API calls
        use_internalID_of_reference     //for non bulk API calls
    }

    public enum eRandomMethod
    {
        useGuide,
        useIndex
    }

}



