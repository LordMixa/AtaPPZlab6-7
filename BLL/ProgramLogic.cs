using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Metadata;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public class ProgramLogic
    {
        public IService showService;
        public IService ticketService;
        public TheatreBoxOffice theatreBox;

        public ProgramLogic()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMappingProfile());
            });
            var mapper = config.CreateMapper();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(mapper)
                .BuildServiceProvider();
            showService = new ShowService(serviceProvider.GetService<IMapper>());
            ticketService = new TicketService(serviceProvider.GetService<IMapper>());
            var shows = showService.GetEntity();
            var tickets = ticketService.GetEntity();
            List<Show> lshows = (List<Show>)shows;
            List<Ticket> ltickets = (List<Ticket>)tickets;
            theatreBox = new TheatreBoxOffice(lshows, ltickets);
        }
        public ProgramLogic(IService showService, IService ticketService, TheatreBoxOffice theatreBox)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMappingProfile());
            });
            var mapper = config.CreateMapper();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(mapper)
                .BuildServiceProvider();
            this.showService = showService;
            this.ticketService = ticketService;
            this.theatreBox = theatreBox;
        }
        public void AddShow(string name, string author, string genre, int countseats, DateTime dateTime, double price)
        {
            theatreBox.AddShow(name, author, genre, countseats, dateTime, price);
            showService.AddEntity(theatreBox.shows.Last());
        }
        public string AddTicket(int countshow, string nameofowner)
        {
            if (CheckFreeSeats(countshow))
            {
                theatreBox.BuyTicket(--countshow, nameofowner);
                ticketService.AddEntity(theatreBox.tickets.Last());
                return "You have successfully purchased a ticket";
            }
            else
                return "I'm sorry but there are no available seats for this show";
        }
        public string DeleteShow(int numshow)
        {
            string nameshow = theatreBox.shows[--numshow].Name;
            showService.DeleteEntity(theatreBox.shows[numshow].Name, theatreBox.shows[numshow].Author);
            theatreBox.DeleteShow(numshow);
            for (int i = 0; i < theatreBox.tickets.Count; i++)
            {
                if (theatreBox.tickets[i].Name == nameshow)
                    ticketService.DeleteEntity(theatreBox.tickets[i].Name, theatreBox.tickets[i].NameOfOwner);
            }
            for (int i = 0; i < theatreBox.tickets.Count; i++)
            {
                if (theatreBox.tickets[i].Name == nameshow)
                    theatreBox.tickets.Remove(theatreBox.tickets[i]);
            }
            return "Show was deleted";
        }
        public string DeleteTicket(int numshow, string nameofowner)
        {
            if (numshow <= theatreBox.shows.Count)
            {
                --numshow;
                for (int i = 0; i < theatreBox.tickets.Count; i++)
                {
                    if (theatreBox.tickets[i].Name == theatreBox.shows[numshow].Name && theatreBox.tickets[i].NameOfOwner == nameofowner)
                    {
                        string check = theatreBox.ReturnTicket(i);
                        if (check == "Too late to return tickets")
                            return check;
                        else
                        {
                            ticketService.DeleteEntity(theatreBox.shows[numshow].Name, nameofowner);
                            return check;
                        }
                    }
                }
                return "You haven`t tickets on this show";
            }else
                return "Incorrect ID";
        }
        public string CheckSoldTickets(int numshow)
        {
            --numshow;
            int countsold = 0;
            for (int i = 0; i < theatreBox.tickets.Count; i++)
            {
                if (theatreBox.tickets[i].Name == theatreBox.shows[numshow].Name)
                    countsold++;
            }
            return $"This show has sold {countsold} tickets";
        }
        public List<string> GetShows()
        {
            List<string> shows = new List<string>();
            for (int i = 0; i < theatreBox.shows.Count; i++)
                shows.Add(theatreBox.shows[i].ToString());
            return shows;
        }
        public List<Show> GetEShows()
        {
            return theatreBox.shows;
        }
        public List<Ticket> GetETickets()
        {
            return theatreBox.tickets;
        }
        public void UpdateShow(IEntity entity,int id)
        {
            showService.UpdateEntity(theatreBox.shows[--id].Name, entity);
            theatreBox.shows.Remove(theatreBox.shows[id]);
            theatreBox.AddShow(entity);
        }
        public void UpdateTicket(int idticket,int idshow,string nameown)
        {
            string nameowner = theatreBox.tickets[--idticket].NameOfOwner;
            theatreBox.tickets.Remove(theatreBox.tickets[idticket]);
            Ticket ticket = new Ticket(theatreBox.shows[--idshow].Name, theatreBox.shows[idshow].Price, nameown, theatreBox.shows[idshow].Date);
            ticketService.UpdateEntity(nameowner,ticket);
            theatreBox.AddTicket(ticket);
        }
        public List<string> GetTickets()
        {
            List<string> tickets = new List<string>();
            for (int i = 0; i < theatreBox.tickets.Count; i++)
                tickets.Add(theatreBox.tickets[i].ToString());
            return tickets;
        }
        public List<string> GetTickets(int nameshow)
        {
            --nameshow;
            List<string> tickets = new List<string>();
            for (int i = 0; i < theatreBox.tickets.Count; i++)
            {
                if (theatreBox.tickets[i].Name == theatreBox.shows[nameshow].Name)
                    tickets.Add(theatreBox.tickets[i].ToString());
            }
            return tickets;
        }
        public bool CheckFreeSeats(int numshow)
        {
            int countsold = 0;
            int countseat = theatreBox.shows[--numshow].CountSeats;
            for (int i = 0; i < theatreBox.tickets.Count; i++)
            {
                if (theatreBox.tickets[i].Name == theatreBox.shows[numshow].Name)
                    countsold++;
            }
            if (countsold < countseat)
                return true;
            else
                return false;
        }
    }
}