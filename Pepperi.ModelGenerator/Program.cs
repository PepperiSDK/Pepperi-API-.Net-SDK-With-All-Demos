﻿using Pepperi.SDK;
using Pepperi.SDK.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.ModelGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Inital setup

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls12
            | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            #endregion

            #region create apiClient instance

            ILogger Logger = Factory.GetLogger();
            ApiClient ApiClient = Factory.CreateApiClientForPrivateApplication(Logger);

            #endregion

            #region calculate Model Directory

            string exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string ModelDirectory = exeDirectory + "\\..\\..\\..\\Pepperi.SDK\\Model";

            #endregion

            #region Generate Models Code

            bool ShouldGenerateCustomFields = Ask_ShouldGenerateCustomFields();
            bool ShouldGenerateUDC = Ask_ShouldGenerateUDC();
            
            ModelGenerator ModelGenerator = new ModelGenerator(ApiClient);
            ModelGenerator.GenerateModelsCode("Pepperi.SDK.Model", ModelDirectory, ShouldGenerateCustomFields, ShouldGenerateUDC);

            #endregion

            Console.WriteLine("Operation completed successfuly !");
            Console.ReadLine();

        }



        private static bool Ask_ShouldGenerateCustomFields()
        {
            bool? result = null;

            string userInput = null;
            while (userInput != "yes" && userInput != "no")
            {
                System.Console.WriteLine("enter 'yes' to generate model including user defined fields or 'no' to generate without it");

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

        private static bool Ask_ShouldGenerateUDC()
        {
            return Ask_WithBoolResponse("Enter 'yes' to generate User Defined Collection Model or 'no' to generate without it. Please note, that this requires additional addons to be installed!");
        }

        private static bool Ask_WithBoolResponse(string message)
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

    }
}
