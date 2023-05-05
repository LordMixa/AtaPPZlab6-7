using System;

namespace BLL
{
    public class Show
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int CountSeats { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
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
    }
}
