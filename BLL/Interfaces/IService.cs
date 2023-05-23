using DAL.DBEntities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IService
    {
        void AddEntity(IEntity entity);
        void DeleteEntity(int id);
        void DeleteEntity(string name1, string name2);
        void UpdateEntity(string name1, IEntity entity);
        IEnumerable<IEntity> GetEntity();
    }
}
