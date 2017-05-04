# Pepperi-API-.Net-SDK-With-All-Demos

The Pepperi SDK for .Net provides a .Net native interface to the Pepperi RESTful API - This solution includes all demo projects.

* Installation
* Samples

# Installation

Download the source code from github and compile it.

# Samples

Update transaction status by InteranlID:

var IAuthentication = new PrivateAuthentication("<your_general_app_consumer_key>", "<your_specific_company_api_token>");

ApiClient ApiClient = new ApiClient("https://api.pepperi.com/v1.0/", IAuthentication,null); // you can pass a logger - in this case it is null

Transaction TransactionForUpdate = new Transaction();

TransactionForUpdate.InternalID = 32456;

TransactionForUpdate.Status = 2; //change status to "Submitted" status (2)

Transaction UpsertResponse_OnUpdate = ApiClient.Transactions.Upsert(TransactionForUpdate);

 
