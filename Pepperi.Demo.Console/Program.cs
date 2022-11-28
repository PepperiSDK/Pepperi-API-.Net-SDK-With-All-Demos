using Pepperi.Demo.Console.Demos;
using Pepperi.SDK;
using Pepperi.SDK.Contracts;
using Pepperi.SDK.Exceptions;
using Pepperi.SDK.Model;
using Pepperi.SDK.Model.Fixed;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.Demo.Console
{
    //-if there is token print it
    //-add: if api token is empty or null, immidiately prompt user,pass
    //-progress
    //-begining : skip genertion
    //-add number of trasctions
    //-see agent name works
    //4. end: read and print transaciton in last 30 datys



    //5. model generator: same beginig
    //6. enter yes to generate model including user defined fields or no to generate without it



    //images

    class Program
    {
        #region Properties

        private static ILogger Logger { get; set; }
        private static ApiClient ApiClient { get; set; }

        #endregion

        #region Static Constructor

        static Program()
        {
            Logger = Factory.GetLogger();
            ApiClient = null;
        }

        #endregion

        static void Main(string[] args)
        {
            try
            {
                #region Set ApiClient

                ApiClient = Factory.CreateApiClientForPrivateApplication(Logger);

                #endregion

                #region Ask Should Generate And UploadData

                bool ShouldGenerateAndUploadData = Ask_ShouldGenerateAndUploadData();

                #endregion

                #region read DataQuatities, Generate And upload Data

                if (ShouldGenerateAndUploadData)
                {
                    DataQuatities DataQuatities = GetDataQuatities();
                    PepperiDbContext PepperiDbContext = GenerateAndUploadData(DataQuatities);
                }

                #endregion

                #region Populate Transaction changes from Pepperi UI to ERP

                var shouldShowPopulateTransactionDemo = ConsoleInteractions.AskWithBoolResponse("Should show Transaction Changes Demo? (Type 'yes' / 'no')");
                bool makeAnotherTransaction = shouldShowPopulateTransactionDemo;
                while (makeAnotherTransaction == true)
                {
                    string userInput = null;

                    #region Change data on Pepperi using Pepperi ui, setting its status to Submitted. (eg, submit transaction)

                    while (userInput != "ok")
                    {
                        System.Console.WriteLine("Please Login to Pepperi , view the uploaded data, then make a transaction/s and once you submitted type below ok to see the transaction/s here");
                        userInput = System.Console.ReadLine();
                    }

                    #endregion

                    #region Read submitted data from Pepperi using Pepperi API (Status = Submitted)

                    IEnumerable<Transaction> transactions = ReadSubmittedTransactions();

                    #endregion

                    #region Print submitted transaction lines

                    PrintTransactions(transactions);

                    #endregion

                    #region Update ERP with the submitted Pepperi data
                    #endregion

                    #region Update Status of the transactions On Pepperi (to inidcate the changes were saved to ERP)

                    UpdateTransactionStatus(transactions, eStatus.Invoice);

                    #endregion

                    #region ask: make another transaction or exit

                    userInput = null;
                    while (userInput != "yes" && userInput != "no")
                    {
                        System.Console.WriteLine("The program updated the Status field to simulate the integration process - so that only new submitted transactions will be displayed the next time you type 'ok'.\r\n");
                        System.Console.WriteLine("type 'yes' to make another transaction or 'no' to display how many transactions made in the last 30 days.");

                        userInput = System.Console.ReadLine();
                        if (userInput == "yes")
                        {
                            makeAnotherTransaction = true;
                        }
                        if (userInput == "no")
                        {
                            makeAnotherTransaction = false;
                        }
                    }

                    #endregion
                }

                #endregion


                #region Print number of transactions in last 30 days

                double numberOfDays = 30;
                long count = CountTransactions(numberOfDays);
                System.Console.WriteLine("There were " + count + " Transactions in the last " + numberOfDays + " days");

                #endregion


                #region Other Demos

                UserDefinedCollectionsDemo.StartDemo(ApiClient);

                #endregion

                #region Goodbye

                System.Console.WriteLine("This was a demo program to help you integrate quickly with Pepperi. Visit us on developer.pepperi.com. ");
                System.Console.ReadKey();

                #endregion

            }

            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                System.Console.ReadKey();
            }

        }



        private static bool Ask_ShouldGenerateAndUploadData()
        {
            bool? result = null;

            string userInput = null;
            while (userInput != "yes" && userInput != "no")
            {
                System.Console.WriteLine("Enter 'yes' to generate and upload data or 'no' to skip to display transactions.");

                userInput = System.Console.ReadLine();
                if (userInput == "no")
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }

            return result.Value;
        }


        private static DataQuatities GetDataQuatities()
        {
            //step1: ask user whether to use default data quantities
            bool? useDefaultDataQuanitites = null;
            while (useDefaultDataQuanitites == null)
            {
                System.Console.WriteLine("enter 'yes' to generate and upload data in default size ('low') or 'no' to select data size");
                string resultAsString = System.Console.ReadLine();

                if (resultAsString == "yes")
                {
                    useDefaultDataQuanitites = true;
                }

                if (resultAsString == "no")
                {
                    useDefaultDataQuanitites = false;
                }
            }


            //step2: set size 
            string size = null;
            if (useDefaultDataQuanitites.Value == true)
            {
                size = "low";
            }


            while (size == null)
            {
                System.Console.WriteLine("enter high/mid/low/min");
                string resultAsString = System.Console.ReadLine();

                if (resultAsString == "min" || resultAsString == "low" || resultAsString == "mid" || resultAsString == "high")
                {
                    size = resultAsString;
                }
            }

            //step3: initialize DataQuantiites
            DataQuatities DataQuatities = new DataQuatities();
            switch (size)
            {
                case "min":
                    {
                        DataQuatities.generate_X_Item = 2;
                        DataQuatities.generate_X_PriceList = 2;
                        DataQuatities.generate_X_accounts = 2;
                        DataQuatities.generate_X_contactsPerAccount = 2;
                        DataQuatities.generate_X_transactionsPerAccount = 2;
                        DataQuatities.generate_X_transactionLinesPerTransaction = 2;
                        DataQuatities.generate_X_activitierPerAccount = 2;
                        DataQuatities.generate_X_UserDefinedTableDocuments = 2;
                        break;
                    }
                case "low":
                    {
                        DataQuatities.generate_X_Item = 1000;
                        DataQuatities.generate_X_PriceList = 10;
                        DataQuatities.generate_X_accounts = 100;
                        DataQuatities.generate_X_contactsPerAccount = 5;
                        DataQuatities.generate_X_transactionsPerAccount = 10;
                        DataQuatities.generate_X_transactionLinesPerTransaction = 5;
                        DataQuatities.generate_X_activitierPerAccount = 4;
                        DataQuatities.generate_X_UserDefinedTableDocuments = 10;
                        break;
                    }
                case "mid":
                    {
                        DataQuatities.generate_X_Item = 10000;
                        DataQuatities.generate_X_PriceList = 10;
                        DataQuatities.generate_X_accounts = 1000;
                        DataQuatities.generate_X_contactsPerAccount = 5;
                        DataQuatities.generate_X_transactionsPerAccount = 10;
                        DataQuatities.generate_X_transactionLinesPerTransaction = 10;
                        DataQuatities.generate_X_activitierPerAccount = 10;
                        DataQuatities.generate_X_UserDefinedTableDocuments = 100;
                        break;
                    }
                case "high":
                    {
                        DataQuatities.generate_X_Item = 100000;
                        DataQuatities.generate_X_PriceList = 10;
                        DataQuatities.generate_X_accounts = 10000;
                        DataQuatities.generate_X_contactsPerAccount = 5;
                        DataQuatities.generate_X_transactionsPerAccount = 10;
                        DataQuatities.generate_X_transactionLinesPerTransaction = 10;
                        DataQuatities.generate_X_activitierPerAccount = 10;
                        DataQuatities.generate_X_UserDefinedTableDocuments = 1000;
                        break;
                    }
                default:
                    throw new PepperiException("unexpected size: " + size == null ? " " : size);

            }

            return DataQuatities;
        }

        private static PepperiDbContext GenerateAndUploadData(DataQuatities DataQuatities)
        {
            System.Console.WriteLine("Generating and Uploading Data...");
            DataGenerator DataGenerator = new DataGenerator(Logger, ApiClient, On_DataGenerator_Progress);
            DataGeneratorOptions dataGeneratorOptions = GetDataGeneratorOptions();
            PepperiDbContext PepperiDbContext = DataGenerator.GenerateAndUploadData(DataQuatities, dataGeneratorOptions);

            System.Console.WriteLine("finished uploading all data successfully!");

            return PepperiDbContext;
        }

        private static DataGeneratorOptions GetDataGeneratorOptions() {
            var udcConsoleInfo = "\nPlease note, that UDC requires next: \n";
            udcConsoleInfo += "1) Additional addons to be installed;\n";
            udcConsoleInfo += "2) UDC Collection: \n";
            udcConsoleInfo += "2.1) Collection Name: 'MyTestCollection';\n";
            udcConsoleInfo += "2.2) 'testBoolField' Bool field;\n";
            udcConsoleInfo += "2.3) 'testDateTimeField' DateTime field;\n";
            udcConsoleInfo += "2.4) 'testDoubleField' Double field;\n";
            udcConsoleInfo += "2.5) 'testStringArrayField' Array of String field;\n";
            udcConsoleInfo += "2.6) 'testIntegerArrayField' Array of Integer field;\n";
            udcConsoleInfo += "2.7) 'key' string field;\n";
            System.Console.WriteLine(udcConsoleInfo);
            var shouldGenerateUDC = ConsoleInteractions.AskWithBoolResponse("Generate UDC Data? ('yes' / 'no')");
            return new DataGeneratorOptions {
                ShouldGenerateUDC = shouldGenerateUDC
            };
        }

        private static void On_DataGenerator_Progress(string message)
        {
            System.Console.WriteLine(message);
        }


        private static IEnumerable<Transaction> ReadSubmittedTransactions()
        {
            IEnumerable<Transaction> Transactions = ApiClient.Transactions.Find(
                            where: "Status=" + (int)eStatus.Submitted,
                            order_by: "ModificationDateTime DESC",
                            include_nested: true,   //1:many
                            full_mode: true         //1:1
                            );

            return Transactions;
        }

        private static void PrintTransactions(IEnumerable<Transaction> Transactions)
        {
            System.Console.WriteLine("There are " + Transactions.Count() + " transactions:");
            foreach (var Transaction in Transactions)
            {
                System.Console.WriteLine("Header:");
                System.Console.WriteLine("*******");
                System.Console.WriteLine("DateCreated || Account Name|| PepperiID || ExternalID || Agent Name || LastModified || GrandTotal || Status ");
                System.Console.WriteLine(string.Format("{0} || {1} || {2} || {3} || {4} || {5} || {6} || {7}",
                                                Transaction.CreationDateTime.HasValue ? Transaction.CreationDateTime.Value.ToUniversalTime().ToString("u") : "",
                                                Transaction.Account.Data.Name != null ? Transaction.Account.Data.Name : "",
                                                Transaction.InternalID.HasValue ? Transaction.InternalID.Value.ToString() : "",
                                                Transaction.ExternalID != null ? Transaction.ExternalID : "",
                                                (Transaction.Agent != null && Transaction.Agent.Data != null && Transaction.Agent.Data.FirstName != null ? Transaction.Agent.Data.FirstName : "") + " " +
                                                (Transaction.Agent != null && Transaction.Agent.Data != null && Transaction.Agent.Data.LastName != null ? Transaction.Agent.Data.LastName : "") + " ",
                                                Transaction.ModificationDateTime.HasValue ? Transaction.ModificationDateTime.Value.ToUniversalTime().ToString("u") : "",
                                                Transaction.GrandTotal != null ? Transaction.GrandTotal.ToString() : "",
                                                Transaction.Status.HasValue ? Transaction.Status.ToString() : ""
                                                ));


                System.Console.WriteLine("Lines");
                System.Console.WriteLine("*****");
                System.Console.WriteLine("ItemExternalID ||  UnitsQuantity || Unit Price After Discount || Total Units Price After Discount");

                if (Transaction.TransactionLines != null && Transaction.TransactionLines.Data != null)
                {
                    foreach (var TransactionLine in Transaction.TransactionLines.Data)
                    {
                        System.Console.WriteLine(string.Format("{0} || {1} || {2} || {3}",

                            TransactionLine.ItemExternalID != null ? TransactionLine.ItemExternalID : "",
                            TransactionLine.UnitsQuantity,
                            TransactionLine.UnitPriceAfterDiscount,
                            TransactionLine.TotalUnitsPriceAfterDiscount.HasValue ? TransactionLine.TotalUnitsPriceAfterDiscount.Value.ToString() : ""
                            ));
                    }
                }
            }

        }

        private static void UpdateTransactionStatus(IEnumerable<Transaction> Transactions, eStatus newStatus)
        {
            foreach (var transactionFromDB in Transactions)
            {
                Transaction TransactionToUpsert = new Transaction();
                TransactionToUpsert.InternalID = transactionFromDB.InternalID;
                TransactionToUpsert.Status = (int)newStatus;

                Transaction UpdatedTransaction = ApiClient.Transactions.Upsert(TransactionToUpsert);
            }
        }


        private static long CountTransactions(double numberOfDays)
        {
            long result = ApiClient.Transactions.GetCount(
                            where: "ActionDateTime >" + "'" + DateTime.UtcNow.AddDays((-1) * numberOfDays).ToString("yyyy-MM-ddTHH:mm:ssZ") + "'");

            return result;
        }

    }
}
