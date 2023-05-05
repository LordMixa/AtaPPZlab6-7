using DAL.DBEntities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repository
{
    public class TicketRepository : IRepository<DBTicket>
    {
        public readonly UnitOfWork _unitOfWork;
        public readonly DbSet<DBTicket> _ticketSet;
        public TicketRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ticketSet = _unitOfWork.Context.Set<DBTicket>();
        }
        public IEnumerable<DBTicket> GetAll()
        {
            return _ticketSet;
        }

        public DBTicket Get(int id)
        {
            return _ticketSet.Find(id);
        }

        public void Create(DBTicket ticket)
        {
            _ticketSet.Add(ticket);
        }

        public void Update(string name1, string name2, DBTicket entity)
        {
            var existingEntity = _ticketSet.Find(name1, name2);
            if (existingEntity != null)
                _unitOfWork.Context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public void Delete(int id)
        {
            DBTicket book = _ticketSet.Find(id);
            if (book != null)
                _ticketSet.Remove(book);
        }
        public void Delete(string name1, string name2)
        {
            var objectToDelete = _ticketSet.FirstOrDefault(x => x.NameShow == name1 && x.NameOfOwner == name2);

            if (objectToDelete != null)
                _ticketSet.Remove(objectToDelete);
        }
    }
}
