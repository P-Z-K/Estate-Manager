using EstateManager.Estates;
using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManager.Data
{
    interface IDatabase
    {
        void Add(Estate estate);
        void Remove(int id);
        Estate GetEstate(int id);
        IEnumerable<Estate> GetEstates();
        bool IsEmpty();
    }
}
