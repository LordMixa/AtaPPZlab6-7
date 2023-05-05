using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using DAL.Configurations;
using DAL.DBEntities;

namespace DAL
{
    public class ShowContext : DbContext
    {
        public ShowContext(DbContextOptions<ShowContext> options) : base("name=ShowContext")
        {
        }
        public DbSet<DBShow> Shows { get; set; }
        public DbSet<DBTicket> Tickets { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ShowConfiguration());
            modelBuilder.Configurations.Add(new TicketConfiguration());
        }
    }
}