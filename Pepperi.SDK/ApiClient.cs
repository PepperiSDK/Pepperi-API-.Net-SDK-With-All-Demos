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

        public ApiClient(string ApiBaseUri, IAuthentication Authentication, ILogger Logger)
        {
             Initialize(ApiBaseUri, Authentication, Logger);
        }


        private void Initialize(string ApiBaseUri, IAuthentication Authentication, ILogger Logger)
        {
            this.Accounts =             new AccountsEndpoint            (ApiBaseUri, Authentication, Logger);
            this.AccountCatalogs =      new AccountCatalogsEndpoint     (ApiBaseUri, Authentication, Logger);
            this.AccountInventory =     new AccountInventoryEndpoint    (ApiBaseUri, Authentication, Logger);
            this.AccountUsers =         new AccountUsersEndpoint        (ApiBaseUri, Authentication, Logger);
            this.Activities =           new ActivitiesEndpoint          (ApiBaseUri, Authentication, Logger);
            this.Catalogs =             new CatalogsEndpoint            (ApiBaseUri, Authentication, Logger);
            this.Contacts =             new ContactsEndpoint            (ApiBaseUri, Authentication, Logger);
            this.Inventory =            new InventoryEndpoint           (ApiBaseUri, Authentication, Logger);
            this.ItemDimensions1 =      new ItemDimensions1Endpoint     (ApiBaseUri, Authentication, Logger);
            this.ItemDimensions2 =      new ItemDimensions2Endpoint     (ApiBaseUri, Authentication, Logger);
            this.Items =                new ItemsEndpoint               (ApiBaseUri, Authentication, Logger);
            this.PriceLists =           new PriceListsEndpoint          (ApiBaseUri, Authentication, Logger);
            this.ItemPrices =           new ItemPricesEndpoint          (ApiBaseUri, Authentication, Logger);
            this.SpecialPriceLists =    new SpecialPriceListsEndpoint   (ApiBaseUri, Authentication, Logger);
            this.Transactions =         new TransactionsEndpoint        (ApiBaseUri, Authentication, Logger);
            this.TransactionLines =     new TransactionLinesEndpoint    (ApiBaseUri, Authentication, Logger);
            this.Users =                new UsersEndpoint               (ApiBaseUri, Authentication, Logger);
            this.UserDefinedTables =    new UserDefinedTablesEndpoint   (ApiBaseUri, Authentication, Logger);

        }

        #endregion

        #region Properties methods

        public AccountsEndpoint             Accounts            { get; private set; }
        public AccountCatalogsEndpoint      AccountCatalogs     { get; private set; }
        public AccountInventoryEndpoint     AccountInventory    { get; private set; }
        public AccountUsersEndpoint         AccountUsers        { get; private set; }
        public ActivitiesEndpoint           Activities          { get; private set; }
        public CatalogsEndpoint             Catalogs            { get; private set; }
        public ContactsEndpoint             Contacts            { get; private set; }
        public InventoryEndpoint            Inventory           { get; private set; }
        public ItemDimensions1Endpoint      ItemDimensions1     { get; private set; }
        public ItemDimensions2Endpoint      ItemDimensions2     { get; private set; }
        public ItemsEndpoint                Items               { get; private set; }
        public ItemPricesEndpoint           ItemPrices          { get; private set; }
        public PriceListsEndpoint           PriceLists          { get; private set; }
        public SpecialPriceListsEndpoint    SpecialPriceLists   { get; private set; }
        public TransactionsEndpoint         Transactions        { get; private set; }
        public TransactionLinesEndpoint     TransactionLines    { get; private set; }
        public UsersEndpoint                Users               { get; private set; }
        public UserDefinedTablesEndpoint    UserDefinedTables   { get; private set; }

        #endregion

        #region Private methods
        #endregion
    }
}
