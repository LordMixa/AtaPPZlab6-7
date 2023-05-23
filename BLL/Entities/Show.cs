using System;

namespace BLL
{
    public class Show:IEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int CountSeats { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public Show() { }
        public Show(string name, string author, string genre, int countseats, DateTime date, double price)
        {
            Name = name;
            Author = author;
            Genre = genre;
            CountSeats = countseats;
            Date = date;
            Price = price;
        }
        public override string ToString()
        {
            return $"Name = {Name}\n" +
                $"Author = {Author}\n" +
                $"Genre = {Genre}\n" +
                $"Count of Seats = {CountSeats}\n" +
                $"Date = {Date}\n" +
                $"Price = {Price}\n\n";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Show other = (Show)obj;
            return Name == other.Name &&
                   Author == other.Author &&
                   Genre == other.Genre &&
                   CountSeats == other.CountSeats &&
                   Date == other.Date &&
                   Price == other.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Author, Genre, CountSeats, Date, Price);
        }
    }
}
