using System.Collections.Generic;
//using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using DAL.Configurations;
using DAL.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ShowContext : DbContext
    {
        public ShowContext(DbContextOptions<ShowContext> options) : base(options)
        {
        }
        public DbSet<DBShow> Shows { get; set; }
        public DbSet<DBTicket> Tickets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ShowContext;Trusted_Connection=True;");
        }
    }
}