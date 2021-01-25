using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManager.Estates
{
    class Office : Estate
    {
        public int Floors { get; private set; }
        public int MaxPeople { get; private set; }

        public Office(string adress, decimal width, decimal length, decimal price, OwnerType owner, int floors, int maxPeople, DateTime addedDate)
            : base(adress, width, length, price, owner, addedDate)
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
