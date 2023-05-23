﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using DAL.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ShowRepository : IRepository<DBShow>
    {
        public readonly UnitOfWork _unitOfWork;
        public readonly DbSet<DBShow> _showSet;
        public ShowRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _showSet = _unitOfWork.Context.Set<DBShow>();
        }
        public void Create(DBShow item)
        {
            _showSet.Add(item);
        }

        public void Delete(int id)
        {
            DBShow show = _showSet.Find(id);
            if (show != null)
                _showSet.Remove(show);
        }
        public void Delete(string name1, string name2)
        {
            var objectToDelete = _showSet.FirstOrDefault(x => x.Name == name1 && x.Author == name2);

            if (objectToDelete != null)
                _showSet.Remove(objectToDelete);
        }
        public DBShow Get(int id)
        {
            return _showSet.Find(id);
        }

        public IEnumerable<DBShow> GetAll()
        {
            return _showSet;
        }
        public void Update(string name1,DBShow entity)
        {
            var existingEntity = _showSet.FirstOrDefault(s => s.Name == name1);
            if (existingEntity != null)
            {
                _showSet.Remove(existingEntity);
                _unitOfWork.Save();
                _showSet.Add(entity);
            }
        }
    }
}
