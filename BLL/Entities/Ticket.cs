using System;

namespace BLL
{
    public class Ticket
    {
        public string NameShow { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string NameOfOwner { get; set; }

        public Ticket(string nameshow, double price, string nameOfOwner, DateTime dateTime)
        {
            NameShow = nameshow;
            Date = dateTime;
            Price = price;
            NameOfOwner = nameOfOwner;
        }
        public Ticket() { }
        public override string ToString()
        {
            return $"Name of Show = {NameShow}\n" +
                $"Name of Owner = {NameOfOwner}\n" +
                $"Date = {Date}\n" +
                $"Price = {Price}\n\n";
        }
    }
}
