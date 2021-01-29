using EstateManager.Data;
using EstateManager.Estates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstateManager
{
    class EstateManager
    {
        private readonly IDatabase _database;

        public EstateManager(IDatabase database)
        {
            _database = database;
        }

        public void Add(Estate estate)
        {
            int newID = GetID();

            if (estate is Parcel)
            {
                var parcel = estate as Parcel;
                estate = new Parcel(estate.Address, estate.Width, estate.Length, estate.Price,
                                    estate.Owner, parcel.ParcelType, estate.AddedDate);
            }
            else if (estate is Office)
            {
                var office = estate as Office;
                estate = new Office(estate.Address, estate.Width, estate.Length, estate.Price,
                                    estate.Owner, office.Floors, office.MaxPeople, estate.AddedDate);
            }

            _database.Add(newID, estate);
        }

        public bool IsInDatabase(int id)
        {
            return _database.GetEstates().Any(item => item.Key == id);
        }

        private int GetID()
        {
            var all = _database.GetEstates();

            if (all.Any())
            {
                return all.Max(item => item.Key) + 1;    // make estate with id greater than 1 from the current max id
            }
            return 1;                                   // database is empty, thus make first estate with id 1
        }
    }
}
