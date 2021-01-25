using System;
using System.Collections.Generic;
using System.Text;

namespace EstateManager.Estates
{
    abstract class Estate
    {
        public string Address { get; private set; }
        public decimal Width { get; private set; }
        public decimal Length { get; private set; }
        public decimal Price { get; private set; }
        public OwnerType Owner { get; private set; }
        public DateTime AddedDate { get; private set; }
        public DateTime ControlDate { get; private set; }

        // Every how many years the estate must pass inspection
        private const int controlFrequency = 3;

        public decimal Area
        {
            get
            {
                return Width * Length;
            }
        }

        public decimal PricePerMeter
        {
            get
            {
                return Price / Area;
            }
        }

        public abstract IEnumerable<string> AdditionalInfo();

        public Estate(string adress, decimal width, decimal length, decimal price, OwnerType owner, DateTime addedDate)
        {
            Address = adress;
            Width = width;
            Length = length;
            Price = price;
            AddedDate = addedDate;
            ControlDate = AddedDate.AddYears(controlFrequency);
            Owner = owner;
        }

        public Estate(string adress, decimal width, decimal length, decimal price, OwnerType owner)
        {
            Address = adress;
            Width = width;
            Length = length;
            Price = price;
            AddedDate = DateTime.Now;
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
