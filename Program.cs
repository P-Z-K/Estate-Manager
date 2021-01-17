using EstateManager.Estates;
using EstateManager.Utils;
using System;

// TODO: Need kind of manager class to manage estates' ids
// TODO: Need to implement functionality to functions in Program class
// TODO: May we should add some sort of validator class that ensures custom validate

namespace EstateManager
{
    class Program
    {
        static void Main()
        {
            //EstatePrinter.PrintEstate(new Office(1, "Słoneczna 4", 100, 150, 450000, OwnerType.City, 15, 150, DateTime.Now));
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

        }

        private static void RemoveEstate()
        {

        }

        private static void ShowEstate()
        {

        }

        private static void ShowAllEstates()
        {

        }
    }
}
