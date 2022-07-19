using Pepperi.SDK.Helpers;
using Pepperi.SDK.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Contracts;

namespace Pepperi.SDK
{
    public class ApiClient
    {
        #region Properties
        #endregion


        #region Constructor

        public ApiClient(string ApiBaseUri, IAuthentication Authentication, ILogger Logger, AuthentificationManager AuthentificationManager = null)
        {
            Initialize(ApiBaseUri, Authentication, Logger, AuthentificationManager);
        }


        private void Initialize(string ApiBaseUri, IAuthentication Authentication, ILogger Logger, AuthentificationManager AuthentificationManager = null)
        {
            this.Accounts = new AccountsEndpoint(ApiBaseUri, Authentication, Logger);
            this.AccountsMetaData = new AccountsMetaData_Endpoint(ApiBaseUri, Authentication, Logger);

            this.AccountCatalogs = new AccountCatalogsEndpoint(ApiBaseUri, Authentication, Logger);
            this.AccountCatalogsMetaData = new AccountCatalogsMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.AccountInventory = new AccountInventoryEndpoint(ApiBaseUri, Authentication, Logger);
            this.AccountInventoryMetaData = new AccountInventoryMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.AccountUsers = new AccountUsersEndpoint(ApiBaseUri, Authentication, Logger);
            this.AccountUsersMetaData = new AccountUsersMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.Activities = new ActivitiesEndpoint(ApiBaseUri, Authentication, Logger);
            this.ActivitiesMetaData = new ActivitiesMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.Catalogs = new CatalogsEndpoint(ApiBaseUri, Authentication, Logger);
            this.CatalogsMetaData = new CatalogsMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.Contacts = new ContactsEndpoint(ApiBaseUri, Authentication, Logger);
            this.ContactsMetaData = new ContactsMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.Inventory = new InventoryEndpoint(ApiBaseUri, Authentication, Logger);
            this.InventoryMetaData = new InventoryMetaData_Endpoint(ApiBaseUri, Authentication, Logger);

            this.ItemDimensions1 = new ItemDimensions1Endpoint(ApiBaseUri, Authentication, Logger);
            this.ItemDimensions1_MetaData = new ItemDimensions1_MetaData_Endpoint(ApiBaseUri, Authentication, Logger);

            this.ItemDimensions2 = new ItemDimensions2Endpoint(ApiBaseUri, Authentication, Logger);
            this.ItemDimensions2_MetaData = new ItemDimensions2_MetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.Items = new ItemsEndpoint(ApiBaseUri, Authentication, Logger);
            this.ItemsMetaData = new ItemsMetaData_Endpoint(ApiBaseUri, Authentication, Logger);

            this.PriceLists = new PriceListsEndpoint(ApiBaseUri, Authentication, Logger);
            this.PriceListsMetaData = new PriceListsMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.ItemPrices = new ItemPricesEndpoint(ApiBaseUri, Authentication, Logger);
            this.ItemPricesMetaData = new ItemPricesMetaData_Endpoint(ApiBaseUri, Authentication, Logger);

            this.SpecialPriceLists = new SpecialPriceListsEndpoint(ApiBaseUri, Authentication, Logger);
            this.SpecialPriceListsMetaData = new SpecialPriceListsMetaData_Endpoint(ApiBaseUri, Authentication, Logger);

            this.Transactions = new TransactionsEndpoint(ApiBaseUri, Authentication, Logger);
            this.TransactionsMetaData = new TransactionsMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.TransactionLines = new TransactionLinesEndpoint(ApiBaseUri, Authentication, Logger);
            this.TransactionLinesMetaData = new TransactionLinesMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.Users = new UsersEndpoint(ApiBaseUri, Authentication, Logger);
            this.UsersMetaData = new UsersMetaData_Endpoint(ApiBaseUri, Authentication, Logger);


            this.UserDefinedTables = new UserDefinedTablesEndpoint(ApiBaseUri, Authentication, Logger);
            this.UserDefinedTablesMetaData = new UserDefinedTableMetaData_Endpoint(ApiBaseUri, Authentication, Logger);

            this.UserDefinedCollectionsMetaData = new UserDefinedCollectionsMetaData_Endpoint(AuthentificationManager?.IdpAuth, Logger);
            this.UserDefinedCollections = new UserDefinedCollectionsEndpoint(AuthentificationManager, Logger);
        }

        #endregion

        #region Properties methods

        public AccountsEndpoint Accounts { get; private set; }
        public AccountsMetaData_Endpoint AccountsMetaData { get; private set; }

        public AccountCatalogsEndpoint AccountCatalogs { get; private set; }
        public AccountCatalogsMetaData_Endpoint AccountCatalogsMetaData { get; private set; }


        public AccountInventoryEndpoint AccountInventory { get; private set; }
        public AccountInventoryMetaData_Endpoint AccountInventoryMetaData { get; private set; }


        public AccountUsersEndpoint AccountUsers { get; private set; }
        public AccountUsersMetaData_Endpoint AccountUsersMetaData { get; private set; }


        public ActivitiesEndpoint Activities { get; private set; }
        public ActivitiesMetaData_Endpoint ActivitiesMetaData { get; private set; }


        public CatalogsEndpoint Catalogs { get; private set; }
        public CatalogsMetaData_Endpoint CatalogsMetaData { get; private set; }


        public ContactsEndpoint Contacts { get; private set; }
        public ContactsMetaData_Endpoint ContactsMetaData { get; private set; }


        public InventoryEndpoint Inventory { get; private set; }
        public InventoryMetaData_Endpoint InventoryMetaData { get; private set; }


        public ItemDimensions1Endpoint ItemDimensions1 { get; private set; }
        public ItemDimensions1_MetaData_Endpoint ItemDimensions1_MetaData { get; private set; }


        public ItemDimensions2Endpoint ItemDimensions2 { get; private set; }
        public ItemDimensions2_MetaData_Endpoint ItemDimensions2_MetaData { get; private set; }


        public ItemsEndpoint Items { get; private set; }
        public ItemsMetaData_Endpoint ItemsMetaData { get; private set; }


        public ItemPricesEndpoint ItemPrices { get; private set; }
        public ItemPricesMetaData_Endpoint ItemPricesMetaData { get; private set; }


        public PriceListsEndpoint PriceLists { get; private set; }
        public PriceListsMetaData_Endpoint PriceListsMetaData { get; private set; }


        public SpecialPriceListsEndpoint SpecialPriceLists { get; private set; }
        public SpecialPriceListsMetaData_Endpoint SpecialPriceListsMetaData { get; private set; }


        public TransactionsEndpoint Transactions { get; private set; }
        public TransactionsMetaData_Endpoint TransactionsMetaData { get; private set; }


        public TransactionLinesEndpoint TransactionLines { get; private set; }
        public TransactionLinesMetaData_Endpoint TransactionLinesMetaData { get; private set; }

        public UsersEndpoint Users { get; private set; }
        public UsersMetaData_Endpoint UsersMetaData { get; private set; }


        public UserDefinedTablesEndpoint UserDefinedTables { get; private set; }

        public UserDefinedTableMetaData_Endpoint UserDefinedTablesMetaData { get; private set; }

        public UserDefinedCollectionsMetaData_Endpoint UserDefinedCollectionsMetaData { get; private set; }
        public UserDefinedCollectionsEndpoint UserDefinedCollections { get; private set; }


        #endregion

        #region Private methods
        #endregion
    }
}
