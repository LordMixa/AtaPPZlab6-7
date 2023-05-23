using AutoMapper;
using DAL.DBEntities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TicketService:IService
    {
        private readonly IMapper _mapper;
        public TicketService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void AddEntity(IEntity ticket)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ticketRepository = new TicketRepository(unitOfWork);
                var dbticket = _mapper.Map<DBTicket>(ticket);
                ticketRepository.Create(dbticket);
                unitOfWork.Save();
            }
        }
        public void DeleteEntity(int id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ticketRepository = new TicketRepository(unitOfWork);
                ticketRepository.Delete(id);
                unitOfWork.Save();
            }
        }
        public void DeleteEntity(string name1, string name2)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ticketRepository = new TicketRepository(unitOfWork);
                ticketRepository.Delete(name1, name2);
                unitOfWork.Save();
            }
        }
        public void UpdateEntity(string name1, IEntity ticket)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ticketRepository = new TicketRepository(unitOfWork);
                var dbticket = _mapper.Map<DBTicket>(ticket);
                ticketRepository.Update(name1, dbticket);
                unitOfWork.Save();
            }
        }
        public IEnumerable<IEntity> GetEntity()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ticketRepository = new TicketRepository(unitOfWork);
                var dbtickets = ticketRepository.GetAll();
                var tickets = _mapper.Map<IEnumerable<Ticket>>(dbtickets);
                return tickets;
            }
        }
    }
}
