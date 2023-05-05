using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.DBEntities
{
    public class DBTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DBTicketId { get; set; }
        public string NameShow { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string NameOfOwner { get; set; }
    }
}
