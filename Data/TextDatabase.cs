using EstateManager.Estates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EstateManager.Data
{
    class TextDatabase : IDatabase
    {
        private readonly string _fileName;

        private List<Estate> _estates;

        public TextDatabase(string fileName)
        {
            _fileName = fileName;

            if (!File.Exists(_fileName))
                File.Create(_fileName).Dispose();


        }

        public void Add(Estate estate)
        {
            _estates.Add(estate);
        }

        public Estate GetEstate(int id)
        {
            return _estates.SingleOrDefault(item => item.ID == id);
        }

        public IEnumerable<Estate> GetEstates()
        {
            return _estates;
        }

        public bool IsEmpty()
        {
            return _estates.Count <= 0;
        }

        public void Remove(int id)
        {
            _estates.Remove(_estates.Find(item => item.ID == id));
        }

        private void UpdateData()
        {
            // TODO: Need to implement data updating
        }

        private void LoadData()
        {
            _estates = new List<Estate>();

            var lines = File.ReadAllLines(_fileName);

            
            foreach (var line in lines)
            {
                var splitted = line.Split('|');

                // TODO: Need to diffrentiate estate between parcel and office
            }
            
        }
    }
}
