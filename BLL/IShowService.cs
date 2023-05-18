using DAL.DBEntities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IShowService
    {
        void AddShow(Show show);
        void AddTicket(Ticket ticket);
        void DeleteShow(int id);
        void DeleteShow(string name1, string name2);
        void DeleteTicket(int id);
        void DeleteTicket(string name1, string name2);
        void UpdateShow(string name1, Show show);
        void UpdateTicket(string name1, Ticket ticket);
        IEnumerable<Show> GetShows();
        IEnumerable<Ticket> GetTickets();
    }
}
