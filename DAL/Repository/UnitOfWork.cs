using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Runtime.Remoting.Contexts;

namespace DAL.Repository
{
    public class UnitOfWork : IDisposable
    {
        private ShowContext db;
        private bool disposed = false;
        public UnitOfWork()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShowContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ShowContext;Trusted_Connection=True;");
            var dbContextOptions = optionsBuilder.Options;
            db = new ShowContext(dbContextOptions);
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
