﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManager.Estates
{
    class Parcel : Estate
    {
        public ParcelType ParcelType { get; private set; }

        public Parcel(string adress, decimal width, decimal length, decimal price, OwnerType owner, ParcelType parcelType, DateTime addedDate)
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
