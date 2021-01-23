using EstateManager.Estates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EstateManager.Utils
{
    class Validator
    {
        public static double AskDouble(string inputQuery)
        {
            double userInput;
            bool isValidated = false;

            do
            {
                Console.Write(inputQuery);


                if (double.TryParse(Console.ReadLine(), out userInput))
                {
                    if (userInput < 0)
                    {
                        Console.WriteLine("Wprowadzona wartość jest ujemna!");
                        PrepareConsole();
                    }
                    else
                    {
                        isValidated = true;
                    }

                }
                else
                {
                    Console.WriteLine("Wprowadzona wartość jest nieodpowiednia!");
                    PrepareConsole();
                }
            } while (!isValidated);

            return userInput;
        }

        public static OwnerType AskOwner(string inputQuery)
        {
            char userInput;
            bool isValidated = false;

            Regex rx = new Regex("[pPmMoO]");

            do
            {
                Console.Write(inputQuery);

                if (char.TryParse(Console.ReadLine(), out userInput) && rx.IsMatch(userInput.ToString()))
                {
                    isValidated = true;
                    userInput = char.ToUpper(userInput);
                }
                else
                {
                    Console.WriteLine("Wprowadzona wartość jest nieodpowiednia!");
                    PrepareConsole();
                }
            } while (!isValidated);

            switch (userInput)
            {
                default:
                    return OwnerType.Other;
                case 'M':
                    return OwnerType.City;
                case 'P':
                    return OwnerType.Private;
            }

        }

        public static ParcelType AskParcelType(string inputQuery)
        {
            char userInput;
            bool isValidated = false;

            Regex rx = new Regex("[rRbB]");

            do
            {
                Console.Write(inputQuery);

                if (char.TryParse(Console.ReadLine(), out userInput) && rx.IsMatch(userInput.ToString()))
                {
                    isValidated = true;
                    userInput = char.ToUpper(userInput);
                }
                else
                {
                    Console.WriteLine("Wprowadzona wartość jest nieodpowiednia!");
                    PrepareConsole();
                }
            } while (!isValidated);

            return userInput == 'B' ? ParcelType.BuildingLand : ParcelType.AgroLand;

        }

        public static string AskAddress(string inputQuery)
        {
            // TODO: Need to implement regex to validate address given by the user
            Console.Write(inputQuery);
            return Console.ReadLine();
        }

        public static int AskInteger(string inputQuery)
        {
            int userInput;
            bool isValidated = false;

            do
            {
                Console.Write(inputQuery);


                if (int.TryParse(Console.ReadLine(), out userInput))
                {
                    if (userInput < 0)
                    {
                        Console.WriteLine("Wprowadzona wartość jest ujemna!");
                        PrepareConsole();
                    }
                    else
                    {
                        isValidated = true;
                    }

                }
                else
                {
                    Console.WriteLine("Wprowadzona wartość jest nieodpowiednia!");
                    PrepareConsole();
                }
            } while (!isValidated);

            return userInput;
        }

        private static void PrepareConsole()
        {
            Console.ReadLine();
            Console.Clear();
        }
    }

}

