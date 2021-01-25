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

        public KeyValuePair<int, Estate> GetEstate(int id)
        {
            return _database.GetEstate(id);
        }

        public IEnumerable<KeyValuePair<int, Estate>> GetEstates()
        {
            return _database.GetEstates();
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

        public bool Remove(int id)
        {
            if (!IsInDatabase(id))
                return false;

            _database.Remove(id);
            return true;
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

        public bool IsInDatabase(int id)
        {
            return _database.GetEstates().Any(item => item.Key == id);
        }

        public bool IsDatabaseEmpty()
        {
            return _database.IsEmpty();
        }
    }
}
