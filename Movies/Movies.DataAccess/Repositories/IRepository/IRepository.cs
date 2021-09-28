using System.Collections.Generic;

namespace Movies.DataAccess.Repository.IRepository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        int Add(T entity);
        void Delete(int id);
        void Update(T entity);
    }
}
