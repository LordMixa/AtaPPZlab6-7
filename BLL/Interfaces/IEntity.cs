using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IEntity
    {
        string Name { get; set; }
        DateTime Date { get; set; }
        double Price { get; set; }
    }
}
