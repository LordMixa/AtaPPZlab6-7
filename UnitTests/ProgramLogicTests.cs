using BLL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace UnitTests
{
    [TestFixture]
    public class ProgramLogicTests
    {
        private ProgramLogic programLogic;
        private Mock<IService> showServiceMock;
        private Mock<IService> ticketServiceMock;
        private Mock<TheatreBoxOffice> theatreBoxMock;
        [SetUp]
        public void Setup()
        {
            showServiceMock = new Mock<IService>();
            ticketServiceMock = new Mock<IService>();
            theatreBoxMock = new Mock<TheatreBoxOffice>(new List<Show> {new Show("TestN","TestA","TestG",3, new DateTime(2023, 05, 22), 100) }, new List<Ticket> { });

            programLogic = new ProgramLogic(showServiceMock.Object, ticketServiceMock.Object, theatreBoxMock.Object);
        }
        [Test]
        public void AddShow_ValidParameters_ShouldAddShow()
        {
            string name = "ShowName";
            string author = "Author";
            string genre = "Genre";
            int countseats = 100;
            DateTime dateTime = DateTime.Now;
            double price = 50.0;

            programLogic.AddShow(name, author, genre, countseats, dateTime, price);

            showServiceMock.Verify(mock => mock.AddEntity(It.IsAny<Show>()), Times.Once);
        }

        [Test]
        public void AddTicket_ValidParametersAndAvailableSeats_ShouldAddTicket()
        {
            int countshow = 1;
            string nameofowner = "OwnerName";

            string result = programLogic.AddTicket(countshow, nameofowner);

            Assert.AreEqual("You have successfully purchased a ticket", result);
            ticketServiceMock.Verify(mock => mock.AddEntity(It.IsAny<Ticket>()), Times.Once);
        }

        [Test]
        public void AddTicket_ValidParametersAndNoAvailableSeats_ShouldReturnErrorMessage()
        {
            int countshow = 2;
            string nameofowner = "OwnerName";

            programLogic.theatreBox.AddShow("ShowName", "Author", "Genre", 0, DateTime.Now, 50.0);

            string result = programLogic.AddTicket(countshow, nameofowner);

            Assert.AreEqual("I'm sorry but there are no available seats for this show", result);
        }
        [Test]
        public void DeleteShow_ValidShowNumber_ShouldDeleteShow()
        {
            // Arrange
            int numshow = 1;
            string showName = "TestN";
            string showAuthor = "TestA";
            programLogic.AddTicket(numshow, "TestN1");
            programLogic.AddTicket(numshow, "TestN2");
            // Act
            string result = programLogic.DeleteShow(numshow);

            // Assert
            Assert.AreEqual("Show was deleted", result);
            showServiceMock.Verify(s => s.DeleteEntity(showName, showAuthor), Times.Once);
            ticketServiceMock.Verify(t => t.DeleteEntity(showName, It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void DeleteTicket_ValidParameters_ShouldDeleteTicket()
        {
            int numshow = 2;
            string nameofowner = "OwnerName";
            string showName = "TestN";

            programLogic.AddShow(showName, "TestA", "TestG", 10, new DateTime(2023, 05, 30), 100);
            programLogic.AddTicket(numshow , nameofowner);
            string result = programLogic.DeleteTicket(numshow, nameofowner);
             
            Assert.AreEqual("Your return: 80", result);
            ticketServiceMock.Verify(t => t.DeleteEntity(showName, It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void DeleteTicket_TooLateToReturnTicket_ShouldReturnErrorMessage()
        {
            int numshow = 1;
            string nameofowner = "OwnerName";
            string showName = "TestN";

            programLogic.AddTicket(numshow, nameofowner);
            string result = programLogic.DeleteTicket(numshow, nameofowner);

            Assert.AreEqual("Too late to return tickets", result);
            ticketServiceMock.Verify(t => t.DeleteEntity(showName, It.IsAny<string>()), Times.Exactly(0));
        }
        [Test]
        public void DeleteTicket_Youhaventtickets_ShouldReturnErrorMessage()
        {
            int numshow = 1;
            string nameofowner = "OwnerName";
            string showName = "TestN";

            programLogic.AddTicket(numshow, nameofowner);
            string result = programLogic.DeleteTicket(numshow, "TestOwner");

            Assert.AreEqual("You haven`t tickets on this show", result);
            ticketServiceMock.Verify(t => t.DeleteEntity(showName, It.IsAny<string>()), Times.Exactly(0));
        }
        [Test]
        public void GetShows_ShouldReturnListOfShowStrings()
        {
            // Arrange
            var expectedShows = new List<string> { "Name = TestN\nAuthor = TestA\nGenre = TestG\nCount of Seats = 3\nDate = 22.05.2023 00:00:00\nPrice = 100\n" };

            // Act
            var shows = programLogic.GetShows();

            // Assert
            Assert.AreEqual(expectedShows.Count, shows.Count);
            for (int i = 0; i < expectedShows.Count; i++)
            {
                Assert.AreEqual(expectedShows[i].Trim(), shows[i].Trim());
            }
        }
        [Test]
        public void GetTickets_ShouldReturnListOfTicketStrings()
        {
            programLogic.AddTicket(1, "Owner1");
            // Arrange
            var expectedShows = new List<string> { "Name of Show = TestN\nName of Owner = Owner1\nDate = 22.05.2023 00:00:00\nPrice = 100\n" };

            // Act
            var shows = programLogic.GetTickets();

            // Assert
            Assert.AreEqual(expectedShows.Count, shows.Count);
            for (int i = 0; i < expectedShows.Count; i++)
            {
                Assert.AreEqual(expectedShows[i].Trim(), shows[i].Trim());
            }
        }
        [Test]
        public void GetTickets_ShouldReturnListOfTicketStringsById()
        {
            programLogic.AddTicket(1, "Owner1");
            // Arrange
            var expectedShows = new List<string> { "Name of Show = TestN\nName of Owner = Owner1\nDate = 22.05.2023 00:00:00\nPrice = 100\n" };

            // Act
            var shows = programLogic.GetTickets(1);

            // Assert
            Assert.AreEqual(expectedShows.Count, shows.Count);
            for (int i = 0; i < expectedShows.Count; i++)
            {
                Assert.AreEqual(expectedShows[i].Trim(), shows[i].Trim());
            }
        }
        [Test]
        public void GetEShows_ShouldReturnListOfShows()
        {
            // Arrange
            var expectedShows = new List<Show>
            {
                new Show("TestN", "TestA", "TestG", 3, new DateTime(2023, 05, 22), 100)
            };

            // Act
            var shows = programLogic.GetEShows();

            // Assert
            Assert.IsTrue(expectedShows.SequenceEqual(shows));
        }
        [Test]
        public void GetETickets_ShouldReturnListOfTickets()
        {
            programLogic.AddTicket(1, "Owner1");
            // Arrange
            var expectedTickets = new List<Ticket>
            {
                 new Ticket("TestN",100, "Owner1", new DateTime(2023, 05, 22))
            };

            // Act
            var tickets = programLogic.GetETickets();

            // Assert
            Assert.IsTrue(expectedTickets.SequenceEqual(tickets));
        }
        [Test]
        public void CheckSoldTickets_ShouldReturnCorrectCountOfSoldTickets()
        {
            // Arrange
            var theatreBoxMock = new Mock<TheatreBoxOffice>(new List<Show> { new Show("TestN", "TestA", "TestG", 3, DateTime.Now, 100) },
                                                          new List<Ticket> { new Ticket("TestN",12, "Owner1", DateTime.Now), new Ticket("TestN", 12, "Owner2", DateTime.Now) });

            var programLogic = new ProgramLogic(showServiceMock.Object, ticketServiceMock.Object, theatreBoxMock.Object);

            int numshow = 1;
            int expectedSoldCount = 2;

            // Act
            string result = programLogic.CheckSoldTickets(numshow);

            // Assert
            Assert.AreEqual($"This show has sold {expectedSoldCount} tickets", result);
        }
        [Test]
        public void UpdateShow_ValidParameters_ShouldUpdateShowAndRemoveFromTheatreBox()
        {
            // Arrange
            Show updatedShow = new Show("UpdatedName", "UpdatedAuthor", "UpdatedGenre", 10, DateTime.Now, 100);
            int id = 1;
            string name = programLogic.theatreBox.shows[0].Name;
            // Act
            Show oldshow = programLogic.theatreBox.shows[0];
            programLogic.UpdateShow( updatedShow, id);
            id--;
            // Assert
            showServiceMock.Verify(mock => mock.UpdateEntity(name, updatedShow), Times.Once);
            Assert.IsFalse(theatreBoxMock.Object.shows.Contains(oldshow));
            Assert.IsTrue(theatreBoxMock.Object.shows.Contains(updatedShow));
        }
        [Test]
        public void UpdateTicket_ValidParameters_ShouldUpdateTicketAndRemoveFromTheatreBox()
        {
            programLogic.AddTicket(1, "OldOwner");
            // Arrange
            Ticket updatedTicket = new Ticket("TestN", 100, "NewOwner", new DateTime(2023, 05, 22));
            int id = 1;
            string name = programLogic.theatreBox.tickets[0].NameOfOwner;
            // Act
            Ticket oldticket = programLogic.theatreBox.tickets[0];
            programLogic.UpdateTicket(id,1,"NewOwner");
            id--;
            // Assert
            ticketServiceMock.Verify(mock => mock.UpdateEntity(name, updatedTicket), Times.Once);
            Assert.IsFalse(theatreBoxMock.Object.tickets.Contains(oldticket));
            Assert.IsTrue(theatreBoxMock.Object.tickets.Contains(updatedTicket));
        }
        [TearDown]
        public void TearDown()
        {
            showServiceMock.Verify();
            ticketServiceMock.Verify();
        }
    }
}
