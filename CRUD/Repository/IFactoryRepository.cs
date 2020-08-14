using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Repository
{
    interface IFactoryRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        string Get(int? id);
        string Create(string item);
        string Update(int id, string item);
        bool Delete(int id);

    }
}
