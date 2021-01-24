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
            Estate toAdd = estate;

            // Prevents id duplication 
            if (ExistsOnList(estate.ID))
            {
                _database.Remove(estate.ID);
            }

            int newID = GetID();

            if (toAdd is Parcel)
            {
                var parcel = toAdd as Parcel;
                toAdd = new Parcel(newID, estate.Address, estate.Width, estate.Length, estate.Price,
                                    estate.Owner, parcel.ParcelType, estate.AddedDate);
            }
            else if (toAdd is Office)
            {
                var office = toAdd as Office;
                toAdd = new Office(newID, estate.Address, estate.Width, estate.Length, estate.Price,
                                    estate.Owner, office.Floors, office.MaxPeople, estate.AddedDate);
            }

            _database.Add(toAdd);
        }

        public bool Remove(int id)
        {
            if (!ExistsOnList(id))
                return false;

            _database.Remove(id);
            return true;
        }

        private int GetID()
        {
            var all = _database.GetEstates();

            if (all.Any())
            {
                return all.Max(item => item.ID) + 1;    // make estate with id greater than 1 from the current max id
            }
            return 1;                                   // database is empty, thus make first estate with id 1
        }

        private bool ExistsOnList(int id)
        {
            return _database.GetEstates().Any(item => item.ID == id);
        }
    }
}
