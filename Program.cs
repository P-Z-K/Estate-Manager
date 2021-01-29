using EstateManager.Data;
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
            Console.WriteLine("Jaki rodzaj nieruchomości wybierasz");
            Console.WriteLine("1. Biuro");
            Console.WriteLine("2. Działka");

            int userInput = Validator.AskInteger("Wybieram: ");

            Estate estate;

            switch (userInput)
            {
                default:
                    estate = null;
                    break;
                case 1:
                    estate = GetOffice();
                    break;
                case 2:
                    estate = GetParcel();
                    break;
            }

            if (estate != null)
            {
                EstateManager manager = new EstateManager(_database);

                manager.Add(estate);
                Console.WriteLine("Pomyślnie dodano nieruchomość!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Wprowadzona odpowiedź jest niepoprawna, nastąpi powrót do głównego menu");
                Console.ReadLine();
            }

        }

        private static Parcel GetParcel()
        {
            GetEstateBasicInfo(out string address, out decimal width, out decimal length, out decimal price, out OwnerType owner);

            ParcelType parcelType = Validator.AskParcelType("Rodzaj działki (R - Rolna; B - Budowlana): ");

            return new Parcel(address, width, length, price, owner, parcelType, DateTime.Now);
        }

        private static Office GetOffice()
        {
            GetEstateBasicInfo(out string address, out decimal width, out decimal length, out decimal price, out OwnerType owner);

            int floors = Validator.AskInteger("Ilość pięter: ");
            int maxPeople = Validator.AskInteger("Maksymalna ilość osób: ");

            return new Office(address, width, length, price, owner, floors, maxPeople, DateTime.Now);
        }


        private static void GetEstateBasicInfo(out string address, out decimal width, out decimal length, out decimal price, out OwnerType owner)
        {
            Console.Clear();

            address = Validator.AskAddress("Adres: ");

            Console.WriteLine("Wymiary nieruchomości");
            width = Validator.AskDecimal("Szerokość: ");
            length = Validator.AskDecimal("Długość: ");
            price = Validator.AskDecimal("Cena: ");
            owner = Validator.AskOwner("Własność (P - Prywatna; M - Miejska, O - Inna): ");
        }

        private static void RemoveEstate()
        {
            if (!IsEstateInDatabase(out int id))
            {
                Console.WriteLine("Brak nieruchomości o podanym numerze!");
                Console.ReadLine();
            }
            else
            {
                _database.Remove(id);
                Console.WriteLine("Usunięcie nieruchomości zakończone powodzeniem!");
                Console.ReadLine();
            }
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
                EstatePrinter.PrintEstates(_database.GetEstates());
            }
        }

        private static bool IsEstateInDatabase(out int userInput)
        {
            userInput = Validator.AskInteger("Podaj numer nieruchomości: ");

            EstateManager manager = new EstateManager(_database); 

            return manager.IsInDatabase(userInput);
        }
    }
}
