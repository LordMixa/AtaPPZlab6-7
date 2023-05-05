using System;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace DAL.Repository
{
    public class UnitOfWork : IDisposable
    {
        private ShowContext db;
        private bool disposed = false;
        public UnitOfWork()
        {
            db = new ShowContext();
        }

        public void Save()
        {
            db.SaveChanges();
        }
        public DbContext Context => db;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
