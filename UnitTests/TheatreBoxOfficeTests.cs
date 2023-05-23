using BLL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
namespace UnitTests
{
    [TestFixture]
    public class TheatreBoxOfficeTests
    {
        private TheatreBoxOffice theatreBoxOffice;

        [SetUp]
        public void Setup()
        {
            theatreBoxOffice = new TheatreBoxOffice(new List<Show>(), new List<Ticket>());
        }

        [Test]
        public void AddShow_ValidEntity_ShouldAddShowToList()
        {
            // Arrange
            Show show = new Show("ShowName", "Author", "Genre", 100, DateTime.Now, 50.0);

            // Act
            theatreBoxOffice.AddShow(show);

            // Assert
            Assert.AreEqual(1, theatreBoxOffice.shows.Count);
            Assert.AreEqual(show, theatreBoxOffice.shows[0]);
        }

        [Test]
        public void AddTicket_ValidEntity_ShouldAddTicketToList()
        {
            // Arrange
            Ticket ticket = new Ticket("ShowName", 50.0, "OwnerName", DateTime.Now);

            // Act
            theatreBoxOffice.AddTicket(ticket);

            // Assert
            Assert.AreEqual(1, theatreBoxOffice.tickets.Count);
            Assert.AreEqual(ticket, theatreBoxOffice.tickets[0]);
        }

        [Test]
        public void AddShow_ValidParameters_ShouldAddShowToList()
        {
            // Arrange
            string name = "ShowName";
            string author = "Author";
            string genre = "Genre";
            int countseats = 100;
            DateTime dateTime = DateTime.Now;
            double price = 50.0;

            // Act
            theatreBoxOffice.AddShow(name, author, genre, countseats, dateTime, price);

            // Assert
            Assert.AreEqual(1, theatreBoxOffice.shows.Count);
            Assert.AreEqual(name, theatreBoxOffice.shows[0].Name);
            Assert.AreEqual(author, theatreBoxOffice.shows[0].Author);
            Assert.AreEqual(genre, theatreBoxOffice.shows[0].Genre);
            Assert.AreEqual(countseats, theatreBoxOffice.shows[0].CountSeats);
            Assert.AreEqual(dateTime, theatreBoxOffice.shows[0].Date);
            Assert.AreEqual(price, theatreBoxOffice.shows[0].Price);
        }

        [Test]
        public void DeleteShow_ValidShowNumber_ShouldRemoveShowFromList()
        {
            // Arrange
            theatreBoxOffice.shows.Add(new Show("ShowName", "Author", "Genre", 100, DateTime.Now, 50.0));
            int numshow = 0;

            // Act
            theatreBoxOffice.DeleteShow(numshow);

            // Assert
            Assert.AreEqual(0, theatreBoxOffice.shows.Count);
        }

        [Test]
        public void BuyTicket_ValidShowNumberAndOwnerName_ShouldAddTicketToListAndReturnTicketString()
        {
            // Arrange
            Show show = new Show("ShowName", "Author", "Genre", 100, DateTime.Now, 50.0);
            theatreBoxOffice.shows.Add(show);
            int countshow = 0;
            string nameofowner = "OwnerName";

            // Act
            string result = theatreBoxOffice.BuyTicket(countshow, nameofowner);

            // Assert
            Assert.AreEqual(1, theatreBoxOffice.tickets.Count);
            Assert.AreEqual(show.Name, theatreBoxOffice.tickets[0].Name);
            Assert.AreEqual(show.Price, theatreBoxOffice.tickets[0].Price);
            Assert.AreEqual(nameofowner, theatreBoxOffice.tickets[0].NameOfOwner);
            Assert.AreEqual(show.Date, theatreBoxOffice.tickets[0].Date);
            StringAssert.Contains("Name of Show = " + show.Name, result);
            StringAssert.Contains("Name of Owner = " + nameofowner, result);
            StringAssert.Contains("Date = " + show.Date.ToString(), result);
            StringAssert.Contains("Price = " + show.Price.ToString(), result);
        }

        [Test]
        public void ReturnTicket_TicketWithinReturnPeriod_ShouldNotRemoveTicketAndReturnTooLateString()
        {
            // Arrange
            Ticket ticket = new Ticket("ShowName", 50.0, "OwnerName", DateTime.Now);
            theatreBoxOffice.tickets.Add(ticket);

            // Act
            string result = theatreBoxOffice.ReturnTicket(0);

            // Assert
            Assert.AreEqual(1, theatreBoxOffice.tickets.Count);
            StringAssert.Contains("Too late to return tickets", result);
        }

        [Test]
        public void ReturnTicket_TicketOutsideReturnPeriod_ShouldRemoveTicketAndReturnPriceString()
        {
            // Arrange
            DateTime now = DateTime.Now;
            Ticket ticket = new Ticket("ShowName", 50.0, "OwnerName", now.AddDays(+3));
            theatreBoxOffice.tickets.Add(ticket);

            // Act
            string result = theatreBoxOffice.ReturnTicket(0);

            // Assert
            Assert.AreEqual(0, theatreBoxOffice.tickets.Count);
            StringAssert.Contains("Your return: " + (ticket.Price * 0.8).ToString(), result);
        }
    }

}
