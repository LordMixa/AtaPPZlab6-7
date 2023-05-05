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

        public void Create(DBTicket book)
        {
            _ticketSet.Add(book);
        }

        public void Update(DBTicket item)
        {
            _ticketSet.Attach(item);
            _unitOfWork.Context.Entry(item).State = EntityState.Modified;
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
