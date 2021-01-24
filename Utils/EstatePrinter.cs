using EstateManager.Estates;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManager.Utils
{
    class EstatePrinter
    {
        private static StringBuilder sb;
        private static string formatString;

        public static void PrintEstate(KeyValuePair<int, Estate> estate)
        {
            Console.Clear();

            sb = new StringBuilder();

            var e = estate.Value;

            string translatedOwner = TranslateOwner(e.Owner);

            // We want to display date without hours etc.
            string addedDate = e.AddedDate.ToShortDateString();
            string controlDate = e.ControlDate.ToShortDateString();

            formatString = "{0} -\t\t {1}\n" +
                "Własność:\t {2}\n" +
                "Wymiary:\t {3:N} m x {4:N} m\n" +
                "Powierzchnia:\t {5:N} m2\n" +
                "Cena za m2:\t {6:N} zł\n" +
                "Data dodania:\t {7}\n" +
                "Data kontroli:\t {8}\n";
            sb.AppendFormat(formatString, estate.Key, e.Address, translatedOwner, e.Length, e.Width, e.Area, e.PricePerMeter, addedDate, controlDate);
            Console.Write(sb);

            foreach (var item in e.AdditionalInfo())
            {
                Console.Write(item);
            }
            Console.ReadLine();
        }

        public static void PrintEstates(IEnumerable<KeyValuePair<int, Estate>> estates)
        {
            Console.Clear();

            sb = new StringBuilder();

            formatString = "{0} -\t\t {1} ({2})\n" +
                "Powierzchnia:\t {3:N} m2\n" +
                "Cena:\t\t {4:N} zł\n" +
                "=======================\n";

            foreach (var estate in estates)
            {
                var e = estate.Value;
                string translatedOwner = TranslateOwner(e.Owner);

                sb.AppendFormat(formatString, estate.Key, e.Address, translatedOwner, e.Area, e.Price);
            }

            Console.WriteLine(sb);
            Console.ReadLine();
        }

        private static string TranslateOwner(OwnerType owner)
        {
            return owner switch
            {
                OwnerType.City => "Miejska",
                OwnerType.Private => "Prywatna",
                _ => "Inna",
            };
        }
    }
}
