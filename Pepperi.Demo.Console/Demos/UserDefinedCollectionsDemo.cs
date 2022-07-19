using Pepperi.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.Demo.Console.Demos
{
    static public class UserDefinedCollectionsDemo
    {

        static public void StartDemo(ApiClient ApiClient)
        {
            var shouldStartDemo = ConsoleInteractions.AskWithBoolResponse("Enter 'yes' to start UDC Demo or 'no' to skip it.\nPlease note, that this requires additional addons to be installed!");
            if (!shouldStartDemo) return;

            StartUploadFileDemo(ApiClient);
        }

        static private void StartUploadFileDemo(ApiClient ApiClient)
        {
            var shouldStartDemo = ConsoleInteractions.AskWithBoolResponse("Start UDC File Uploading Demo? (type 'yes' / 'no')");
            if (!shouldStartDemo) return;

            var shouldDoAgain = true;
            while (shouldDoAgain)
            {
                var filePath = ConsoleInteractions.AskFilePath("Please type path to file! (Type empty to exit)");
                var schemeName = ConsoleInteractions.AskWithStringResponse("Please type Scheme Name");

                try
                {
                    var result = ApiClient.UserDefinedCollections.BulkUploadFile(schemeName, filePath);
                    System.Console.WriteLine($"File was uploaded! Total Number of Rows - {result.Total}, Failed - {result.TotalFailed}");
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Looks like something goes wrong. Error Message - " + (e.Message ?? "No Message"));
                }


                shouldDoAgain = ConsoleInteractions.AskWithBoolResponse("Should do file upload again? (type 'yes' / 'no')");
            }
        }
    }
}
