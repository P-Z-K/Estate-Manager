﻿using EstateManager.Data;
using EstateManager.Estates;
using EstateManager.Utils;
using System;

// TODO: Need to implement functionality to functions in Program class
// TODO: May we should add some sort of validator class that ensures custom validate

namespace EstateManager
{
    class Program
    {
        private static IDatabase _database;
        private const string _databaseFileName = "estates.txt";

        static void Main()
        {
            _database = new TextDatabase(_databaseFileName);

            bool isRunning = true;
            do
            {
                ShowMenu();

                if (!int.TryParse(Console.ReadLine(), out int userInput))
                {
                    Console.WriteLine("Wprowadziłeś niepoprawną wartość!");
                    Console.ReadLine();
                    continue;
                }

                RunSelected(userInput, ref isRunning);

            } while (isRunning);
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("1 -> Dodaj nieruchomość");
            Console.WriteLine("2 -> Usuń nieruchomość");
            Console.WriteLine("3 -> Wyświetl nieruchomość");
            Console.WriteLine("4 -> Wyświetl wszystkie nieruchomośći");
            Console.WriteLine("0 -> Wyjdź z programu");
            Console.Write("Wybieram: ");
        }

        private static void RunSelected(int answer, ref bool isRunning)
        {
            switch (answer)
            {
                default:
                    break;
                case 1:
                    AddEstate();
                    break;
                case 2:
                    RemoveEstate();
                    break;
                case 3:
                    ShowEstate();
                    break;
                case 4:
                    ShowAllEstates();
                    break;
                case 0:
                    isRunning = false;
                    break;
            }
        }

        private static void AddEstate()
        {
            EstateManager estateManager = new EstateManager(_database);

            estateManager.Add(new Office("Słoneczna 4", 100, 150, 450000, OwnerType.City, 15, 150, DateTime.Now));
            estateManager.Add(new Office("Słoneczna 4", 100, 150, 450000, OwnerType.City, 15, 150, DateTime.Now));
            estateManager.Add(new Office("Słoneczna 4", 100, 150, 450000, OwnerType.City, 15, 150, DateTime.Now));
            estateManager.Add(new Office("Słoneczna 4", 100, 150, 450000, OwnerType.City, 15, 150, DateTime.Now));
            estateManager.Add(new Office("Słoneczna 4", 100, 150, 450000, OwnerType.City, 15, 150, DateTime.Now));

        }

        private static void RemoveEstate()
        {

        }

        private static void ShowEstate()
        {
            if (!IsEstateInDatabase(out int id))
            {
                Console.WriteLine("Brak nieruchomości o podanym numerze!");
                Console.ReadLine();
            }
            else
            {
                EstatePrinter.PrintEstate(_database.GetEstate(id));
            }
        }

        private static void ShowAllEstates()
        {
            if (_database.IsEmpty())
            {
                Console.WriteLine("Brak jakiejkolwiek nieruchomości!");
                Console.ReadLine();
            }
            else
            {
                var estates = _database.GetEstates();
                EstatePrinter.PrintEstates(estates);
            }
        }

        private static bool IsEstateInDatabase(out int userInput)
        {
            userInput = Validator.AskInteger("Podaj numer nieruchomości: ");

            var estate = _database.GetEstate(userInput);

            return estate.Value != null;
        }
    }
}
