using System.Collections.Generic;

namespace DAL.Repository
{
    internal interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(string name1, T item);
        void Delete(int id);
        void Delete(string name1, string name2);
    }
}
