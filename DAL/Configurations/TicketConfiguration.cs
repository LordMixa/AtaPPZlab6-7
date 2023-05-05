using DAL.DBEntities;
using System.Data.Entity.ModelConfiguration;

namespace DAL.Configurations
{
    public class TicketConfiguration : EntityTypeConfiguration<DBTicket>
    {
        public TicketConfiguration()
        {
            ToTable("Tickets");

            Property(c => c.NameShow)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.NameOfOwner)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.Date)
                .IsRequired();

            Property(c => c.Price)
                .IsRequired();
        }
    }
}
