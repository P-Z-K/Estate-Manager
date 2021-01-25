using EstateManager.Estates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EstateManager.Data
{
    class TextDatabase : IDatabase
    {
        private readonly string _fileName;

        private SortedDictionary<int, Estate> _dir;

        public TextDatabase(string fileName)
        {
            _fileName = fileName;

            if (!File.Exists(_fileName))
                File.Create(_fileName).Dispose();

            


            LoadData();
        }

        public void Add(int key, Estate value)
        {
            _dir.Add(key, value);

            UpdateData();
        }

        public KeyValuePair<int, Estate> GetEstate(int id)
        {      
            return _dir.SingleOrDefault(item => item.Key == id);
        }

        public IEnumerable<KeyValuePair<int, Estate>> GetEstates()
        {
            return _dir;
        }

        public bool IsEmpty()
        {
            return _dir.Count <= 0;
        }

        public void Remove(int id)
        {
            _dir.Remove(id);
            UpdateData();
        }

        private void UpdateData()
        {
            var lines = new List<string>();

            foreach (var pair in _dir)
            {
                if (pair.Value is Office)
                {
                    var office = pair.Value as Office;
                    lines.Add($"{pair.Key}|OFFICE|{office.Address}|{office.Owner}|{office.Length}|{office.Width}|{office.Price}" +
                        $"|{office.Floors}|{office.MaxPeople}|{office.AddedDate.ToString("dd-MM-yyyy")}");
                }
                else if (pair.Value is Parcel)
                {
                    var parcel = pair.Value as Parcel;
                    lines.Add($"{pair.Key}|PARCEL|{parcel.ParcelType}|{parcel.Address}|{parcel.Owner}|{parcel.Length}|{parcel.Width}|{parcel.Price}" +
                        $"|{parcel.AddedDate.ToString("dd-MM-yyyy")}");
                }
            }

            File.WriteAllLines(_fileName, lines);
        }

        private void LoadData()
        {
            _dir = new SortedDictionary<int, Estate>();

            var lines = File.ReadAllLines(_fileName);

            
            foreach (var line in lines)
            {
                var splitted = line.Split('|');

                int id = int.Parse( splitted[0]);

                if (splitted[1] == "OFFICE")
                {
                    Add(id, LoadOffice(splitted));
                }
                else if (splitted[1] == "PARCEL")
                {
                    Add(id, LoadParcel(splitted));
                }
            }
            
        }

        private Office LoadOffice(string[] line)
        {
            var address = line[2];
            var owner = Enum.Parse<OwnerType>(line[3]);
            var length = decimal.Parse(line[4]);
            var width = decimal.Parse(line[5]);
            var price = decimal.Parse(line[6]);
            var floors = int.Parse(line[7]);
            var maxPeople = int.Parse(line[8]);
            var addedDate = DateTime.ParseExact(line[9], "dd-MM-yyyy", null);

            return new Office(address, width, length, price, owner, floors, maxPeople, addedDate);
        }

        private Parcel LoadParcel(string[] line)
        {
            var parcelType = Enum.Parse<ParcelType>(line[2]);
            var address = line[3];
            var owner = Enum.Parse<OwnerType>(line[4]);
            var length = decimal.Parse(line[5]);
            var width = decimal.Parse(line[6]);
            var price = decimal.Parse(line[7]);
            var addedDate = DateTime.ParseExact(line[8], "dd-MM-yyyy", null);

            return new Parcel(address, width, length, price, owner, parcelType, addedDate);
        }
    }
}
