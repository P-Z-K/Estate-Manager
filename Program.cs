using EstateManager.Estates;
using EstateManager.Utils;
using System;

namespace EstateManager
{
    class Program
    {
        static void Main()
        {
            EstatePrinter.PrintEstate(new Office(1, "Słoneczna 4", 100, 150, 450000, OwnerType.City, 15, 150, DateTime.Now));
        }
    }
}
