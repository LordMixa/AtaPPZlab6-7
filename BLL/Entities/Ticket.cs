using System;

namespace BLL
{
    public class Ticket : IEntity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string NameOfOwner { get; set; }

        public Ticket(string nameshow, double price, string nameOfOwner, DateTime dateTime)
        {
            Name = nameshow;
            Date = dateTime;
            Price = price;
            NameOfOwner = nameOfOwner;
        }
        public Ticket() { }
        public override string ToString()
        {
            return $"Name of Show = {Name}\n" +
                $"Name of Owner = {NameOfOwner}\n" +
                $"Date = {Date}\n" +
                $"Price = {Price}\n\n";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Ticket other = (Ticket)obj;
            return Name == other.Name &&
                   Date == other.Date &&
                   Price == other.Price &&
                   NameOfOwner == other.NameOfOwner;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Date, Price, NameOfOwner);
        }
    }
}
