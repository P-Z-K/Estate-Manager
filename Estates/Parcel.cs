using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManager.Estates
{
    class Parcel : Estate
    {
        public ParcelType ParcelType { get; private set; }

        public Parcel(int number, string adress, double width, double length, double price, OwnerType owner, ParcelType parcelType, DateTime addedDate)
            : base(number, adress, width, length, price, owner, addedDate)
        {
            ParcelType = parcelType;
        }

        public Parcel(string adress, double width, double length, double price, OwnerType owner, ParcelType parcelType, DateTime addedDate)
            : base(adress, width, length, price, owner, addedDate)
        {
            ParcelType = parcelType;
        }

        public override IEnumerable<string> AdditionalInfo()
        {
            string parcelType = ParcelType == ParcelType.BuildingLand ? "Budowlana" : "Rolna";
            return new List<string>() { "Rodzaj działki:\t " + parcelType + "\n" };
        }
    }
}
