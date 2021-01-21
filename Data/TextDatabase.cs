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

        private List<Estate> _estates;

        public TextDatabase(string fileName)
        {
            _fileName = fileName;

            if (!File.Exists(_fileName))
                File.Create(_fileName).Dispose();


            LoadData();
        }

        public void Add(Estate estate)
        {
            _estates.Add(estate);
            _estates.Sort((x, y) => x.ID.CompareTo(y.ID));
            UpdateData();
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
            UpdateData();
        }

        private void UpdateData()
        {
            var lines = new List<string>();

            foreach (var estate in _estates)
            {
                if (estate is Office)
                {
                    var office = estate as Office;
                    lines.Add($"{office.ID}|OFFICE|{office.Adress}|{office.Owner}|{office.Length}|{office.Width}|{office.Price}" +
                        $"|{office.Floors}|{office.MaxPeople}|{office.AddedDate.ToString("dd-MM-yyyy")}");
                }
                else if (estate is Parcel)
                {
                    var parcel = estate as Parcel;
                    lines.Add($"{parcel.ID}|PARCEL|{parcel.ParcelType}|{parcel.Adress}|{parcel.Owner}|{parcel.Length}|{parcel.Width}|{parcel.Price}" +
                        $"|{parcel.AddedDate.ToString("dd-MM-yyyy")}");
                }
            }

            File.WriteAllLines(_fileName, lines);
        }

        private void LoadData()
        {
            _estates = new List<Estate>();

            var lines = File.ReadAllLines(_fileName);

            
            foreach (var line in lines)
            {
                var splitted = line.Split('|');

                if (splitted[1] == "OFFICE")
                {
                    Add(LoadOffice(splitted));
                }
                else if (splitted[1] == "PARCEL")
                {
                    Add(LoadParcel(splitted));
                }
            }
            
        }

        private Office LoadOffice(string[] line)
        {
            var ID = int.Parse(line[0]);
            var address = line[2];
            var owner = Enum.Parse<OwnerType>(line[3]);
            var length = double.Parse(line[4]);
            var width = double.Parse(line[5]);
            var price = double.Parse(line[6]);
            var floors = int.Parse(line[7]);
            var maxPeople = int.Parse(line[8]);
            var addedDate = DateTime.ParseExact(line[9], "dd-MM-yyyy", null);

            return new Office(ID, address, width, length, price, owner, floors, maxPeople, addedDate);
        }

        private Parcel LoadParcel(string[] line)
        {
            var ID = int.Parse(line[0]);
            var parcelType = Enum.Parse<ParcelType>(line[2]);
            var address = line[3];
            var owner = Enum.Parse<OwnerType>(line[4]);
            var length = double.Parse(line[5]);
            var width = double.Parse(line[6]);
            var price = double.Parse(line[7]);
            var addedDate = DateTime.ParseExact(line[8], "dd-MM-yyyy", null);

            return new Parcel(ID, address, width, length, price, owner, parcelType, addedDate);
        }
    }
}
