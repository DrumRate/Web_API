using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Repository
{
    public interface IFactoryRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int? id);
        Task<T> Create(T item);
        Task<T> Update(int id, T item);
        Task<bool> Delete(int id);

    }
}
