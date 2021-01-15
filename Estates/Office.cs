using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManager.Estates
{
    class Office : Estate
    {
        public int Floors { get; private set; }
        public int MaxPeople { get; private set; }

        public Office(int number, string adress, double width, double length, double price, OwnerType owner, int floors, int maxPeople, DateTime addedDate)
            : base(number, adress, width, length, price, owner, addedDate)
        {
            Floors = floors;
            MaxPeople = maxPeople;
        }

        public Office(string adress, double width, double length, double price, OwnerType owner, int floors, int maxPeople)
            : base(adress, width, length, price, owner)
        {
            Floors = floors;
            MaxPeople = maxPeople;
        }

        public override IEnumerable<string> AdditionalInfo()
        {
            string floors = "Liczba pięter:\t " + Floors + "\n";
            string maxPeople = "Maksymalna liczba osób:\t " + MaxPeople + "\n";
            return new List<string>() { floors, maxPeople };
        }
    }
}
