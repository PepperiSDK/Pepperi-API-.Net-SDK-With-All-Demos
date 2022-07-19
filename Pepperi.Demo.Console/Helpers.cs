using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.Demo.Console
{
    static public class ConsoleInteractions
    {

        static public bool AskWithBoolResponse(string message)
        {
            bool? result = null;

            string userInput = null;
            while (userInput != "yes" && userInput != "no")
            {
                System.Console.WriteLine(message);

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

        static public string AskWithStringResponse(string message)
        {
            System.Console.WriteLine(message);
            var userInput = System.Console.ReadLine();

            return userInput;
        }

        static public string AskFilePath(string message, bool checkFileExist = true)
        {
            string filePath = null;
            var isCorrectFile = false;
            while (!isCorrectFile)
            {
                filePath = AskWithStringResponse(message);
                if (filePath == "")
                {
                    filePath = null;
                    break;
                }

                if (checkFileExist && !File.Exists(filePath))
                {
                    System.Console.WriteLine("Looks like file doesn't exist!");
                    continue;
                }
                isCorrectFile = true;
            }
            return filePath;
        }
    }
}
