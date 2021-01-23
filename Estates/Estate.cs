using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManager.Estates
{
    abstract class Estate
    {
        public int ID { get; private set; }
        public string Address { get; private set; }
        public double Width { get; private set; }
        public double Length { get; private set; }
        public double Price { get; private set; }
        public OwnerType Owner { get; private set; }
        public DateTime AddedDate { get; private set; }
        public DateTime ControlDate { get; private set; }

        // Every how many years the estate must pass inspection
        private const int controlFrequency = 3;

        public double Area
        {
            get
            {
                return Width * Length;
            }
        }

        public double PricePerMeter
        {
            get
            {
                return Price / Area;
            }
        }

        public abstract IEnumerable<string> AdditionalInfo();

        public Estate(int id, string adress, double width, double length, double price, OwnerType owner, DateTime addedDate)
        {
            ID = id;
            Address = adress;
            Width = width;
            Length = length;
            Price = price;
            AddedDate = addedDate;
            ControlDate = AddedDate.AddYears(controlFrequency);
            Owner = owner;
        }

        public Estate(int id, string adress, double width, double length, double price, OwnerType owner)
        {
            ID = id;
            Address = adress;
            Width = width;
            Length = length;
            Price = price;
            AddedDate = DateTime.Now;
            ControlDate = AddedDate.AddYears(controlFrequency);
            Owner = owner;
        }

        public Estate(string adress, double width, double length, double price, OwnerType owner, DateTime addedDate)
        {
            ID = 0;
            Address = adress;
            Width = width;
            Length = length;
            Price = price;
            AddedDate = addedDate;
            ControlDate = AddedDate.AddYears(controlFrequency);
            Owner = owner;
        }

        protected bool IsControlDeadlinePassed()
        {
            int result = DateTime.Compare(AddedDate, ControlDate);
            return result > 0;
        }
    }
}
