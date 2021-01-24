using EstateManager.Estates;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManager.Data
{
    interface IDatabase
    {
        void Add(int id, Estate estate);
        void Remove(int id);
        KeyValuePair<int, Estate> GetEstate(int id);
        IEnumerable<KeyValuePair<int, Estate>> GetEstates();
        bool IsEmpty();
    }
}
